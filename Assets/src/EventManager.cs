using UnityEngine;
using System.Collections.Generic;

namespace game {
	public class InvalidActionArgs : System.EventArgs {
		public string msg { get; set; }
	}

	public class GameEventArgs : System.EventArgs {

	}

	static class EventManager {
		// game event handler
		public delegate void GameEvent(GameEventArgs eventArgs);
		public static event GameEvent StartEvent, ScanEvent, MoveEvent, SpreadEvent;

		// Invalid event handler
		public delegate void InvalidActionEvent(InvalidActionArgs eventArgs);
		public static event InvalidActionEvent InvalidEvent;

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

		public static void TriggerScanEvent(GameEventArgs eventArgs = null) {
			if (ScanEvent != null) { // if the number of subscribers isn't null
				ScanEvent (eventArgs);
			}
		}

		public static void TriggerMoveEvent(GameEventArgs eventArgs = null) {
			if (MoveEvent != null) {
				MoveEvent (eventArgs);
			}
		}

		public static void TriggerSpreadEvent(GameEventArgs eventArgs = null) {
			if (SpreadEvent != null) {
				SpreadEvent (eventArgs);
			}
		}
	}
}
