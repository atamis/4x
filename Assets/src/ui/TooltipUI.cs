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

				if (h.unit != null) {
					GUILayout.Box("Unit: " + h.unit.ToString());
				}
				GUILayout.EndArea ();
			}
		}
	}
}