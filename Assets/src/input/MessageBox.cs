using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.input {
    public class MessageBox : MonoBehaviour {
        private static Texture2D tex = Resources.Load<Texture2D>("Textures/Helper/T_HelperL1");

        private LinkedList<string> messages;

        public void init() {
            messages = new LinkedList<string>();

            AddMessage("Team Yog-Sothoth welcomes you.");
            AddMessage("Enjoy the game.");
        }

        public void AddMessage(string msg) {
            messages.AddFirst(msg);
        }

        void Update() {
        }

        void OnGUI() {
            var m = messages.Take(6).Reverse().Aggregate<string>((acc, msg) => acc + "\n" + msg);
            GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f - tex.height, tex.width, tex.height), tex);
            GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f, 250, 100), m);
        }
        
    }
}
