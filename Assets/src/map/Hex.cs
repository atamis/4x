using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.map {
    class Hex : MonoBehaviour {
        WorldMap w;
        public HexLoc loc;

        HexModel model;

        public void init(WorldMap w, HexLoc loc) {
            this.w = w;
            this.loc = loc;

            transform.localPosition = w.l.HexPixel(loc);

            var o = new GameObject("Hex Model");
            o.transform.parent = transform;
            o.transform.localPosition = new Vector2(0, 0);
            model = o.AddComponent<HexModel>();
            model.init(this);
        }

        void Start() {

        }

        void Update() {

        }

        private class HexModel : MonoBehaviour {
            public Hex h;
            SpriteRenderer sp;

            public void init(Hex h) {
                this.h = h;

                sp = gameObject.AddComponent<SpriteRenderer>();

                sp.sprite = Resources.Load<Sprite>("Textures/Hexagon");
            }
        }
    }
}
