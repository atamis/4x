using UnityEngine;
using System.Collections;

namespace game.effects {
	class GlitchMaterial : CustomMaterial {
		float glitchup, glitchdown, flicker; // timers for effects
		float uptime = 0.05f, downtime = 0.05f, ftime = 0.05f; // length of effect
		float intensity = 1;

		public GlitchMaterial (Shader s) : base (s) { 

		}

		public override void tick(params float[] list) {
			this.SetFloat ("_intensity", intensity);

			glitchup += Time.deltaTime * intensity;
			glitchdown += Time.deltaTime * intensity;
			flicker += Time.deltaTime * intensity;

			if(flicker > ftime){
				this.SetFloat("filterRadius", Random.Range(-3f, 3f) * intensity);
				flicker = 0;
				ftime = Random.value;
			}

			if(glitchup > uptime){
				if(Random.value < 0.1f * intensity)
					this.SetFloat("flip_up", Random.Range(0, 1f) * intensity);
				else
					this.SetFloat("flip_up", 0);

				glitchup = 0;
				uptime = Random.value/10f;
			}

			if(glitchdown > downtime){
				if(Random.value < 0.1f * intensity)
					this.SetFloat("flip_down", 1-Random.Range(0, 1f) * intensity);
				else
					this.SetFloat("flip_down", 1);

				glitchdown = 0;
				downtime = Random.value/10f;
			}

			if (Random.value < 0.05 * intensity) {
				this.SetFloat ("displace", Random.value * intensity);
				this.SetFloat ("scale", 1 - Random.value * intensity);
			} else {
				this.SetFloat ("displace", 0);
			}
		}
	}
}
