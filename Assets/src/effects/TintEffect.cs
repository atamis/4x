using UnityEngine;
using System.Collections;

namespace game.effects {
	class TintEffect : MonoBehaviour {
		private Material mat;
		private Color color;

		void OnAwake() {
			color = new Color (1, 1, 1);
			mat = new Material(Shader.Find("Custom/TintShader"));
			mat.SetColor ("_Color", color);
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			mat.SetColor ("_Color", color);
			Graphics.Blit (source, dest, mat);
		}
	}
}
