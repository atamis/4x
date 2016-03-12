using UnityEngine;

namespace game.ui {
	class SolarClock {
		SolarModel s_model;
		LunarModel m_model;

		public SolarClock() {
			this.s_model = new GameObject ("Solar Model").AddComponent<SolarModel> ();
			s_model.init (this);

			this.m_model = new GameObject ("Lunar Model").AddComponent<LunarModel> ();
			m_model.init (this);
		}

		private class SolarModel : MonoBehaviour {
			SpriteRenderer sr;
			SolarClock st;
			float clock;

			public void init(SolarClock st) {
				this.sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite>("Textures/Circle");
				sr.color = new Color (1, 1, 1);

				transform.localPosition = new Vector3 (0, 0, 1);
				transform.localScale = new Vector3 (1.1f, 1.1f, 1f);

				clock = 0f;
			}

			void Update() {
				clock += 0.01f;
				transform.eulerAngles = new Vector3(0,0,7*clock);
			}
		}

		private class LunarModel : MonoBehaviour {
			SpriteRenderer sr;
			SolarClock st;
			//float clock;
			public Material mat;

			public void init(SolarClock st) {
				this.sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite>("Textures/Circle");
				transform.localScale = new Vector3 (1f, 1f, 1f);

				mat = new Material (Shader.Find ("Custom/GlitchShader"));
				mat.color = new Color (0, 0, 0);

				sr.material = mat;
			}

			void Update() {
				//sr.material = mat;
			}
		}
	}
}

