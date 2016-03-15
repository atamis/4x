using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game {
    class VictoryScreen : MonoBehaviour {
        public void OnGUI() {
            Rect loc = new Rect(0, 0, 100, 100);
            
            loc.center = new Vector2(Screen.width, Screen.height) / 2;

            GUILayout.BeginArea(loc);

            GUILayout.Box("You win");
            if (GUILayout.Button("Main Menu")) {
                SceneManager.LoadSceneAsync("MainMenu");
            }

            GUILayout.EndArea();
        }
    }
}
