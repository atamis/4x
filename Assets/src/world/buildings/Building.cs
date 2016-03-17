using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.ui;
using game.actor;

namespace game.world.buildings {
    enum BuildingState {
        Inactive, Active, Warping,
    }
    
	public enum BuildingType {
        // 5 over 1 turn.
		Conduit,
        // 24 over 3 turns
        Harvester,
        // 150 over 10 turns.
        WarpGate,
        // 40 over 4 turns.
        Purifier
	}

    public static class BuildingTypeExtensions {
        public static int BuildPerTurn(this BuildingType b) {
            switch(b) {
                case BuildingType.Conduit:
                    return 5;
                case BuildingType.Harvester:
                    return 8;
                case BuildingType.WarpGate:
                    return 15;
                case BuildingType.Purifier:
                    return 10;
                default:
                    return 5;
            }
        }

        public static int BuildTotal(this BuildingType b) {
            switch (b) {
                case BuildingType.Conduit:
                    return 5;
                case BuildingType.Harvester:
                    return 24;
                case BuildingType.WarpGate:
                    return 120;
                case BuildingType.Purifier:
                    return 40;
                default:
                    return 5;
            }
        }

        public static int PowerGen(this BuildingType b) {
            switch(b) {
                case BuildingType.Harvester:
                    return 7;
                case BuildingType.WarpGate:
                    return 2;
                default:
                    return 0;
            }
        }

        public static int PowerDrain(this BuildingType b) {
            switch (b) {
                case BuildingType.Purifier:
                    return 5;
                default:
                    return 0;
            }
        }
    }


    abstract class Building : MonoBehaviour {

        protected BuildingModel model;
        private Hex _h;
		public Hex h {
			get {
                return _h;
            }
			internal set {
                if (value != null && value.building != null) {
                    throw new Exception("There is already a building there");
                }

                if (value == null) {
                    _h.building = null;
                    _h = null;
                } else {

                    value.building = this;
                    _h = value;
                }
            }
		}
		public Actor a;
		public PowerNetwork pn;

		public bool disabled { get; set; }
		public bool grided;
        private bool powerDrainSuccessful;

		public virtual void init(Actor a, Hex h) {
			this.a = a;

            if (h.building != null) {
                // TODO: cleanup may be necessary.
                throw new Exception("There is already a building there.");
            }
            
            this.h = h;

			transform.parent = h.gameObject.transform;
			transform.localPosition = new Vector3 (0, 0, 0);

			AddModel ();

			disabled = false;
            powerDrainSuccessful = false;
		}

        public abstract BuildingType? getBuildingType();

		internal virtual void AddModel() {
			model = new GameObject ("Building Model").AddComponent<BuildingModel> ();
			model.init (this);
		}

		public virtual string GetName() {
			return "Building";
		}

		public virtual string GetTooltip() {
			return GetName ();
		}

		public virtual bool Powered() {
			return h.powered && powerDrainSuccessful;
		}

		public virtual bool ProjectsPower() {
			return false;
		}

		public virtual bool VisuallyConnects() {
			return false;
		}

		public void Die() {
			Destroy (model.gameObject);
			h = null;
			Destroy (gameObject);
		}

		public virtual void SpreadPower(PowerNetwork pn) {
			this.grided = true;

			if (this.pn == null) {
				this.pn = pn;
			}

			if (Powered() && ProjectsPower()) {
				HashSet<Hex> hexes = h.Neighbors().Aggregate(new HashSet<Hex>(),
					(HashSet<Hex> acc, Hex hs) => {
						acc.Add(h);
						foreach (Hex n in hs.Neighbors()) {
							acc.Add(n);
						}
						return acc;
					});

				foreach(Hex hs in hexes) {
					hs.pn = pn;
				}

				foreach(Hex hs in hexes) {
					if (hs.building != null && !hs.building.grided) {
						if (VisuallyConnects() && hs.building.VisuallyConnects()) {
							h.wm.bm.Add(h.loc, hs.loc);
						}

						hs.building.SpreadPower(pn);
					}
				}
			}
		}

		public virtual void PreTurn(Actor old, Actor cur) {

		}

		public virtual void NewTurn(Actor old, Actor cur) {
            if (pn != null && getBuildingType().HasValue) {
                pn.power += getBuildingType().Value.PowerGen();
            }
        }

        public virtual void PostTurn(Actor old, Actor cur) {
            powerDrainSuccessful = false;

            if (pn != null) {
                if (getBuildingType().HasValue) {
                    var drain = getBuildingType().Value.PowerDrain();

                    if (drain == 0) {
                        powerDrainSuccessful = true;
                        return;
                    }

                    if (pn.power > drain) {
                        pn.power -= drain;
                        powerDrainSuccessful = true;
                    }
                }
            }
        }

        public class BuildingModel : MonoBehaviour {
			public SpriteRenderer sp;
			Building b;
			Material mat;

			public virtual void init(Building b) {
				this.b = b;

				transform.parent = b.gameObject.transform;
				transform.localPosition = new Vector3 (0, 0, Layer.Buildings);

				sp = gameObject.AddComponent<SpriteRenderer> ();

				//mat = new Material (Shader.Find ("Custom/CRTShader"));
				//sp.material = mat;

				sp.sprite = Resources.Load<Sprite> ("Textures/T_" + b.GetName ());
            }

			void Update() {
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
					//mat.SetFloat ("_bwBlend", 0.7f);
				} else {
					//mat.SetFloat ("_bwBlend", 0f);
				}
			}
        }
    }
}
