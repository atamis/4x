using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game {
    class TutorialManager : MonoBehaviour {
		AudioClip[] clips;
		AudioSource source;
		Queue<int> queue;
		public bool[] milestones = new bool[20];

		public void init() {
			clips = new AudioClip[24];
			for (int i = 0; i < 24; i++) {
				clips [i] = Resources.Load <AudioClip> ("Audio/Dialogue/Dialogue" + i);
			}

			source = gameObject.AddComponent<AudioSource>();
			source.loop = false;

			this.queue = new Queue<int> ();
			this.milestones [0] = true;
		}

		void Start() {
		}

		public void play(int code) {
			if (source.isPlaying == true) {
				source.Stop ();
			}
			source.PlayOneShot(clips[code]);
			//Debug.Log ("Played Clip");
		}

		public void enqueueClip (int code) {
			this.queue.Enqueue (code);
		}

		public void playQueue(params int[] clips) {
			if (source.isPlaying) {
				source.Stop ();
			}
			foreach (int i in clips) {
				this.queue.Enqueue (i);
			}
		}

		void Update() {
			if (this.queue.Count > 0 && !source.isPlaying) {
				source.PlayOneShot (clips[queue.Dequeue ()]);
			}
		}
    }
}
