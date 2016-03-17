using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.ui {
	class HelperUI : MonoBehaviour {
		private Texture2D[] texes;

		private static Texture2D tex;
		private LinkedList<string> messages;
		UIManager um;

		public void init(UIManager um) {
			this.um = um;
			texes = new Texture2D[5]; 
			for (int i = 1; i < 6; i++) {
				texes [i - 1] = Resources.Load<Texture2D> ("Textures/Helper/T_Helper" + i);
			}

			transform.parent = um.transform;

			tex = Resources.Load<Texture2D>("Textures/Helper/T_HelperL1");
			messages = new LinkedList<string>();

			messages.AddFirst ("Welcome!");
			EventManager.StartEvent += new EventManager.GameEventHandler(OnStartEvent);
			EventManager.BuildEvent += new EventManager.WarpEventHandler(OnBuildEvent);
			EventManager.ScanEvent += new EventManager.ScanEventHandler(OnScanEvent);
			EventManager.ErrorEvent += new EventManager.InvalidEventHandler(OnInvalidAction);
			EventManager.TutorialEvent += new EventManager.TutorialEventHandler (OnTutorialEvent);
			EventManager.SpreadEvent += new EventManager.SpreadEventHandler (OnSpreadEvent);

			EventManager.PostStartEvent(new GameEventArgs{} ); // start the game
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

		void OnSpreadEvent(SpreadEventArgs args) {
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

		void OnGamewOn(GameEventArgs args) {
			um.tm.play (19);
		}

		void OnGameOver(GameEventArgs args) {
			um.tm.play (20);
		}

		void OnGUI() {
			var m = messages.Take(6).Reverse().Aggregate<string>((acc, msg) => acc + "\n" + msg);
			GUI.Box(new Rect(Screen.width - tex.width, 50, tex.width , tex.width ), tex);
			GUI.Box(new Rect(0, Screen.height * .8f, Screen.width * .25f, Screen.height * .2f), m);
		}
	}
}
