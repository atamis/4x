using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map;
using game.map.units;

namespace game.input {
    class TooltipUI : MonoBehaviour {
        WorldMap w;

        public void init(WorldMap w) {
            this.w = w;
        }


        void Start() {
        }

        void Update() {

        }

        private Hex GetHexAtMouse() {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HexLoc l = w.l.PixelHex(worldPos);
            if (w.map.ContainsKey(l)) {
                Hex h = w.map[l];
                return h;
            }
            return null;
        }

        void OnGUI() {
            Hex h = GetHexAtMouse();

            if (h != null) {

                var mp = Input.mousePosition;
                mp.y = Screen.height - mp.y;
                mp = mp + new Vector3(10, 10, 0);

                var style = GUI.skin.box;
                style.alignment = TextAnchor.UpperLeft;
                GUI.backgroundColor = new Color(0, 0, 0, 1);

                GUILayout.BeginArea(new Rect(mp, new Vector2(250, 150)));

                GUILayout.Box(h.ToString());
                GUILayout.Box((h.corrupted ? "Corrupted " : "") + h.b.ToString());

                
                GUILayout.Box("Energy: " + (h.scanned ? h.ev.ToString() : "??"));

                if (h.pn != null) {
                    var pn = h.pn;
                    GUILayout.Box("Power: " + pn.power + " (" + pn.id + ")");
                }

                if (h.building != null) {
                    StringBuilder b = new StringBuilder("Building: ");
                    if (h.building.GetType() == typeof(WarpingBuilding)) {
                        var w = (WarpingBuilding) h.building;
                        b.Append("warping ");
                        b.Append(w.type.ToString());
                        b.Append(", ");
                        b.Append(w.power);
                        b.Append("/");
                        b.Append(w.required);

                    } else {
                        b.Append(h.building.GetName());
                    }

                    GUILayout.Box("Building: " + h.building.GetTooltip());
                }

                if (h.units.Count > 0) {
                    GUILayout.Box("Units: " +
                        h.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
                }


                GUILayout.EndArea();
            }
            //GUI.Box(new Rect(mp, new Vector2(100, 100)), message, boxStyle);
        }

        private Texture2D makeBlankTex() {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, new Color(0, 0, 0, 0.6f));
            tex.Apply();
            return tex;
        }
    }
}
