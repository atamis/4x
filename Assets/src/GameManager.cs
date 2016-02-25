using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.input;
using game.actor;
using game.map.units;

/*
 * Current z-level layout:
 * z = 0  : game board
 * z = -1 : units
 */

namespace game {
    public class GameManager : MonoBehaviour {

        PlayerCamera pc;
        private Layout l;
        private WorldMap w;
        MapClick mc;
        private List<Actor> actors;
        private int currentActor;
        private Player player;
        private CorruptManager cm;

        // Use this for initialization
        void Start() {
            player = new Player("Player");
            actors = new List<Actor>();
            actors.Add(player);
            actors.Add(new PassTurnActor("Pass Turn Actor"));
            currentActor = 0;

            new GameObject("Player Control").AddComponent<PlayerControl>().init(player);
        


            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));


            w = new GameObject("World Map").AddComponent<WorldMap>();
            w.init(l);

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);
            
            mc = new GameObject("Map Click").AddComponent<MapClick>();
            mc.init(w, player);

            cm = new GameObject("Corruption Manager").AddComponent<CorruptManager>();
            cm.init(w);

            Unit u1 = new GameObject("Unit2").AddComponent<Unit>();
            u1.init(w, w.map[new HexLoc(1, 0, -1)]);

            Unit u2 = new GameObject("Unit2").AddComponent<Unit>();

            u2.init(w, w.map[new HexLoc(1, 2, -3)]);

            mc = new GameObject("Map Click").AddComponent<MapClick>();
            mc.init(w, player);
        }
            




        // Update is called once per frame
        void Update() {
            Actor ca = actors[currentActor];
            Command c = ca.GetNextCommand();
            
            if (c == null)
                return;

            print("Executing " + c.ToString());
            c.Apply(w);

            if (c.GetType() == typeof(EndTurnCommand)) {
                print(ca + " ends their turn.");
                currentActor = (currentActor + 1) % actors.Count;
                w.NewTurn(ca, actors[currentActor]);
                cm.processTurn(); //Process Corruptions turn after player is done
            }
        }
    } 
}