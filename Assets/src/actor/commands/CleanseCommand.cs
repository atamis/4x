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
            
			if (u.actions < 2) {
				throw new Exception (u + "doesn't have enough action points!");
			}
		}

        private void purify(Hex h) {
            if (h.miasma != null) {
                h.miasma.level -= 1;
                if (h.miasma.level < 0) {
                    h.miasma.Die();
                }
                UnityEngine.Debug.Log("Cleansed " + u.h);
            }
        }

        public override void Apply(WorldMap w) {
			u.actions -= 2;

            purify(u.h);

            foreach (Hex h in u.h.Neighbors()) {
                purify(h);
            }
		}

		public override void Undo(WorldMap w) {
			throw new NotImplementedException ("You can't undo a Industrial Military Complex!");
		}
	}
}
