using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game {
    class TutorialManager : MonoBehaviour {
		private static AudioClip[] clips = new AudioClip[] {
			Resources.Load<AudioClip>("Audio/Dialogue/DialogueA"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue B"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue C"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue D"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue E"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue F"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue G"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue H"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue I"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue J"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue K"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue L"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue M"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue N"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue O"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue P"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue Q"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue R"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue S"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue T"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue U"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue V"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue W"),
			Resources.Load<AudioClip>("Audio/Dialogue/Dialogue X"),
		};

		public bool[] milestones = new bool[20];
		AudioSource source;
		Queue<int> queue;

		public void init() { 
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
