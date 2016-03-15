using UnityEngine;
using System.Collections.Generic;

namespace game {

	public class GameEventArgs : System.EventArgs {
		public int turn { get; set; }
	}

	public class InvalidActionArgs : System.EventArgs {
		public string msg { get; set; }
	}

	public class ScanEventArgs : System.EventArgs {
		public bool found { get; set; }
	}
		
	public class MoveEventArgs: System.EventArgs{
		public int stamina { get; set;}
	}

	public class BuildEventArgs: System.EventArgs{
		public string name { get; set;}
		public int turns { get; set;}
	}

	public class TutorialEventArgs : System.EventArgs {
		public int tut_id { get; set; }
	}

	static class EventManager {
		// game event handler
		public delegate void GameEvent(GameEventArgs eventArgs);
		public static event GameEvent StartEvent, SpreadEvent, BuildMenuEvent;
		public static event GameEvent GameOver, GameWon;

		//Move event handler
		public delegate void MoveEvent(MoveEventArgs eventArgs);
		public static event MoveEvent MoveEventBefore, MoveEventAfter;

		// Invalid event handler
		public delegate void InvalidActionEvent(InvalidActionArgs eventArgs);
		public static event InvalidActionEvent InvalidEvent;

		//Build event handler
		public delegate void WarpEvent(BuildEventArgs eventArgs);
		public static event WarpEvent BuildEvent;

		// Select Unit Handler
		public delegate void TutorialEvent(TutorialEventArgs args);
		public static event TutorialEvent TutEvent;

		// Scan Event Handler
		public delegate void ScanEvent(ScanEventArgs eventArgs);
		public static event ScanEvent ScannedEvent;



		public static void PostInvalidAction(InvalidActionArgs args = null) {
			if (InvalidEvent != null) {
				InvalidEvent(args);
			}
		}

		public static void TriggerStartEvent(GameEventArgs eventArgs = null) {
			if (StartEvent != null) {
				StartEvent(eventArgs);
			}
		}

		public static void TriggerTutorialEvent(TutorialEventArgs args = null) {
			if (TutEvent != null) {
				TutEvent(args);
			}
		}

		public static void TriggerBuildEvent(BuildEventArgs eventArgs = null) {
			if (BuildEvent != null) {
				BuildEvent(eventArgs);
			}
		}

		public static void TriggerBuildMenuEvent(GameEventArgs eventArgs = null) {
			if (BuildMenuEvent != null) {
				BuildMenuEvent(eventArgs);
			}
		}

		public static void TriggerScanEvent(ScanEventArgs eventArgs = null) {
			if (ScannedEvent != null) { // if the number of subscribers isn't null
				ScannedEvent (eventArgs);
			}
		}

		public static void TriggerMoveEventBefore(MoveEventArgs eventArgs = null) {
			if (MoveEventBefore != null) {
				MoveEventBefore (eventArgs);
			}
		}

		public static void TriggerMoveEventAfter(MoveEventArgs eventArgs = null) {
			if (MoveEventAfter != null) {
				MoveEventAfter (eventArgs);
			}
		}

		public static void TriggerGameOver(GameEventArgs args = null) {
			if (GameOver != null) {
				GameOver (args);
			}
		}

		public static void TriggerGameWon(GameEventArgs args = null) {
			if (GameWon != null) {
				GameWon (args);
			}
		}
		public static void TriggerSpreadEvent(GameEventArgs eventArgs = null) {
			if (SpreadEvent != null) {
				SpreadEvent (eventArgs);
			}
		}
	}
}
