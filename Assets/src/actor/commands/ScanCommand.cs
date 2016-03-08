using UnityEngine;
using System;
using System.Collections.Generic;
using game;
using game.world;
using game.world.units;

namespace game.actor.commands {

	class ScanCommand : Command {
		Unit u;
		Hex target;

		public ScanCommand(Actor a, Unit u, Hex target) : base(a) {
			this.u = u;
			this.target = target;
			if (u.actions == 0) {
				throw new Exception (u + " doesn't have enough action points!");
			}
		}

		public override void Apply(WorldMap w) {
			UnityEngine.Debug.Log ("Scanned at " + this.target.ToString ());
			target.scanned = true;
			foreach (Hex h in target.Neighbors()) {
				h.scanned = true;
			}
			EventManager.TriggerScanEvent(new GameEventArgs{});
		}
	}


}
