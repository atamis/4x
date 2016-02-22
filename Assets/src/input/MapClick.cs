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
                print("you just clicked at " + mouseX + " " + mouseY);
                Vector2 v = new Vector2(mouseX, mouseY);
                Hex h = w.map[w.l.PixelHex(v)];
                handleClick(v, h);
            }
        }

        private void handleClick(Vector2 vec, Hex h) {
            if (h.b == Biome.Impassable) {
                h.b = Biome.Passable;
            } else {
                h.b = Biome.Impassable;
            }

        }
    }
}
