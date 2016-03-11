using UnityEngine;


namespace game.effects {
	class BlurEffect : MonoBehaviour {
		public Material mat;
		public float intensity = 1;

		void Awake() {
			mat = new Material (Shader.Find("Custom/BlurShader"));
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			if (intensity == 0) {
				Graphics.Blit (source, dest);
			}
			Graphics.Blit (source, dest, mat);
		}
	}
}

