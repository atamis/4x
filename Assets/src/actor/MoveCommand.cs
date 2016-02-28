using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map.units;
using game.map;

namespace game.actor {
    class MoveCommand : Command {
        // TODO: doesn't take passability/impassability into account. Doesn't do any routing.
        Unit u;
        Hex target;
        Hex oldLocation;
        int distance;

        public MoveCommand(Actor a, Unit u, Hex target) : base(a) {
            this.u = u;
            this.oldLocation = u.h;
            this.target = target;
            distance = target.loc.Distance(oldLocation.loc);
            if (distance > u.movement) {
                throw new Exception(u + " doesn't have enough movement points (" +
                    u.movement + " vs. " + target.loc.Distance(oldLocation.loc)  + ")");
            }
            if (target.units.Count > 0){
                throw new Exception("The target hex already has a unit on it "
                    + "(Number of units on target = " + target.units.Count() + ")");
            }
        }

        public override void Apply(WorldMap w) {
            Console.WriteLine("Moved to " + target);
            u.h = target;
            u.movement -= distance;
        }

        public override void Undo(WorldMap w) {
            u.h = oldLocation;
        }
    }
}
