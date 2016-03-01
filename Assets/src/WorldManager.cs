using UnityEngine;
using game.map;
using System.Collections.Generic;

namespace game {
	class WorldManager  {

		GameManager parent;
		WorldMap wm;
		GameObject nFolder;
		List<Node> nodes;
		public int BIOME_SIZE = 4;

		public WorldManager(GameManager parent, WorldMap wm) {
			this.parent = parent;
			this.wm = wm;

			gen_map (64);
		}

		void gen_map(int size) {
			// seed the biomes
			Dictionary<Vector2, Biome> seeds = new Dictionary<Vector2, Biome> ();

			int bsize = 8;
			int steps = size / bsize;
			int bcount = System.Enum.GetNames(typeof(Biome)).Length;

			for (int x = 0; x < steps; x++) {
				for (int y = 0; y < steps; y++) {
					int i = Random.Range(bsize*x, bsize*(x+1)); int j = Random.Range(bsize*y, bsize*(y+1));
					seeds.Add(new Vector2(i, j), (Biome)Random.Range(0, bcount-1));
				}
			}
			// TODO
			// adjust starting biome

			List<Vector2> keys = new List<Vector2> (seeds.Keys);

			for (int x = 0; x < size; x++) {
				for (int y = 0; y < size; y++) {
					Vector2 pos = new Vector2 (x, y);
					HexLoc loc = new HexLoc (x, y);
					Biome b = seeds[GetNearest (pos, keys)];
					wm.map [loc].b = b;
				}
			}

			// TODO
			// Second-pass for rivers / oceans

			nFolder = new GameObject ("Nodes");
			nodes = new List<Node> ();

			placeNodes (size);
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

		void makeNode(int x, int y) {
			var obj = new GameObject ("Node");
			obj.transform.parent = nFolder.transform;
			obj.transform.position = new Vector2(x, y);

			Node node = obj.AddComponent<Node>();
			Hex h = wm.map [new HexLoc (x, y)];
			node.init (h);
			h.node = node;
			nodes.Add (node);
		}

		void placeNodes(int size) {
			int quads = size / 4;

			for (int x = 0; x < quads; x++) {
				for (int y = 0; y < quads; y++) {
					int i = Random.Range(4*x, 4*(x+1)); int j = Random.Range(4*y, 4*(y+1));
					makeNode (i, j);
					//print("Placed node at: " + i + ", " + j);
				}
			}
		}
	}
}
