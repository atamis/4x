using UnityEngine;
using game.ui;
using game.actor;
using System;

namespace game.world.units {

	class Unit : MonoBehaviour {
        public AudioSource au;
		UnitModel model;
		WorldMap w;
        public Actor actor;
		Hex _h;

		public int actions { get; set; }
		public int maxActions = 50;

		public Hex h {
			get {
				return _h;
			}
			set {
				if (_h != null) {
					_h.units.Remove(this);
				}
				this._h = value;
				if (_h != null) {
					transform.parent = _h.gameObject.transform;
					_h.units.Add(this);
				}
			}
		}

		public void init(Actor actor, WorldMap w, Hex h) {
			this.h = h;
            this.actor = actor;

			var obj = new GameObject ("Unit Model");
			obj.transform.parent = transform;

			model = obj.AddComponent<UnitModel> ();
            au = obj.AddComponent<AudioSource>();
            au.spatialBlend = 1.0f;
			model.init (this);

			this.actions = 4;
			h.reveal();
			foreach (Hex h2 in h.Neighbors()) {
				h2.reveal();
			}
		}

        public override string ToString() {
            return String.Format("[{0} actions:{1}]", name, actions);
        }


        public virtual void PreTurn(Actor old, Actor cur) {

		}

		public void NewTurn(Actor old, Actor cur) {
			actions = maxActions;
		}

		public virtual void PostTurn(Actor old, Actor cur) {
		}

		void Update() {
			transform.localPosition = new Vector3 (0, 0, 0);
		}

		public void Die() {
			Destroy (model);
			h = null;
			Destroy (gameObject);
		}

		private class UnitModel : MonoBehaviour {
			SpriteRenderer sp;
			Material mat;

			public void init(Unit u) {
				transform.localPosition = new Vector3 (0, 0, Layer.Units);

				sp = gameObject.AddComponent<SpriteRenderer> ();
				sp.sprite = Resources.Load<Sprite> ("Textures/T_Unit");
			
				//mat = new Material (Shader.Find ("Custom/BWShader"));
				//sp.material = mat;
			}

			void Update() {
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
					//mat.SetFloat ("bwIntensity", 0.7f);
				} else {
					//mat.SetFloat ("bwIntensity", 0f);
				}
			}
		}
	}
}
