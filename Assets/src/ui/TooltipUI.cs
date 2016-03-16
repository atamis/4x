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
        private static Texture2D iconUnit = Resources.Load<Texture2D>("Textures/T_Icon_Unit");
        private static Texture2D iconBiome = Resources.Load<Texture2D>("Textures/T_Icon_Biome");
        private static Texture2D iconBuilding = Resources.Load<Texture2D>("Textures/T_Icon_Building");
        private static Texture2D iconMiasma = Resources.Load<Texture2D>("Textures/T_Icon_Miasma");

        UIManager um;

		public void init(UIManager um) {
			this.um = um;
		}

		void OnGUI() {
			Hex h = um.h_target;
            if (h != null) {

                Vector2 lefthandCorner = new Vector2(Screen.width * 0.76f, Screen.height * 0.8f);
                Vector2 bigBox = new Vector2(Screen.width * 0.2f, Screen.height * 0.14f);
                var littleBox = bigBox / 2;
                var icon = new Vector2(littleBox.y, littleBox.y) * 0.9f;
                var textBox = littleBox;
                textBox.x -= icon.x;

                var unitStart = lefthandCorner;
                var unitText = new Vector2(unitStart.x + icon.x, unitStart.y);

                var biomeStart = lefthandCorner + new Vector2(littleBox.x, 0);
                var biomeText = new Vector2(biomeStart.x + icon.x, biomeStart.y);

                var buildingStart = lefthandCorner + new Vector2(0, littleBox.y);
                var buildingText = new Vector2(buildingStart.x + icon.x, buildingStart.y);

                var miasmaStart = lefthandCorner + littleBox;
                var miasmaText = new Vector2(miasmaStart.x + icon.x, miasmaStart.y);

                GUI.Box(new Rect(lefthandCorner, bigBox), "");

                
                GUI.DrawTexture(new Rect(unitStart, icon), iconUnit);
                if (h.units.Count > 0) {
                    GUI.Label(new Rect(unitText, textBox),
                        "Units: " + h.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
                }

                GUI.DrawTexture(new Rect(biomeStart, icon), iconBiome);
                GUI.Label(new Rect(biomeText, textBox), "Biome: " + h.b.ToString() + "\n" +
                        "Energy: " + (h.scanned ? h.ev.ToString() : "??") + "\nPassable: " + h.b.Passable());

                GUI.DrawTexture(new Rect(buildingStart, icon), iconBuilding);
                if (h.building != null) {
                    GUI.Label(new Rect(buildingText, textBox), "Building: " + h.building.GetTooltip());
                }

                GUI.DrawTexture(new Rect(miasmaStart, icon), iconMiasma);
                if (h.miasma != null) {
                    GUI.Label(new Rect(miasmaText, textBox), "Miasma: level " + h.miasma.level);
                }
            }
        }
	}
}