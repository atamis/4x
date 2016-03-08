using System;
using System.Collections.Generic;
using System.Linq;
using game.world;
using game.world.units;

namespace game.actor.commands {
	class MoveCommand : Command {
		Unit u;
		Hex target;
		Hex prev;
		int distance;

		public MoveCommand(Actor a, Unit u, Hex target) : base(a) {
			this.u = u;
			this.prev = u.h;
			this.target = target;

			distance = target.loc.Distance (prev.loc);
			if (distance > u.actions) {
				throw new Exception (u + "doesn't have enough movement points!");
			}
		}

		public override void Apply(WorldMap w) {
			UnityEngine.Debug.Log ("Moved to " + target);
			u.h = target;
			u.actions -= distance;

			target.reveal ();
			foreach (Hex h in target.Neighbors()) {
				h.reveal ();
			}
		}

		public override void Undo(WorldMap w) {
			u.h = prev;
		}
	}


}
