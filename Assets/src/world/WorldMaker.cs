using UnityEngine;
using System.Collections.Generic;
using game.actor;
using game.math;
using game.world.units;
using game.world.buildings;

namespace game.world {
	class WorldMaker {
		Player player;
		WorldMap w;
		public Vector2 spawn;
		private int bsize;
		public int RIVER_SPAWN_BOUNDS = 5;
		public int INNER_SPAWN_BOUNDS = 10;
		public int OUTER_SPAWN_BOUNDS = 10;

		public WorldMaker(WorldMap w, Player player, int seed, bool bigbiome) {
			this.w = w;
			this.player = player;

			if (bigbiome) {
				bsize = 10;
			} else {
				bsize = 6;
			}

			int off = UnityEngine.Random.Range (-3, 3); // offset from the center
			int c = w.size / 2; // center of the map

			spawn = new Vector2 (c + UnityEngine.Random.Range (-off, off), c + UnityEngine.Random.Range (-off, off));
		}

		private Vector2 getNearest(Vector2 pos, List<Vector2> points) {
			Vector2 nearest = new Vector2(0, 0);
			float prev = Mathf.Infinity;
			foreach (Vector2 v in points) {
				float distance = Vector2.Distance (pos, v);
				if (distance < prev) {
					nearest = v;
					prev = distance;
					if (distance < 4) {
						break;
					}
				}
			}
			return nearest;
		}

		bool inBounds(Vector2 pos, int bounds) {
			if ((pos.x >= spawn.x - bounds && pos.x <= spawn.x + bounds) && (pos.y >= spawn.y - bounds && pos.y <= spawn.y + bounds)) {
				return true;
			}
			return false;
		}

		public void genWorld() {
			// world borders
			genBiomes ();
			genPlayer ();
			genNodes ();
            genPresents();
			genRivers (2, 20);

			INNER_SPAWN_BOUNDS = 6;
			OUTER_SPAWN_BOUNDS = 7;
			genCorruption(2, 1);

			INNER_SPAWN_BOUNDS = 10;
			OUTER_SPAWN_BOUNDS = 12;
			genCorruption (2, 4);

			decorate ();
		}

		void genBiomes() {
			Dictionary<Vector2, Biome> seeds = new Dictionary<Vector2, Biome> ();
			int steps = w.size / bsize;
			int bcount = System.Enum.GetNames (typeof(Biome)).Length;

			for (int x = 0; x < steps; x++) {
				for (int y = 0; y < steps; y++) {
					int i = Random.Range (bsize * x, bsize * (x + 1));
					int j = Random.Range (bsize * y, bsize * (y + 1));
					seeds.Add (new Vector2 (i, j), (Biome)Random.Range (0, bcount - 1));
				}
			}

			List<Vector2> keys = new List<Vector2> (seeds.Keys);
			for (int x = 0; x < w.size; x++) {
				for (int y = 0; y < w.size; y++) {
					HexLoc loc = new HexLoc (x, y);
					w.makeHex (loc);

					Biome b = seeds [getNearest (new Vector2 (x, y), keys)];
					w.map [loc].b = b;
				}
			}
			//Debug.Log("Generated biomes");
		}

		void genPlayer() {
			WarpGate b1 =  new GameObject("WarpGate1").AddComponent<WarpGate>();
			b1.init(player, w.map[new HexLoc((int)spawn.x, (int)spawn.y)]);
            b1.power = 50;


            int c = 0;
			while (c < 2) {
				HexLoc loc = new HexLoc((int)spawn.x + Random.Range(-1, 1), (int)spawn.y+Random.Range(-1, 1));
				if (w.map[loc].units.Count == 0 && w.map[loc].b.Passable()) {
					w.makeUnit (player, loc);
					//Unit u = new GameObject("Unit" + c).AddComponent<Unit>();
					//u.init(player, w, w.map[loc]);
					c++;
				}
			}
			//Debug.Log (System.String.Format ("Generated Player at {0}", spawn));
		}

		private void genNodes() {
			int quadrants = w.size / 4;

			for (int x = 0; x < quadrants; x++) {
				for (int y = 0; y < quadrants; y++) {
					int i = Random.Range(4*x, 4*(x+1)); int j = Random.Range(4*y, 4*(y+1));
					w.makeNode (new HexLoc(i, j));
				}
			}
			//Debug.Log ("Generated Nodes");
		}

        private void genPresents() {
            int quadrants = w.size / 8; // MAXIMUM OVERPRESENT

            for (int x = 0; x < quadrants; x++) {
                for (int y = 0; y < quadrants; y++) {
                    int i = Random.Range(8 * x, 8 * (x + 1)); int j = Random.Range(8 * y, 8 * (y + 1));
                    if (w.map.ContainsKey(new HexLoc(i, j))) {
                        Present p;
                        if (Random.value < 0.5f) {
                            p = new GameObject("Present").AddComponent<RevealPresent>();
                        } else {
                            p = new GameObject("Present").AddComponent<UnitPresent>();
                        }
                        p.init(w.map[new HexLoc(i, j)]);
                    }

                }
            }
            //Debug.Log ("Generated Nodes");
        }

        private void decorate() {
			for (int x = 0; x < w.size; x++) {
				for (int y = 0; y < w.size; y++) {
					// Set EV value
					HexLoc loc = new HexLoc (x, y);
					if (w.map [loc].node != null) {
						setEv (w.map[loc], 1.0f);
					}

					// Colorize the World
					Color c = w.map [loc].b.GetColor () + new Color(.9f, .9f, .9f, 1);
					foreach (Hex h in w.map[loc].Neighbors()) {
						c += h.b.GetColor ();
					}
					c = (c / 6) * 1.15f;
					c.a = 1; 
					w.map [loc].SetColor (c) ;
					//UnityEngine.Debug.Log (c);
				}
			}
			UnityEngine.Debug.Log ("Decorated world");
		}

		private void setEv(Hex h, float value) {
			h.ev = value;

			float val = value - h.b.Dropoff ();
			foreach (Hex h2 in h.Neighbors()) {
				if (val > h2.ev) {
					setEv (h2, val);
				}
			}
		}

		void genRivers(int count, int length) {
			int c = 0;
			while (c < count) {
				int x = Random.Range (0, w.size); int y = Random.Range (0, w.size);
<<<<<<< HEAD
				if (inBounds (new Vector2 (x, y), RIVER_SPAWN_BOUNDS)) {
=======
				if (inBounds (new Vector2 (x, y), 5)) {
>>>>>>> c2cb5a894716cc9db8cc62bd743927b6c54f474b
					continue;
				}

				int dir = UnityEngine.Random.Range (0, 5);
				HexLoc loc = new HexLoc (x, y);
				for (int i = 0; i < length; i++) {
					int step = (Random.Range (0, 2) + dir) % 6;
					while (!w.map.ContainsKey(loc.Neighbor(step))) {
						step += 1;
					}
					w.map[loc].b = Biome.Ocean;
					loc = loc.Neighbor (step);
				}

				genLake (x, y, 2);
				c++;
			}
			//Debug.Log (System.String.Format ("Generated {0} rivers", count));
		}

		void genLake(int x, int y, int size) {
			HexLoc loc = new HexLoc (x, y);
			w.map [loc].b = Biome.Ocean;
			foreach (Hex h in w.map[loc].Neighbors()) {
				h.b = Biome.Ocean;
			}
		}

		void genCorruption(int count, int aggression) {
			int c = 0;
			while (c < count) {
				int x = Random.Range (0, w.size); int y = Random.Range (0, w.size);
				HexLoc loc = new HexLoc (x, y);
				if ((w.map[loc].b == Biome.Ocean) || (inBounds(new Vector2(x, y), INNER_SPAWN_BOUNDS)) || (!inBounds(new Vector2(x, y), OUTER_SPAWN_BOUNDS))) {
					continue;
				}

				Miasma m = new GameObject ("Miasma").AddComponent<Miasma> ();
				m.init (w, w.map [loc], aggression);
				c++;
			}

			//Debug.Log (System.String.Format ("Generated {0} corruption seeds", count));
		}
	}
}
