using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.effects;

namespace game.ui {
    class MinimapManager : MonoBehaviour {
        RenderTexture tex;
        Camera cam;

        void Start() {
            tex = new RenderTexture(100, 100, 24);

            transform.position = new Vector3(55, 48, -20);

            cam = new GameObject("Minimap Camera").AddComponent<Camera>();
            cam.orthographic = true;
            cam.targetTexture = tex;
            cam.transform.position = new Vector3(26, 23, -20);
            cam.orthographicSize = 30;
            cam.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
			cam.gameObject.AddComponent<BloomEffect> ();
        }

        void Update() {

        }
        
        void OnGUI() {
            GUI.Box(new Rect(0, 0, 100, 100), tex);
        }
    }
}
