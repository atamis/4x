using System;
using System.Collections.Generic;
using System.Linq;
using game.world;
using game.world.units;

namespace game.actor.commands {
	class CleanseCommand : Command {
		Unit u;
		Hex h;

		public CleanseCommand(Actor a, Unit u) : base(a) {
			this.u = u;
			this.h = u.h;

			if (this.h.miasma == null) {
				throw new Exception ("There isn't any miasma at that location1");
			} else if (u.actions < 1) {
				throw new Exception (u + "doesn't have enough action points!");
			}
		}

		public override void Apply(WorldMap w) {
			u.actions--;
			h.miasma.Die();
			UnityEngine.Debug.Log ("Cleansed " + u.h);
		}

		public override void Undo(WorldMap w) {
			throw new NotImplementedException ("You can't undo a Industrial Military Complex!");
		}
	}
}
