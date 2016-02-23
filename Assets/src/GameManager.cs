using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.input;
using game.actor;

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
            currentActor = 0;

            new GameObject("Player Control").AddComponent<PlayerControl>().init(player);
        


            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));


            w = new GameObject("World Map").AddComponent<WorldMap>();
            w.init(l);

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);

            mc = new GameObject("Map Click").AddComponent<MapClick>();
            mc.init(w);

            
        }
            




        // Update is called once per frame
        void Update() {
            Actor ca = actors[currentActor];
            Command c = ca.GetNextCommand();
            
            if (c == null)
                return;

            c.Apply(w);

            if (c.GetType() == typeof(EndTurnCommand)) {
                print(ca + " ends their turn.");
                currentActor = (currentActor + 1) % actors.Count;
            }
        }
    } 
}