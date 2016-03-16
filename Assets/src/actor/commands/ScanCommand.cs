using UnityEngine;
using System;
using System.Collections.Generic;
using game;
using game.world;
using game.world.units;

namespace game.actor.commands {

	class ScanCommand : Command {
        private static readonly AudioClip scanSound = Resources.Load<AudioClip>("Audio/Scan 3");

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
            u.actions--;

			bool found = false;
			target.scan ();
			foreach (Hex h in target.Neighbors()) {
				h.scan ();
				if (h.node != null) {
					found = true;
				}
			}
				
            u.au.PlayOneShot(scanSound);

			if (found) {
				EventManager.PostScanEvent(new ScanEventArgs { found = true });
			}
		}
	}
}
