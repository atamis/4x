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
		int distance {
            get {
                return target.loc.Distance(u.h.loc);
            }
        }

		private MoveCommand(Actor a, Unit u, Hex target) : base(a) {
			this.u = u;
			this.target = target;

			if (distance > u.actions) {
				throw new Exception (u + "doesn't have enough movement points!");
			}
		}

		public override void Apply(WorldMap w) {
            prev = u.h;

            u.actions -= distance;
            u.h = target;

			target.reveal ();
			foreach (Hex h in target.Neighbors()) {
				h.reveal ();
			}
			UnityEngine.Debug.Log ("Moved to " + target + ", distance " + distance);
		}

		public override void Undo(WorldMap w) {
			u.h = prev;
		}

        public static List<MoveCommand> pathfind(WorldMap w, Actor a, Unit u, Hex target) {
            var ll = WorldPathfinding.Pathfind(w, u.h, target);
            if (ll == null) {
                throw new Exception("Path could not be found to " + target);
            }

            if ((ll.Count() - 1) > u.actions) {
                throw new Exception("Unit doesn't have enough movement points!");
            }

            var moves = new List<MoveCommand>(ll.Count);

            foreach(Hex h in ll) {
                moves.Add(new MoveCommand(a, u, h));
            }

            return moves;
        }
	}


}
