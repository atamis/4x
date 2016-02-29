using UnityEngine;
using game.map;
using System.Collections;

namespace game.world {
	class Node : MonoBehaviour {

		Hex parent;
		NodeModel model;

		public void init(Hex parent) {
			this.parent = parent;

			var modelObj = new GameObject ("Node Model");
			modelObj.transform.parent = parent.transform;
			modelObj.transform.localPosition = new Vector2 (0, 0);

			model = modelObj.AddComponent<NodeModel> ();
			model.init (this);
		}

		public void setVisible() {
			this.model.sp.enabled = true;
		}

		private class NodeModel : MonoBehaviour {
			public Node parent;
			public SpriteRenderer sp;
			private float clock;

			public void init(Node n) {
				this.parent = n;

				sp = gameObject.AddComponent<SpriteRenderer>();

				sp.sprite = Resources.Load<Sprite>("Textures/node");
				sp.color = new Color (.5f, 1, 1);
				sp.transform.localScale = new Vector3(1.9f, 1.9f);

				sp.sortingOrder = 2; // game layer

				sp.enabled = false;
				clock = 0f;
			}

			void Update() {
				clock += 0.05f;
				transform.localScale = new Vector3(2+Mathf.Sin(clock)/3, 2+Mathf.Sin(clock)/3,1);
				transform.eulerAngles = new Vector3(0,0,7*clock);
			}
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}
	}
}
