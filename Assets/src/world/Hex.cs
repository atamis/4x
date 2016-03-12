using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.actor;
using game.ui;
using game.world.buildings;
using game.world.units;
using game.effects;

namespace game.world {

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
        public bool revealed;
        internal WorldPathfinding.PathfindingInfo pathfind;

        public bool powered {
			get {
				return pn != null;
			}
		}

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
        }

		public void scan() {
			this.scanned = true;
			if (this.node != null) {
				this.node.setVisible ();
			}
		}

		public void reveal() {
            revealed = true;
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
			SpriteRenderer sr;
			CustomMaterial[] mats;
			Hex h;

			public void init(Hex h) {
				this.h = h;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Textures/T_Fog");

				transform.localScale = new Vector3 (1.9f, 1.9f, 1.9f);
				transform.localPosition = new Vector3 (0, 0, Layer.Board);

				mats = new CustomMaterial[4] {
					new CustomMaterial(Shader.Find("Sprites/Default")),
					new CustomMaterial(Shader.Find("Custom/NoirShader")),
					new CRTMaterial(Shader.Find("Custom/CRTShader")),
					new GlitchMaterial(Shader.Find("Custom/GlitchShader")),
				};
				sr.material = mats [0];
			}

			public void reveal() {
				sr.sprite = h.b.GetSprite ();
			}

			void Update() {
                if (h.revealed) {
                    sr.sprite = h.b.GetSprite();
                }

				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
					if (h.scanned) {
						// Glitch shader
						if (h.miasma != null) {
							sr.color = new Color (1, 1, 1);
							sr.material = mats [3];
							mats [3].tick ();

						// CRT shader
						} else {
							sr.material = mats [2];
							mats [2].SetFloat ("_EvValue", 1 - this.h.ev);
						}
					
					// BW Shader
					} else {
						sr.material = mats [1];
						mats [1].SetFloat ("_bwBlend", 1f);
					}
				
				// Default shader
				} else {
					sr.material = mats [0];
					sr.color = new Color (1, 1, 1);
				}
			}
		}
    }
}
