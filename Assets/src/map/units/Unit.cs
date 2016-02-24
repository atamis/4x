using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.map.units {
    class Unit : MonoBehaviour {
        Hex _h;

        // Uses Unity transform parenting to move itself around.
        public Hex h {
            get {
                return _h;
            }
            set {
                if (_h != null) {
                    _h.units.Remove(this);
                }
                this._h = value;
                transform.parent = _h.gameObject.transform;
                _h.units.Add(this);
            }
        }
        WorldMap w;
        UnitModel model;

        public void init(WorldMap w, Hex h) {
            this.w = w;

            this.h = h;
            h.units.Add(this);

            transform.localPosition = new Vector3(0, 0, 0);

            var obj = new GameObject("Unit Model");
            obj.transform.parent = transform;
            // z = -1 to it's for sure in front of the map.
            obj.transform.localPosition = new Vector3(0, 0, -1);
            model = obj.AddComponent<UnitModel>();
        }

        void Start() {
            
        }

        void Update() {
            // Looks like changing Unity transform doesn't
            // update child objects, so we have to always make sure
            // we're at (0, 0);
            transform.localPosition = new Vector3(0, 0, 0);
        }

        private class UnitModel : MonoBehaviour {
            SpriteRenderer sp;

            void Start() {
                sp = gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/Triangle");
            }

            void Update() {

            }
        }
    }
}
