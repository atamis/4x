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

        // Use this for initialization
        void Start() {
            player = new Player("Player");
            actors = new List<Actor>();
            actors.Add(player);
            actors.Add(new AIActor());
            currentActor = 0;

            new GameObject("Player Control").AddComponent<PlayerControl>().init(player);
        


            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));


            w = new GameObject("World Map").AddComponent<WorldMap>();
            w.init(l);

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);
            
            mc = gameObject.AddComponent<MapClick>();
            mc.init(w, player);

            Building b1 = new GameObject("Harvester1").AddComponent<Harvester>();
            b1.init(player, w.map[new HexLoc(0, 0, 0)]);

            Building b2 = new GameObject("Conduit1").AddComponent<Conduit>();
            b2.init(player, w.map[new HexLoc(2, 2, -4)]);

            Building b3 = new GameObject("Conduit2").AddComponent<Conduit>();
            b3.init(player, w.map[new HexLoc(1, 1, -2)]);

            Building b4 = new GameObject("Harvester1").AddComponent<Harvester>();
            b4.init(player, w.map[new HexLoc(5, 6, -11)]);

            Unit u1 = new GameObject("Unit2").AddComponent<Unit>();
            u1.init(w, w.map[new HexLoc(1, 0, -1)]);

            Unit u2 = new GameObject("Unit2").AddComponent<Unit>();

            u2.init(w, w.map[new HexLoc(1, 2, -3)]);

            mc = new GameObject("Map Click").AddComponent<MapClick>();
            mc.init(w, player);

            w.NewTurn(null, actors[currentActor]);
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
                actors[currentActor].StartTurn();
            }
        }
    } 
}