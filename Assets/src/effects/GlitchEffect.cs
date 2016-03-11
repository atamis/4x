using UnityEngine;

namespace game.effects {
	[ExecuteInEditMode]
	class GlitchEffect : MonoBehaviour {
		public Texture2D dispmap = null;
		public Material mat;
		float glitchup, glitchdown, flicker;
		float uptime = 0.05f, downtime = 0.05f, ftime = 0.05f;
		public float intensity = 1;

		void Awake() {
			mat = new Material (Shader.Find("Custom/GlitchShader"));
		}

		void OnRenderImage(RenderTexture source, RenderTexture dest) {
			mat.SetFloat ("_intensity", intensity);
			mat.SetTexture("_DispTex", dispmap);

			glitchup += Time.deltaTime * intensity;
			glitchdown += Time.deltaTime * intensity;
			flicker += Time.deltaTime * intensity;

			if(flicker > ftime){
				mat.SetFloat("filterRadius", Random.Range(-3f, 3f) * intensity);
				flicker = 0;
				ftime = Random.value;
			}

			if(glitchup > uptime){
				if(Random.value < 0.1f * intensity)
					mat.SetFloat("flip_up", Random.Range(0, 1f) * intensity);
				else
					mat.SetFloat("flip_up", 0);

				glitchup = 0;
				uptime = Random.value/10f;
			}

			if(glitchdown > downtime){
				if(Random.value < 0.1f * intensity)
					mat.SetFloat("flip_down", 1-Random.Range(0, 1f) * intensity);
				else
					mat.SetFloat("flip_down", 1);

				glitchdown = 0;
				downtime = Random.value/10f;
			}

			if(Random.value < 0.05 * intensity){
				mat.SetFloat("displace", Random.value * intensity);
				mat.SetFloat("scale", 1-Random.value * intensity);
			}else
				mat.SetFloat("displace", 0);

			Graphics.Blit (source, dest, mat);
		}
	}
}
