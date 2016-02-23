using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map;

namespace game.input {
    class MapClick : MonoBehaviour {
        WorldMap w;

        public void init(WorldMap w) {
            this.w = w;
        }

        void Start() {

        }

        void Update() {
            if (Input.GetMouseButtonUp(0)) {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float mouseX = worldPos.x;
                float mouseY = worldPos.y;
                Vector2 v = new Vector2(mouseX, mouseY);
                HexLoc l = w.l.PixelHex(v);
                if (w.map.ContainsKey(l)) {
                    Hex h = w.map[l];
                    print("You selected " + h);
                    handleClick(v, h);
                }
            }
        }

        private void handleClick(Vector2 vec, Hex h) {
            h.b = h.b.Toggle();
            foreach (Hex n in h.Neighbors()) {
                n.b = n.b.Toggle();
            }

        }
    }
}
