﻿using System;
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
			EventManager.BuildMenuEvent += new EventManager.GameEvent(OnBuildMenuEvent);
			EventManager.BuildEvent += new EventManager.WarpEvent(OnBuildEvent);
			EventManager.ScanEvent += new EventManager.GameEvent(OnScanEvent);
			EventManager.MoveEventBefore += new EventManager.MoveEvent(OnMoveBeforeEvent);
			EventManager.MoveEventAfter += new EventManager.MoveEvent(OnMoveAfterEvent);
			EventManager.InvalidEvent += new EventManager.InvalidActionEvent(OnInvalidAction);
		}

		void Start() {
		}

		public void AddMessage(string msg) {
			messages.AddFirst(msg);
		}

		void OnInvalidAction(InvalidActionArgs evt) {
			messages.AddFirst(evt.msg);
		}

		void OnBuildMenuEvent(GameEventArgs eventArgs){
			AddMessage("What structure do you want to warp in?");
			Debug.Log("Received Build Menu Event");
		}

		void OnBuildEvent (BuildEventArgs eventArgs){
			AddMessage("The " + eventArgs.name + " will be warped in " + eventArgs.turns + " turn(s).");
			Debug.Log("Received Build Event");	
		}

		void OnStartEvent(GameEventArgs eventArgs) {
			Debug.Log("Recieved Start Event");
		}

		void OnScanEvent(GameEventArgs eventArgs) {
			messages.AddFirst("Your unit has scanned the area.");
			Debug.Log ("Recieved Scan Event!");
		}

		void OnMoveBeforeEvent(MoveEventArgs eventArgs) {
			AddMessage("Where would you like to move your unit? \n (Stamina = " + eventArgs.stamina + ")");
			Debug.Log ("Recieved Move Event");
		}

		void OnMoveAfterEvent(MoveEventArgs eventArgs) {
			AddMessage("This unit now has " + eventArgs.stamina + " stamina.");
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
