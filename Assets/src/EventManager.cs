using UnityEngine;
using System.Collections.Generic;

namespace game {
	class GameEventArgs : System.EventArgs {
		public int turn { get; set; }
	}

	class InvalidActionArgs : System.EventArgs {
		public string msg { get; set; }
	}
		
	class ScanEventArgs : System.EventArgs {
		public bool found { get; set; }
	}
		
	class MoveEventArgs : System.EventArgs {
		public int stamina { get; set; }
		public int state { get; set; }
	}

	class PresentEventArgs : System.EventArgs {
		public string type { get; set; }
	}

	class BuildEventArgs : System.EventArgs {
		public string name { get; set; }
		public int turns { get; set; }
	}

	class TutorialEventArgs : System.EventArgs {
		public int tut_id { get; set; }
	}

	class SpreadEventArgs : GameEventArgs {
		
	}

	/*
	class Event : System.EventArgs {

	}
	*/

	static class EventManager {
		public delegate void GameEventHandler(GameEventArgs args);
		public static GameEventHandler StartEvent, DefeatEvent, VictoryEvent;

		public delegate void InvalidEventHandler(InvalidActionArgs args);
		public static InvalidEventHandler ErrorEvent;

		public delegate void TutorialEventHandler(TutorialEventArgs args);
		public static event TutorialEventHandler TutorialEvent;

		public delegate void ScanEventHandler(ScanEventArgs args);
		public static ScanEventHandler ScanEvent;

		public delegate void MoveEventHandler(MoveEventArgs args);
		public static MoveEventHandler MoveEvent;

		public delegate void WarpEventHandler(BuildEventArgs eventArgs);
		public static event WarpEventHandler BuildEvent;

		public delegate void SpreadEventHandler(SpreadEventArgs eventArgs);
		public static event SpreadEventHandler SpreadEvent;

		public delegate void PresentEventHandler(PresentEventArgs eventArgs);
		public static event PresentEventHandler PresentEvent;

		public static void PostInvalidAction(InvalidActionArgs args = null) {
			if (ErrorEvent != null) {
				ErrorEvent (args);
			}
		}

		public static void PostStartEvent(GameEventArgs args = null) {
			if (StartEvent != null) {
				StartEvent (args);
			}
		}

		public static void PostPresentEvent(PresentEventArgs args = null){
			if (PresentEvent != null) {
				PresentEvent (args);
			}
		}

		public static void PostDefeatEvent(GameEventArgs args = null) {
			if (DefeatEvent != null) {
				DefeatEvent (args);
			}
		}

		public static void PostVictoryEvent(GameEventArgs args = null) {
			if (VictoryEvent != null) {
				VictoryEvent (args);
			}
		}

		public static void PostTutorialEvent(TutorialEventArgs args = null) {
			if (TutorialEvent != null) {
				TutorialEvent (args);
			}
		}

		public static void PostScanEvent(ScanEventArgs args = null) {
			if (ScanEvent != null) {
				ScanEvent (args);
			}
		}

		public static void PostMoveEvent(MoveEventArgs args = null) {
			if (MoveEvent != null) {
				MoveEvent (args);
			}
		}

		public static void PostBuildEvent(BuildEventArgs args = null) {
			if (BuildEvent != null) {
				BuildEvent (args);
			}
		}

		public static void PostSpreadEvent(SpreadEventArgs args = null) {
			if (SpreadEvent != null) {
				SpreadEvent (args);
			}
		}
	}
}
