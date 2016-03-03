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
            GUILayout.BeginArea(new Rect (Screen.width/4, Screen.height*.8f, Screen.width/2, Screen.height * .9f));
            GUILayout.BeginHorizontal ();


            if (GUILayout.Button ("Move", GUILayout.Width(100), GUILayout.Height(100))) {
                print ("moved");
            }

            if (GUILayout.Button ("Build", GUILayout.Width(100), GUILayout.Height(100))) {
                if(buildMenu) buildMenu = false;
                else buildMenu = true;
                print ("Opened Menu");
            }

            if (GUILayout.Button ("Scan", GUILayout.Width(100), GUILayout.Height(100))) {
                if(mc.getSelected() != null){
                    Unit u = mc.getSelected().units.First ();
                    p.AddCommand(new ScanCommand(p, u, u.h));
                    print ("Scanned");
                }
            }

            if (GUILayout.Button ("Cleanse", GUILayout.Width(100), GUILayout.Height(100))) {
                print ("Cleansed");
            }

            if (GUILayout.Button("End Turn", GUILayout.Width(140), GUILayout.Height(100))) {
                p.AddCommand(new EndTurnCommand(p));
            }
            GUILayout.EndHorizontal ();
            GUILayout.EndArea ();

            //If the build menu is open
            if(buildMenu == true){
                GUILayout.BeginArea(new Rect (Screen.width/4, Screen.height*.7f, Screen.width/2, Screen.height * .9f));
                GUILayout.BeginHorizontal ();

                if (GUILayout.Button("Cond.", GUILayout.Width(45), GUILayout.Height(45))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Harv.", GUILayout.Width(45), GUILayout.Height(45))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Tow.", GUILayout.Width(45), GUILayout.Height(45))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                if (GUILayout.Button("Warp", GUILayout.Width(45), GUILayout.Height(45))) {
                    p.AddCommand(new EndTurnCommand(p));
                }

                GUILayout.EndHorizontal ();
                GUILayout.EndArea ();
            }
        }

    }
}
