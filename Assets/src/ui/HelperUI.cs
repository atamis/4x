using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.ui {
	class HelperUI : MonoBehaviour {
		private static Texture2D tex;
		private LinkedList<string> messages;
		UIManager um;

		public void init(UIManager um) {
			this.um = um;
			tex = Resources.Load<Texture2D>("Textures/Helper/T_HelperL1");
			messages = new LinkedList<string>();

			AddMessage("Team Yog-Sothoth welcomes you.");
			AddMessage("Enjoy the game.");

			EventManager.StartEvent += new EventManager.GameEvent(OnStartEvent);
			EventManager.BuildMenuEvent += new EventManager.GameEvent(OnBuildMenuEvent);
			EventManager.BuildEvent += new EventManager.WarpEvent(OnBuildEvent);
			EventManager.ScannedEvent += new EventManager.ScanEvent(OnScanEvent);
			EventManager.MoveEventBefore += new EventManager.MoveEvent(OnMoveBeforeEvent);
			EventManager.MoveEventAfter += new EventManager.MoveEvent(OnMoveAfterEvent);
			EventManager.InvalidEvent += new EventManager.InvalidActionEvent(OnInvalidAction);
			EventManager.TutEvent += new EventManager.TutorialEvent (OnTutorialEvent);
			EventManager.SpreadEvent += new EventManager.GameEvent (OnSpreadEvent);

			EventManager.TriggerStartEvent(new GameEventArgs{} );
		}

		void Start() {
		}

		public void AddMessage(string msg) {
			messages.AddFirst(msg);
		}

		void OnStartEvent(GameEventArgs eventArgs) {
			um.tm.play (0);
			EventManager.StartEvent -= OnStartEvent;
		}

		void OnTutorialEvent(TutorialEventArgs args) {
			um.tm.play (args.tut_id);
		}

		void OnInvalidAction(InvalidActionArgs evt) {
			messages.AddFirst(evt.msg);
		}

		void OnBuildMenuEvent(GameEventArgs eventArgs){
			AddMessage("What structure do you want to warp in?");
			//Debug.Log("Received Build Menu Event");
		}

		void OnBuildEvent (BuildEventArgs eventArgs){
			AddMessage("The " + eventArgs.name + " will be warped in " + eventArgs.turns + " turn(s).");
			//Debug.Log("Received Build Event");	
		}
			
		void OnScanEvent(ScanEventArgs eventArgs) {
			messages.AddFirst("Your unit has scanned the area.");
			if (eventArgs.found == true) {
				um.postEvent (5);
			}
			//Debug.Log ("Recieved Scan Event!");
		}

		void OnMoveBeforeEvent(MoveEventArgs eventArgs) {
			AddMessage("Where would you like to move your unit?");
			//Debug.Log ("Recieved Move Event");
		}

		void OnMoveAfterEvent(MoveEventArgs eventArgs) {
			AddMessage("This unit now has been moved");
			//Debug.Log ("Recieved Move Event");
		}

		void OnSpreadEvent(GameEventArgs args) {
			//Debug.Log (um.tm.milestones.ToString ());
			//Debug.Log (args.turn);
			if (!um.tm.milestones [15] && args.turn > 5) {
				um.tm.enqueueClip (15);
				um.tm.milestones [15] = true;
			} else if (!um.tm.milestones [16] && args.turn >= 10) {
				um.tm.enqueueClip (16);
				um.tm.milestones [16] = true;
			}
			//Debug.Log ("Recieved Cleanse Event");
		}

		void OnGUI() {
			var m = messages.Take(6).Reverse().Aggregate<string>((acc, msg) => acc + "\n" + msg);
			GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f - tex.height, tex.width, tex.height), tex);
			GUI.Box(new Rect(Screen.width * .3f - 300, Screen.height * .8f, 250, 100), m);
		}
	}
}
