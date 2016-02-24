using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map.units;
using game.map;

namespace game.actor {
    class MoveCommand : Command {
        Unit u;
        Hex target;
        Hex oldLocation;

        public MoveCommand(Actor a, Unit u, Hex target) : base(a) {
            this.u = u;
            this.oldLocation = u.h;
            this.target = target;
        }

        public override void Apply(WorldMap w) {
            Console.WriteLine("Moved to " + target);
            u.h = target;
        }

        public override void Undo(WorldMap w) {
            u.h = oldLocation;
        }
    }
}
