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
		Conduit, Harvester, WarpGate, Purifier
	}

    class Building : MonoBehaviour {
		protected BuildingModel model;
		public Hex h {
			get;
			internal set;
		}
		public Actor a;
		public PowerNetwork pn;

		public bool disabled { get; set; }
		public bool grided;

		public virtual void init(Actor a, Hex h) {
			this.a = a;

            if (h.building != null) {
                // TODO: cleanup may be necessary.
                throw new Exception("There is already a building there.");
            }

            h.building = this;

            this.h = h;

			transform.parent = h.gameObject.transform;
			transform.localPosition = new Vector3 (0, 0, 0);

			AddModel ();

			disabled = false;
		}

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
			return h.powered;
		}

		public virtual bool ProjectsPower() {
			return false;
		}

		public virtual bool VisuallyConnects() {
			return false;
		}

		public void Die() {
			Destroy (model);
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

		}

		public virtual void PostTurn(Actor old, Actor cur) {

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
