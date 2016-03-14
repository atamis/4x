using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {

        private static AudioClip[] clips = new AudioClip[] {
            Resources.Load<AudioClip>("Audio/Music/spacemanSpiff"),
            Resources.Load<AudioClip>("Audio/Music/Building Better Worlds"),
            Resources.Load<AudioClip>("Audio/Music/Tin_Can"),
        };

        private static System.Random rand = new System.Random();

        private static AudioClip randClip() {
            return clips[rand.Next(clips.Length)];
        }

		GameManager gm;
		public int ticks = 0;
		AudioSource source;

		public void init(GameManager gm) {
			this.gm = gm;

			source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
        }

		// Use this for initialization
		void Start () {
            source.PlayOneShot(randClip());
		}

		// Update is called once per frame
		void Update () {
            if (!source.isPlaying) {
                source.PlayOneShot(randClip());
            }

            float zoom = gm.pc.cam.orthographicSize;
            if (zoom < 4.4) {
                source.volume = 0;
            } else {
                source.volume = ((zoom - 4.4f) / (20f - 4.4f)) * 0.3f;

            }

        }
	}
}
