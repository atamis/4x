using UnityEngine;
using System.Collections.Generic;
using game.actor;
using game.actor.commands;
using game.ui;
using game.math;
using game.effects;
using game.world;

namespace game {
	class GameManager : MonoBehaviour {
        public PlayerCamera pc;
        private Layout l;
		WorldMap w;
		Player player;

		private List<Actor> actors;
		private int currentActor;

        void Start() {
			player = new Player ("Player");
			actors = new List<Actor> ();
			actors.Add (player);
			actors.Add (new AIActor ());
			currentActor = 0;

			pc = new GameObject ("Player Camera").AddComponent<PlayerCamera> ();
			pc.init (this, Camera.main);

			this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));

			w = new GameObject ("World Map").AddComponent<WorldMap> ();
			w.init (l);

			int seed = Random.Range (1000000000, int.MaxValue);
			WorldMaker maker = new WorldMaker (w, player, seed, false);
			maker.genWorld();
			pc.setLocation (maker.spawn.x, maker.spawn.y);

			w.PreTurn(null, actors[currentActor]);
			w.NewTurn(null, actors[currentActor]);
			w.PostTurn(null, actors[currentActor]);

			gameObject.AddComponent<UIManager> ().init (player, w);

			gameObject.AddComponent<AudioManager> ().init(this);
            gameObject.AddComponent<AmbienceManager>().init(pc, w);

			EventManager.TriggerStartEvent(new GameEventArgs{} );
        }

		void Update() {
			Actor ca = actors[currentActor];
			Command c = ca.GetNextCommand ();

			if (c == null) {
				return;
			}

			print ("Executing + " + c.ToString());
			c.Apply (w);

            if (w.Victory()) {
                print("Victory");
            }

            if (w.Defeat(player)) {
                print("Defeat");
            }

            if (c.GetType () == typeof(EndTurnCommand)) {
				print (ca + " ends their turn.");
				currentActor = (currentActor + 1) % actors.Count;

				w.PreTurn (ca, actors [currentActor]);
				w.NewTurn (ca, actors [currentActor]);
				w.PostTurn (ca, actors [currentActor]);

				actors [currentActor].StartTurn ();
			}
		}
    }
}
