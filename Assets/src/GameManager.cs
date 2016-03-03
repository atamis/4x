using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.input;
using game.actor;
using game.map.units;

namespace game {
    public class GameManager : MonoBehaviour {

        PlayerCamera pc;
        private Layout l;
		private WorldMap w;
        MapClick mc;
        private List<Actor> actors;
        private int currentActor;
        private Player player;
		WorldManager wm;
		AudioManager am;
        TooltipUI t;

        // Use this for initialization
        void Start() {
            player = new Player("Player");
            actors = new List<Actor>();
            actors.Add(player);
            actors.Add(new AIActor());
            currentActor = 0;


            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));

            w = new GameObject("World Map").AddComponent<WorldMap>();
            w.init(l);

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);

            mc = gameObject.AddComponent<MapClick>();
            mc.init(w, player);

            t = gameObject.AddComponent<TooltipUI>();
            t.init(w);

            gameObject.AddComponent<PlayerControl>().init(player, mc);

            t = gameObject.AddComponent<TooltipUI>();
            t.init(w);

            wm = new WorldManager(this, w, player);

            w.map[new HexLoc(32, 63, -95)].corrupted = true;

			/*
            Building b1 = new GameObject("WarpGate1").AddComponent<WarpGate>();
            b1.init(player, w.map[new HexLoc(0, 0, 0)]);

            Building b2 = new GameObject("Conduit1").AddComponent<Conduit>();
            b2.init(player, w.map[new HexLoc(2, 2, -4)]);

            Building b3 = new GameObject("Conduit2").AddComponent<Conduit>();
            b3.init(player, w.map[new HexLoc(1, 1, -2)]);

            Building b4 = new GameObject("Harvester1").AddComponent<Harvester>();
            b4.init(player, w.map[new HexLoc(5, 6, -11)]);
			*/

            w.PreTurn(null, actors[currentActor]);
            w.NewTurn(null, actors[currentActor]);
            w.PostTurn(null, actors[currentActor]);
            
			am = gameObject.AddComponent<AudioManager> ();
			am.init (this);
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

                w.PreTurn(ca, actors[currentActor]);
                w.NewTurn(ca, actors[currentActor]);
                w.PostTurn(ca, actors[currentActor]);

                actors[currentActor].StartTurn();
            }
        }

		void OnGui() {

		}
    }
}
