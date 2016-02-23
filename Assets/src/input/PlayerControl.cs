using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor;

namespace game.input {
    class PlayerControl : MonoBehaviour {
        public Player p;

        public void init(Player p) {
            this.p = p;
        }

        void Start() {
            
        }

        void Update() {

        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 20), "End Turn")) {
                p.AddCommand(new EndTurnCommand(p));
            }
        }

    }
}
