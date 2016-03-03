using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {

		GameManager gm;
		AudioClip clip;
		public int ticks = 0;
		AudioSource source;

		public void init(GameManager gm) {
			this.gm = gm;
			this.clip = Resources.Load<AudioClip>("Audio/spacemanSpiff");

			source = gameObject.AddComponent<AudioSource>();
			source.PlayOneShot(clip);
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			ticks++;

			if (ticks % 8000 == 0) {
				source.PlayOneShot(clip);
			}
		}
	}
}

