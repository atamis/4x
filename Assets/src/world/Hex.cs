using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.actor;
using game.ui;
using game.world.buildings;
using game.world.units;

namespace game.world {
	public enum Biome {
        Highlands, Plains, Forest, Desert, Jungle,

        // MUST BE LAST ASK NICK
        Ocean,
    }

    public static class BiomeExtensions {
        private static Sprite hexagon = Resources.Load<Sprite>("Textures/Hexagon");
        private static Sprite desert = Resources.Load<Sprite>("Textures/T_Hex_Desert");
        private static Sprite forest = Resources.Load<Sprite>("Textures/T_Hex_Forest");
        private static Sprite highlands = Resources.Load<Sprite>("Textures/T_Hex_Highlands");
        private static Sprite jungle = Resources.Load<Sprite>("Textures/T_Hex_Jungle");
        private static Sprite ocean = Resources.Load<Sprite>("Textures/T_Hex_Ocean");
        private static Sprite plains = Resources.Load<Sprite>("Textures/T_Hex_Plains");

        public static Sprite GetSprite(this Biome b) {
            switch (b) {
                case Biome.Highlands:
                    return highlands;
                case Biome.Plains:
                    return plains;
                case Biome.Forest:
                    return forest;
                case Biome.Ocean:
                    return ocean;
                case Biome.Desert:
                    return desert;
                case Biome.Jungle:
                    return jungle;
                default:
                    return hexagon;
            }
        }

        public static float Dropoff(this Biome b) {
            switch (b) {
                case Biome.Highlands:
                    return 0.5f;
                case Biome.Plains:
                    return 0.3f;
                case Biome.Forest:
                    return 0.25f;
                case Biome.Ocean:
                    return 1f;
                case Biome.Desert:
                    return 0.3f;
                case Biome.Jungle:
                    return 0.25f;
                default:
                    return 0f;
            }

        }

        public static bool Passable(this Biome b) {
            return b.PassCost() != -1;
        }

        public static int PassCost(this Biome b) {
            switch(b) {
                case Biome.Highlands:
                    return 1;
                case Biome.Plains:
                    return 1;
                case Biome.Forest:
                    return 1;
                case Biome.Ocean:
                    return 1;
                case Biome.Desert:
                    return -1;
                case Biome.Jungle:
                    return 1;
                default:
                    return -1;
            }
        }
    }

    class Hex : MonoBehaviour {
        private HexModel model;
		public WorldMap wm;
        public Building building { get; set; }
		public Unit unit { get; set; }
        public Node node { get; set; }
        public Biome b { get; set; }
		public Miasma miasma { get; set; }
        public HexLoc loc { get; set; }
		public float ev { get; set; }
        public bool scanned { get; set; }
		public bool selected = false;
		public PowerNetwork pn;

		public bool powered {
			get {
				return pn != null;
			}
		}
		public HashSet<Unit> units;

        public void init(WorldMap wm, HexLoc loc) {
			this.wm = wm;
			this.loc = loc;

            var obj = new GameObject("Hex Model");
            obj.transform.parent = transform;
            model = obj.AddComponent<HexModel>();
            model.init(this);

            transform.localPosition = wm.l.HexPixel(loc);

			this.ev = 0;
			this.scanned = false;

			units = new HashSet<Unit>();
        }

		public void scan() {
			if (this.node != null) {
				this.node.setVisible ();
			}
		}

		public void reveal() {
			this.model.reveal ();
			if (this.miasma != null) {
				this.miasma.setVisible ();
			}
		}

        void Start() {

        }

        void Update() {

        }

        public List<Hex> Neighbors() {
            List<Hex> n = new List<Hex>();

            for(int i = 0; i < 6; i++) {
                HexLoc l = loc.Neighbor(i);
                if (wm.map.ContainsKey(l)) {
                    n.Add(wm.map[l]);
                }
            }

            return n;
        }

		internal void PreTurn(Actor old, Actor cur) {
			if (building != null) {
				building.PreTurn(old, cur);
			}
			if (unit != null) {
				unit.PreTurn(old, cur);
			}
		}

		public void NewTurn(Actor old, Actor cur) {
			if (building != null) {
				building.NewTurn(old, cur);
			}
			if (unit != null) {
				unit.NewTurn (old, cur);
			}
		}

		internal void PostTurn(Actor old, Actor cur) {
			if (building != null) {
				building.PostTurn(old, cur);
			}
			if (unit != null) {
				unit.PostTurn (old, cur);
			}
		}

		private class HexModel : MonoBehaviour {
			public Hex h;
			SpriteRenderer sp;
			Material[] mats;

			public void init(Hex h) {
				this.h = h;

				sp = this.gameObject.AddComponent<SpriteRenderer>();

				sp.transform.localScale = new Vector3(1.9f, 1.9f);
				transform.localPosition = new Vector3(0, 0, Layer.Board);

				mats = new Material[3] {
					new Material(Shader.Find("Sprites/Default")),
					new Material(Shader.Find("Custom/CRTShader")),
					new Material(Shader.Find("Custom/GlitchShader"))
				};
				sp.material = mats[0];
				sp.sprite = Resources.Load<Sprite> ("Textures/T_Fog");
			}

			void Update() {
				if (h.selected == true) {
					sp.color = new Color (1, 0, 0, 1);
				} else {
					sp.color = new Color (1, 1, 1, 1);
				}
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.LeftShift)) {
					if (this.h.scanned) {
						sp.material = mats [1];
						mats [1].SetFloat ("_EvValue", 1 - this.h.ev);
					} else {
						sp.material = mats [0];
						//sp.material = mats [1];
						//mats [1].SetFloat ("_EvValue", 1 - this.h.ev);
					}

				} else {
					sp.material = mats [0];
				}

			}

			public void reveal() {
				sp.sprite = h.b.GetSprite ();
			}
		}
    }


}