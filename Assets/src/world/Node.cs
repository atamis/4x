using UnityEngine;
using game.ui;
using System.Collections;

namespace game.world {
    class Node : MonoBehaviour {
        private NodeModel model;
		public Hex h;
		public bool visible = false;

        public void init(Hex parent) {
			this.h = parent;

            var obj = new GameObject("Node Model");
            obj.transform.parent = parent.transform;
			obj.transform.localPosition = new Vector2 (0, 0);

            model = obj.AddComponent<NodeModel>();
            model.init(this);
        }
			
        public void setVisible() {
			this.visible = true;
		}
			
        void Start() {
        }

        void Update() {
        }

		private class NodeModel : MonoBehaviour {
			public SpriteRenderer sp;
			private float clock;
			Node n;

			public void init(Node n) {
				this.n = n;
				sp = this.gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/node");
				sp.color = new Color (0.5f, 1, 1);
				this.transform.localPosition = new Vector3 (0, 0, Layer.Buildings);

				sp.enabled = false;
				clock = 0f;
			}

			void Update() {
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
					if (this.n.visible) {
						sp.enabled = true;
					}
					clock += 0.05f;
					transform.localScale = new Vector3 (2 + Mathf.Sin (clock) / 3, 2 + Mathf.Sin (clock) / 3, 1);
					transform.eulerAngles = new Vector3 (0, 0, 7 * clock);
				} else {
					sp.enabled = false;
				}
			}
		}

    }
}
