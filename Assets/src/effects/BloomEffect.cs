using UnityEngine;

namespace game.effects {
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Bloom")]
	class BloomEffect : MonoBehaviour {
		public float intensity = 0.0075f;
		public float bloom = 0.5f;
		Material mat;
			
		void Awake() {
			mat = new Material (Shader.Find ("Custom/BloomShader"));
			mat.hideFlags = HideFlags.HideAndDontSave;
			mat.SetFloat ("_intensity", intensity);
			mat.SetFloat ("_bloom", bloom);
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			mat.SetFloat ("_intensity", intensity);
			mat.SetFloat ("_bloom", bloom);
			Graphics.Blit (source, dest, mat);
		}
	}
}
