using UnityEngine;
using System.Collections.Generic;
using System;
using game.actor;
using game.math;
using game.world;
using game.world.units;
using game.world.buildings;

namespace game.world {
	class WorldMap : MonoBehaviour {
		public Dictionary<HexLoc, Hex> map;

		GameObject hFolder; GameObject nFolder;
		public List<Node> nodes; public List<Unit> units;

		public Layout l;
		public int size = 32;
		public int turn;
		public BeamManager bm;

		//public Color[] colors = new Color[] { new Color(1, 1, 1), new Color(0.5f, 0.5f, 0.5f) };

		public void init(Layout l) {
			this.l = l;
			turn = 0;

			hFolder = new GameObject ("Hexes"); nFolder = new GameObject ("Nodes");
			nodes = new List<Node> (); units = new List<Unit> ();

			this.bm = new BeamManager(this);

			map = new Dictionary<HexLoc, Hex> ();
		}

		public void makeHex(HexLoc l) {
			var obj = new GameObject (l.ToString ());
			obj.transform.parent = hFolder.transform;

			Hex h = obj.AddComponent<Hex>();
			h.init(this, l);
			map.Add(l, h);
		}

		public void makeNode(HexLoc loc) {
			var obj = new GameObject ("Node");
			obj.transform.parent = nFolder.transform;

			Node n = obj.AddComponent<Node>();
			n.init(map [loc]);
			map [loc].node = n;

			nodes.Add (n);
			//UnityEngine.Debug.Log ("Generated node at " + pos);
		}

		public void makeUnit(Actor p, HexLoc loc) {
			Unit u = new GameObject ("Unit").AddComponent<Unit> ();
			u.init (p, this, map [loc]);

			units.Add (u);
			UnityEngine.Debug.Log ("Added Unit at " + loc.ToString ());
		}

		public void makeMiasma(HexLoc loc, int aggression) {
			if (map [loc].miasma != null) {
				return;
			}

			Miasma m = new GameObject ("Miasma").AddComponent<Miasma> ();
			m.init (this, map [loc], aggression);

			UnityEngine.Debug.Log ("Added Miasma at " + loc.ToString ());
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

			EventManager.PostDefeatEvent(new GameEventArgs {});
            return true;
        }

        internal bool Victory() {
            foreach(KeyValuePair<HexLoc, Hex> kv in map) {
                if (kv.Value.miasma != null) {
                    return false;
                }
            }

			EventManager.PostVictoryEvent(new GameEventArgs {});
            return true;
        }

        public void PostTurn(Actor old, Actor cur) {
			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.PostTurn(old, cur);
			}
		}
	}
}
