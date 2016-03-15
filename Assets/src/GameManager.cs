using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using game.actor;
using game.actor.commands;
using game.ui;
using game.math;
using game.effects;
using game.world;
using game.world.buildings;

namespace game {
	class GameManager : MonoBehaviour {
        public PlayerCamera pc;
        private Layout l;
		WorldMap w;
		Player player;
        AnnihilationManager am;
        TutorialManager tm;

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

			tm = gameObject.AddComponent<TutorialManager>();
			tm.init();

			int seed = Random.Range (1000000000, int.MaxValue);
			WorldMaker maker = new WorldMaker (w, player, seed, false);
			maker.genWorld();
            var lookat = w.l.HexPixel(new HexLoc((int) maker.spawn.x, (int) maker.spawn.y));
			pc.setLocation (lookat.x, lookat.y);

			w.PreTurn(null, actors[currentActor]);
			w.NewTurn(null, actors[currentActor]);
			w.PostTurn(null, actors[currentActor]);

            am = gameObject.AddComponent<AnnihilationManager>();
            am.init(w, player);

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


            if (c.GetType () == typeof(EndTurnCommand)) {
				print (ca + " ends their turn.");
				currentActor = (currentActor + 1) % actors.Count;


                if (w.Victory()) {
                    print("Victory");
                    SceneManager.LoadScene("Victory");
                }

                if (w.Defeat(player)) {
                    print("Defeat");
                    SceneManager.LoadScene("Defeat");
                }

                w.PreTurn (ca, actors [currentActor]);
				w.NewTurn (ca, actors [currentActor]);
				w.PostTurn (ca, actors [currentActor]);

				actors [currentActor].StartTurn ();
			}
		}
    }
}
