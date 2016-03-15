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
			Hex h = um.GetHexAtMouse ();
			if (h != null) {
				var mp = Input.mousePosition;
				mp.y = Screen.height - mp.y;
				mp = mp + new Vector3 (10, 10, 0);

				var style = GUI.skin.box;
				style.alignment = TextAnchor.UpperLeft;
				GUI.backgroundColor = new Color(0, 0, 0, 1);

				GUILayout.BeginArea (new Rect (mp, new Vector2 (250, 150)));
				GUILayout.Box (h.ToString ());
                
                if (h.miasma != null) {
                    GUILayout.Box("Miasma: level " + h.miasma.level);
                }

				GUILayout.Box("Energy: " + (h.scanned ? h.ev.ToString() : "??"));

				if (h.pn != null) {
					var pn = h.pn;
					GUILayout.Box("Power: " + pn.power + " (" + pn.id + ")");
				}

				if (h.building != null) {

					GUILayout.Box("Building: " + h.building.GetTooltip());

                }

				if (h.units.Count > 0) {
					GUILayout.Box("Units: " + h.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
				}
				GUILayout.EndArea ();
			}
		}
	}
}