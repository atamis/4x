using UnityEngine;
using System.Collections.Generic;

namespace game {
	public class GameEventArgs : System.EventArgs {

	}

	static class EventManager {
		public delegate void GameEvent(GameEventArgs eventArgs);
		public static event GameEvent StartEvent, ScanEvent, MoveEvent, SpreadEvent;

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
