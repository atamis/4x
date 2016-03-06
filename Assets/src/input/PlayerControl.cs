using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor;
using game.map.units;
using game.map;
using game.input;


namespace game.input {
    class PlayerControl : MonoBehaviour {
        public Player p;
        MapClick mc;
        private bool buildMenu;

        private static Texture2D UI_Move = Resources.Load<Texture2D>("Textures/T_UI_Move");
        private static Texture2D UI_Scan = Resources.Load<Texture2D>("Textures/T_UI_Scan");
        private static Texture2D UI_Build = Resources.Load<Texture2D>("Textures/T_UI_Build");
        private static Texture2D UI_Purify = Resources.Load<Texture2D>("Textures/T_UI_Purify");
        private static Texture2D UI_MoveH = Resources.Load<Texture2D>("Textures/T_UI_MoveH");
        private static Texture2D UI_ScanH = Resources.Load<Texture2D>("Textures/T_UI_ScanH");
        private static Texture2D UI_BuildH = Resources.Load<Texture2D>("Textures/T_UI_BuildH");
        private static Texture2D UI_PurifyH = Resources.Load<Texture2D>("Textures/T_UI_PurifyH");
        private static Texture2D UI_MoveC = Resources.Load<Texture2D>("Textures/T_UI_MoveC");
        private static Texture2D UI_ScanC = Resources.Load<Texture2D>("Textures/T_UI_ScanC");
        private static Texture2D UI_BuildC = Resources.Load<Texture2D>("Textures/T_UI_BuildC");
        private static Texture2D UI_PurifyC = Resources.Load<Texture2D>("Textures/T_UI_PurifyC");
        private static Texture2D UI_End = Resources.Load<Texture2D>("Textures/T_UI_End");
        private static Texture2D UI_EndH = Resources.Load<Texture2D>("Textures/T_UI_EndH");
        private static Texture2D UI_EndC = Resources.Load<Texture2D>("Textures/T_UI_EndC");

        private GUIStyle ButtonStyle;

        public void init(Player p, MapClick mc) {
            this.p = p;
            this.mc = mc;
            buildMenu = false;

        }

        void Start() {

        }

        void Update() {

        }

        void OnGUI() {
            //Hex selected = mc.getSelected();
            // for units
            GUILayout.BeginArea(new Rect (Screen.width * .3f, Screen.height*.8f,
                 Screen.width/2, Screen.height * .9f));
            GUILayout.BeginHorizontal ();

            // Move Button        
            ButtonStyle = new GUIStyle(GUI.skin.label);
            ButtonStyle.normal.background = UI_Move;
            ButtonStyle.hover.background = UI_MoveH;
            ButtonStyle.active.background = UI_MoveC;
            if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f),
                GUILayout.Height(Screen.height * .13f))){
                Hex h = mc.selected;
                if (mc.selected.units.Count() < 1) {
                    mc.s = MapClick.State.Selected;
                }else mc.s = MapClick.State.Moving;
                print ("Move");
            }

            // Build Button
            ButtonStyle.normal.background = UI_Build;
            ButtonStyle.hover.background = UI_BuildH;
            ButtonStyle.active.background = UI_BuildC;
            if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f),
                GUILayout.Height(Screen.height * .13f))){
                  if(buildMenu) buildMenu = false;
                else buildMenu = true;
                print ("Opened Menu");
            }

            // Scan Button
            ButtonStyle.normal.background = UI_Scan;
            ButtonStyle.hover.background = UI_ScanH;
            ButtonStyle.active.background = UI_ScanC;
            if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f),
                GUILayout.Height(Screen.height * .13f))){
                if(mc.getSelected() != null && mc.getSelected().units.Count > 0){
                    Unit u = mc.getSelected().units.First ();
                    p.AddCommand(new ScanCommand(p, u, u.h));
                    print ("Scanned");
                }
            }

            //Purify Button
            ButtonStyle.normal.background = UI_Purify;
            ButtonStyle.hover.background = UI_PurifyH;
            ButtonStyle.active.background = UI_PurifyC;
            if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f),
                GUILayout.Height(Screen.height * .13f))){
                if(mc.getSelected() != null && mc.getSelected().units.Count > 0){
                    Unit u = mc.getSelected().units.First ();
                    p.AddCommand(new PurifyCommand(p, u, u.h));
                    print ("Scanned");
                }
            }

            //End Turn Button
            ButtonStyle.normal.background = UI_End;
            ButtonStyle.hover.background = UI_EndH;
            ButtonStyle.active.background = UI_EndC;
            if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .12f),
                GUILayout.Height(Screen.height * .13f))){  
                p.AddCommand(new EndTurnCommand(p));
            }
            GUILayout.EndHorizontal ();
            GUILayout.EndArea ();

            //If the build menu is open
            if(buildMenu == true){
                GUILayout.BeginArea(new Rect (Screen.width * .3f, Screen.height*.7f, Screen.width/2, Screen.height * .9f));
                GUILayout.BeginHorizontal ();

                // Build conduit button
                if (GUILayout.Button("Cond.",  GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    if(mc.getSelected() != null){
                        Hex h = mc.getSelected();
                        p.AddCommand(new BuildBuildingsCommand(p, h, BuildingType.Conduit));
                        print ("Built Conduit");
                   }
                }

                // Build harvester button
                if (GUILayout.Button("Harv.", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    if(mc.getSelected() != null){
                        Hex h = mc.getSelected();
                        p.AddCommand(new BuildBuildingsCommand(p, h, BuildingType.Harvester));
                        print ("Built Harvester");
                   }
                }

                // Build Tower button
                if (GUILayout.Button("Tow.", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    // TODO once towers are implemented
                }

                // Build warp gate button
                if (GUILayout.Button("Warp", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    if(mc.getSelected() != null){
                        Hex h = mc.getSelected();
                        p.AddCommand(new BuildBuildingsCommand(p, h, BuildingType.WarpGate));
                        print ("Built Warp Gate");
                   }
                }

                GUILayout.EndHorizontal ();
                GUILayout.EndArea ();
            }
        }

    }
}
