using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using game.world.buildings;
using game.math;

namespace game.ui {
	class TooltipUI : MonoBehaviour {
		UIManager um;

		public void init(UIManager um) {
			this.um = um;
		}

		void OnGUI() {
			Hex h = um.h_target;
            if (h != null) {
                var mp = Input.mousePosition;
                mp.y = Screen.height - mp.y;
                mp = mp + new Vector3(10, 10, 0);

                var style = GUI.skin.box;
                style.alignment = TextAnchor.UpperLeft;
                //GUI.backgroundColor = new Color(0, 0, 0, 1);

                Vector2 lefthandCorner = new Vector2(Screen.width * 0.76f, Screen.height * 0.8f);
                Vector2 bigBox = new Vector2(Screen.width * 0.2f, Screen.height * 0.14f);
                var littleBox = bigBox / 2;

                GUI.Box(new Rect(lefthandCorner, bigBox), "");
                if (h.units.Count > 0) {
                    GUI.Label(new Rect(lefthandCorner, littleBox),
                        "Units: " + h.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
                }

                GUI.Label(new Rect(lefthandCorner + new Vector2(littleBox.x, 0), littleBox), "Biome: " + h.b.ToString() + "\n" +
                        "Energy: " + (h.scanned ? h.ev.ToString() : "??") + "\nPassable: " + h.b.Passable());

                if (h.building != null) {
                    GUI.Label(new Rect(lefthandCorner + new Vector2(0, littleBox.y), littleBox), "Building: " + h.building.GetTooltip());
                }

                if (h.miasma != null) {
                    GUI.Label(new Rect(lefthandCorner + littleBox, littleBox), "Miasma: level " + h.miasma.level);
                }

                /*GUILayout.BeginArea(new Rect(new Vector2(Screen.width * 0.76f, Screen.height * 0.8f), new Vector2 (240, 150)));
				GUILayout.Box (h.ToString ());
                
                if (h.miasma != null) {
                    GUILayout.Box("Miasma: level " + h.miasma.level);
                }

				GUILayout.Box("Energy: " + (h.scanned ? h.ev.ToString() : "??"));

				if (h.pn != null) {
					var pn = h.pn;
					GUILayout.Box("Power: " + pn.power + " (" + pn.id + ")");
				}

				

				if (h.units.Count > 0) {
					GUILayout.Box("Units: " + h.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
				}
				GUILayout.EndArea ();*/
            }
        }
	}
}