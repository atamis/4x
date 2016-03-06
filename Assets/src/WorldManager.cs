using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.actor;
using game.map.units;

namespace game {
	class WorldManager  {
		GameManager parent;
		WorldMap wm;
		Player player;

		GameObject nFolder;
		List<Node> nodes;

		Vector2 startloc;
		Dictionary<Vector2, Biome> seeds;
		int MAPSIZE = 64;
		public int BIOME_SIZE = 4;

		float[,] heights;

		public WorldManager(GameManager parent, WorldMap wm, Player player) {
			this.parent = parent;
			this.player = player;

			// initialize the world map
			this.wm = wm;
			wm.map = new Dictionary<HexLoc, Hex>();

			nFolder = new GameObject ("Nodes");
			nodes = new List<Node> ();

			// generate the player
			startloc = gen_player ();

			// generate the biomes
			seeds = gen_biomes();

			// generate the nodes
			gen_nodes();

			// generate the terrain
			gen_terrain();
		}

		/*
		 * generate the player's starting location
		 */
		Vector2 gen_player() {
			int center = MAPSIZE / 2;
			int offset = 5; // the distance the player is from the true center of the map

			int start_x = center + Random.Range (-offset, offset);
			int start_y = center + Random.Range (-offset, offset);

			Building b1 =  new GameObject("WarpGate1").AddComponent<WarpGate>();
			b1.init(player, wm.map[new HexLoc(start_x, start_y)]);

			Unit u1 = new GameObject("Unit1").AddComponent<Unit>();
			u1.init(wm, wm.map[new HexLoc(start_x + Random.Range(-1, 1), start_y+Random.Range(-1, 1))]);

			Unit u2 = new GameObject("Unit2").AddComponent<Unit>();
			u2.init(wm, wm.map[new HexLoc(start_x + Random.Range(-1, 1), start_y+Random.Range(-1, 1))]);

			return new Vector2 (start_x, start_y);
		}

		/*
		 * generates the inital biome seeds
		 */
		Dictionary<Vector2, Biome> gen_biomes() {
			Dictionary<Vector2, Biome> seeds = new Dictionary<Vector2, Biome> ();

			int bsize = 8;
			int steps = MAPSIZE / bsize;
			int bcount = System.Enum.GetNames(typeof(Biome)).Length;

			// set biome seeds
			for (int x = 0; x < steps; x++) {
				for (int y = 0; y < steps; y++) {
					int i = Random.Range(bsize*x, bsize*(x+1)); int j = Random.Range(bsize*y, bsize*(y+1));
					seeds.Add(new Vector2(i, j), (Biome)Random.Range(0, bcount-1));
				}
			}

			// set biomes for all the nodes
			List<Vector2> keys = new List<Vector2> (seeds.Keys);
			for (int x = 0; x < MAPSIZE; x++) {
				for (int y = 0; y < MAPSIZE; y++) {
					Vector2 pos = new Vector2 (x, y);
					HexLoc loc = new HexLoc (x, y);
					Biome b = seeds[GetNearest (pos, keys)];
					wm.map [loc].b = b;
				}
			}
			return seeds;
		}
	
		/*
		 * generate the nodes
		 */
		void gen_nodes() {
			int quadrants = MAPSIZE / 4;

			for (int x = 0; x < quadrants; x++) {
				for (int y = 0; y < quadrants; y++) {
					int i = Random.Range(4*x, 4*(x+1)); int j = Random.Range(4*y, 4*(y+1));
					makeNode (i, j);
				}
			}
		}

		/*
		 * generate mountains and rivers
		 */
		void gen_terrain() {
			// TODO
		}

		/*
		 * generate corruption
		 */
		void gen_corruption() {
			// TODO
		}

		Vector2 GetNearest(Vector2 point, List<Vector2> points) {
			Vector2 nearest = new Vector2(0, 0);
			float prev = Mathf.Infinity;
			foreach (Vector2 v in points) {
				float distance = Vector2.Distance (point, v);
				//print("Distance from " + point + " to " + v + ":" + distance);
				if (distance < prev) {
					nearest = v;
					prev = distance;
				}
			}
			return nearest;
		}

		void makeHex(int x, int y) {
			HexLoc hl = new HexLoc (x, y);

			var o = new GameObject(l.ToString());
			o.transform.parent = transform;
			Hex h = o.AddComponent<Hex>();
			h.init(this, l);

			wm.map.Add(, h);
		}

		void makeNode(int x, int y) {
			//print("Placed node at: " + i + ", " + j);

			var obj = new GameObject ("Node");
			obj.transform.parent = nFolder.transform;
			obj.transform.position = new Vector2(x, y);

			Node node = obj.AddComponent<Node>();

			Hex h = wm.map [];
			node.init (h);
			h.node = node;
			nodes.Add (node);
		}
	}
}
