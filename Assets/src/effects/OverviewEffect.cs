using UnityEngine;
using System.Collections;

namespace game.effects {
	[ExecuteInEditMode]
	class OverviewEffect : MonoBehaviour {
		private Material mat;

		void Awake() {
			mat = new Material(Shader.Find("Custom/WorldShader"));
			mat.color = new Color (0, 1, 1);
			//mat.SetColor ("_Color", color);
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			//mat.SetColor ("_Color", color);
			Graphics.Blit (source, dest, mat);
		}
	}
}
