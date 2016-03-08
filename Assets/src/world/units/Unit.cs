using UnityEngine;
using game.ui;
using game.actor;

namespace game.world.units {

	class Unit : MonoBehaviour {
		UnitModel model;
		WorldMap w;
		Hex _h;

		public int actions { get; set; }
		public int maxActions = 4;

		public Hex h {
			get {
				return _h;
			}
			set {
				if (_h != null) {
					_h.unit = null;
				}
				this._h = value;
				if (_h != null) {
					transform.parent = _h.gameObject.transform;
					_h.unit = this;
				}
			}
		}

		public void init(WorldMap w, Hex h) {
			this.h = h;

			var obj = new GameObject ("Unit Model");
			obj.transform.parent = transform;

			model = obj.AddComponent<UnitModel> ();
			model.init (this);

			this.actions = 4;
		}

		public virtual void PreTurn(Actor old, Actor cur) {

		}

		public virtual void PostTurn(Actor old, Actor cur) {

		}

		public void NewTurn(Actor old, Actor cur) {
			actions = maxActions;
			Debug.Log ("Reset Unit Actions");
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

			public void init(Unit u) {
				transform.localPosition = new Vector3 (0, 0, Layer.Units);

				sp = gameObject.AddComponent<SpriteRenderer> ();
				sp.sprite = Resources.Load<Sprite> ("Textures/T_Unit");
			}

			void Update() {

			}
		}
	}
}