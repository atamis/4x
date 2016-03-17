using game.actor;
using game.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.world {
    class UnitPresent : Present {
        protected override void enactNewWorldOrder(Actor a) {
            print(h);
            h.wm.makeUnit(a, h.loc);
        }
    }

    class RevealPresent : Present {
        protected override void enactNewWorldOrder(Actor a) {
            foreach (KeyValuePair<HexLoc, Hex> kv in h.wm.map) {
                if (kv.Key.Distance(h.loc) < 4) {
                    kv.Value.reveal();
                    kv.Value.scan();
                }
            }
        }
    }

    abstract class Present : MonoBehaviour {
        private Hex _h;
        public Hex h {
            get {
                return _h;
            }
            set {
                if (value != null) {
                    value.present = this;
                    transform.parent = value.transform;
                    transform.localPosition = Vector3.zero;
                } else {
                    _h.present = null;
                }
                this._h = value;
            }
        }

        private PresentModel model;

        public void init(Hex h) {
            this.h = h;
            model = new GameObject("Present Model").AddComponent<PresentModel>();
            model.init(this);
        }

        public void Trigger(Actor a) {
            print("Triggered");
            enactNewWorldOrder(a);
            h = null;
            Destroy(gameObject);
        }

        protected abstract void enactNewWorldOrder(Actor a);

        private class PresentModel : MonoBehaviour {
            private static Sprite sprite = Resources.Load<Sprite>("Textures/T_Pod");

            Present p;
            SpriteRenderer sp;

            internal void init(Present p) {
                this.p = p;

                transform.parent = p.transform;

                sp = gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = sprite;
            }

            void Update() {
                if (p.h.revealed) {
                    sp.enabled = true;
                } else {
                    sp.enabled = false;
                }

                transform.localPosition = LayerV.Buildings;
            }
        }
    }
}
