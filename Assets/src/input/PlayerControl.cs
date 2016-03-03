using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor;
using game.map.units;
using game.map;


namespace game.input {
    class PlayerControl : MonoBehaviour {
        public Player p;
        MapClick mc;
        private bool buildMenu;

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
            GUILayout.BeginArea(new Rect (Screen.width * .3f, Screen.height*.8f, Screen.width/2, Screen.height * .9f));
            GUILayout.BeginHorizontal ();


            if (GUILayout.Button ("Move", GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .15f))) {
                print ("moved");
            }

            if (GUILayout.Button ("Build", GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .15f))) {
                if(buildMenu) buildMenu = false;
                else buildMenu = true;
                print ("Opened Menu");
            }

            if (GUILayout.Button ("Scan", GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .15f))) {
                if(mc.getSelected() != null){
                    Unit u = mc.getSelected().units.First ();
                    p.AddCommand(new ScanCommand(p, u, u.h));
                    print ("Scanned");
                }
            }

            if (GUILayout.Button ("Cleanse", GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .15f))) {
                print ("Cleansed");
            }

            if (GUILayout.Button("End Turn", GUILayout.Width(Screen.width * .12f), GUILayout.Height(Screen.height * .15f))) {
                p.AddCommand(new EndTurnCommand(p));
            }
            GUILayout.EndHorizontal ();
            GUILayout.EndArea ();

            //If the build menu is open
            if(buildMenu == true){
                GUILayout.BeginArea(new Rect (Screen.width * .3f, Screen.height*.7f, Screen.width/2, Screen.height * .9f));
                GUILayout.BeginHorizontal ();

                if (GUILayout.Button("Cond.",  GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Harv.", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Tow.", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Warp", GUILayout.Width(Screen.width * .035f), GUILayout.Height(Screen.height * .08f))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                GUILayout.EndHorizontal ();
                GUILayout.EndArea ();
            }
        }

    }
}
