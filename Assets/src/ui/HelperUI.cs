using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.ui {
	public class HelperUI : MonoBehaviour {
		private static Texture2D tex;
		private LinkedList<string> messages;

		public void init() {
			tex = Resources.Load<Texture2D>("Textures/Helper/T_HelperL1");
			messages = new LinkedList<string>();

			AddMessage("Team Yog-Sothoth welcomes you.");
			AddMessage("Enjoy the game.");

			EventManager.StartEvent += new EventManager.GameEvent(OnStartEvent);
			
		}

		void Start() {

		}

		public void AddMessage(string msg) {
			messages.AddFirst(msg);
		}

		void OnStartEvent(GameEventArgs eventArgs) {
			Debug.Log("Recieved Start Event");
		}

		void OnScanEvent() {
			Debug.Log ("Recieved Scan Event!");
		}

		void OnMoveEvent() {
			Debug.Log ("Recieved Move Event");
		}

		void OnSpreadEvent() {
			Debug.Log ("Recieved Cleanse Event");
		}

		void OnGUI() {
			var m = messages.Take(6).Reverse().Aggregate<string>((acc, msg) => acc + "\n" + msg);
			GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f - tex.height, tex.width, tex.height), tex);
			GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f, 250, 100), m);
		}
	}
}
