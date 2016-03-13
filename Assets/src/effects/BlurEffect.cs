using UnityEngine;

namespace game.effects {
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Blur")]
	class BlurEffect : MonoBehaviour {
		public float intensity = 0.0075f;
		Material mat;

		void Awake() {
			mat = new Material (Shader.Find("Custom/BlurShader"));
			mat.hideFlags = HideFlags.HideAndDontSave;
			mat.SetFloat("_intensity", intensity);
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			mat.SetFloat("_intensity", intensity);
			Graphics.Blit (source, dest, mat);
		}
	}
}
