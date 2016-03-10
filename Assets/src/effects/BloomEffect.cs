using UnityEngine;

namespace game.effects {
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Bloom")]
	class BloomEffect : MonoBehaviour {
		public float intensity = 0.5f;
		Material mat;
			
		void Awake() {
			mat = new Material (Shader.Find ("Custom/BloomShader"));
			mat.SetFloat ("_Strength", intensity);
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			if (intensity == 0) {
				Graphics.Blit (source, dest);
			}
			mat.SetFloat ("_Strength", intensity);
			Graphics.Blit (source, dest, mat);
		}
	}
}
