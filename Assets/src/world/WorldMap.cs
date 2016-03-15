using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.world;
using game.world.buildings;
using game.actor;
using System;

namespace game.world {
	class WorldMap : MonoBehaviour {
		GameObject hFolder; GameObject nFolder;
		public Dictionary<HexLoc, Hex> map;
		public List<Node> nodes;
		public Layout l;
		public int size = 32;
		public int turn;
		public BeamManager bm;

		public Color[] colors = new Color[] { new Color(1, 1, 1), new Color(0.5f, 0.5f, 0.5f) };

		public void init(Layout l) {
			this.l = l;
			turn = 0;

			hFolder = new GameObject ("Hexes"); nFolder = new GameObject ("Nodes");

			this.bm = new BeamManager(this);

			map = new Dictionary<HexLoc, Hex> ();
			nodes = new List<Node> ();
		}

		public void makeHex(HexLoc l) {
			var obj = new GameObject (l.ToString ());
			obj.transform.parent = hFolder.transform;

			Hex h = obj.AddComponent<Hex>();
			h.init(this, l);
			map.Add(l, h);
		}

		public void makeNode(int x, int y) {
			var obj = new GameObject ("Node");
			obj.transform.parent = nFolder.transform;

			HexLoc loc = new HexLoc (x, y);

			Node n = obj.AddComponent<Node>();
			n.init(map [loc]);
			map [loc].node = n;

			nodes.Add (n);

			//UnityEngine.Debug.Log ("Placed node at " + pos);
		}

		public void PreTurn(Actor old, Actor cur) {
			turn++;

			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.PreTurn(old, cur);
			}
		}

		public void NewTurn(Actor old, Actor cur) {
			List<Building> poweredBuildings = new List<Building>();
			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.pn = null;
				var b = kv.Value.building;
				if (b != null) {
					b.grided = false;
					b.pn = null;
					if (b.Powered()) {
						poweredBuildings.Add(b);
					}
				}
			}

			bm.Clear();

			foreach (Building b in poweredBuildings) {
				if (!b.grided) {
					b.SpreadPower(new PowerNetwork());
				}
			}

			bm.Create();

			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.NewTurn(old, cur);
			}
		}

        internal bool Defeat(Actor a) {
            foreach (KeyValuePair<HexLoc, Hex> kv in map) {
                var building = kv.Value.building;
                var units = kv.Value.units;
                if ((building != null && building.a == a)
                    || units.Count > 0) {
                    return false;
                }
            }
            return true;
        }

        internal bool Victory() {
            foreach(KeyValuePair<HexLoc, Hex> kv in map) {
                if (kv.Value.miasma != null) {
                    return false;
                }
            }
            return true;
        }

        public void PostTurn(Actor old, Actor cur) {
			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.PostTurn(old, cur);
			}
		}
	}
}
