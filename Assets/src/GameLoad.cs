using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.src {
    class GameLoad : MonoBehaviour {
        void OnGUI() {
            GUIStyle style = GUI.skin.box;
            style.alignment = TextAnchor.MiddleCenter;

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), style);

            GUILayout.Box("Game Loading");

            GUILayout.EndArea();

            SceneManager.LoadScene("4x");
        }
    }
}
