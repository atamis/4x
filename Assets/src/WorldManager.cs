﻿using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.actor;
using game.map.units;
using game.input;

namespace game {
	class WorldManager  {

		GameManager parent;
		WorldMap wm;
		Player player;
		GameObject nFolder;
		List<Node> nodes;
		PlayerCamera pc;
		public int BIOME_SIZE = 4;
		int offset = 5; // the distance the player is from the true center of the map

		public WorldManager(GameManager parent, WorldMap wm, Player player, PlayerCamera pc) {
			this.parent = parent;
			this.wm = wm;
			this.player = player;
			this.pc = pc;

			gen_map (64);
			gen_player (64);
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

		void gen_player(int mapsize) {
			int center = mapsize / 2;

			

			int start_x = center + Random.Range (-offset, offset);
			int start_y = center + Random.Range (-offset, offset);
				
			pc.cam.transform.Translate( new Vector3(start_x * 1.7f, start_y * 1.5f, 0));			

			Building b1 =  new GameObject("WarpGate1").AddComponent<WarpGate>();
			b1.init(player, wm.map[new HexLoc(start_x, start_y)]);

			Unit u1 = new GameObject("Unit1").AddComponent<Unit>();
			u1.init(wm, wm.map[new HexLoc(start_x + Random.Range(-1, 1), start_y+Random.Range(-1, 1))]);

			Unit u2 = new GameObject("Unit2").AddComponent<Unit>();
			u2.init(wm, wm.map[new HexLoc(start_x + Random.Range(-1, 1), start_y+Random.Range(-1, 1))]);
		}
	}
}
