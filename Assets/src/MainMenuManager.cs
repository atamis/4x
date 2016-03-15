using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game {
    class MainMenuManager : MonoBehaviour {

        AudioSource au;

        private enum MenuState {
            Main, Credits, Instructions
        }

        private MenuState state;

        void Start() {
            state = MenuState.Main;

            au = gameObject.AddComponent<AudioSource>();
            au.clip = Resources.Load<AudioClip>("Audio/Music/spacemanSpiff");
            au.Play();

            au.volume = 0.7f;
        }

        void OnGUI() {
            GUIStyle style = GUI.skin.box;
            style.alignment = TextAnchor.MiddleCenter;

            Rect rect = new Rect(0, 0, 200, 300);
            rect.center = new Vector2(Screen.width / 2, Screen.height / 2);


            switch (state) {
                case MenuState.Main:

                    GUILayout.BeginArea(rect, style);

                    GUILayout.Box("Team Yog-Sothoth Presents:\n\n4X Game");

                    if (GUILayout.Button("Start Game")) {
                        SceneManager.LoadSceneAsync("GameLoad");
                    }

                    if (GUILayout.Button("Instructions")) {
                        state = MenuState.Instructions;
                        return;
                    }

                    if (GUILayout.Button("Credits")) {
                        state = MenuState.Credits;
                        return;
                    }

                    if (GUILayout.Button("Quit")) {
                        Application.Quit();
                    }

                    GUILayout.EndArea();

                    state = MenuState.Main;
                    break;
                case MenuState.Credits:
                    GUILayout.BeginArea(rect, style);

                    GUILayout.Box("Lead Designer Nick Care\nLead Programmer Andrew Amis\nSound Designer Kirk Pearson\n Robert \"of all trades\" Tomcik");
                    if (GUILayout.Button("Back")) {
                        state = MenuState.Main;
                    }

                    GUILayout.EndArea();
                    break;
                case MenuState.Instructions:
                    print("Instructions clicked");
                    break;
            }
        }
        
    }
}
