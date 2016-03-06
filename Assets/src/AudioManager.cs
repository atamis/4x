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
			this.clip = Resources.Load<AudioClip>("Audio/Music/spacemanSpiff");

			source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
			source.PlayOneShot(clip);
            source.loop = true;
        }

		// Use this for initialization
		void Start () {
            source.Play();
		}

		// Update is called once per frame
		void Update () {
            float zoom = gm.pc.cam.orthographicSize;
            if (zoom < 4.4) {
                source.volume = 0;
            } else {
                source.volume = ((zoom - 4.4f) / (20f - 4.4f)) * 0.3f;

            }

        }
	}
}

