using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map;
using game.map.units;

namespace game.actor {
    class WarpUnitCommand : Command {
        WarpGate g;

        public WarpUnitCommand(Actor a, WarpGate g) : base(a) {
            this.g = g;
        }

        public override void Apply(WorldMap w) {
            if (g.h != null) {
                g.unitQueue++;
            }
        }

        public override void Undo(WorldMap w) {
            if (g.h != null) {
                g.unitQueue--;
            }
        }
    }
}
