using game.world;
using game.world.buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.actor.commands {
    class DeleteBuildingCommand : Command {
        private Hex h;

        public DeleteBuildingCommand(Actor a, Hex h) : base(a) {
            this.h = h;
        }

        public override void Apply(WorldMap w) {
            if (h.building != null && h.building.a == a) {
                var b = h.building;
                h.building = null;
                b.Die();
            }
        }
    }
}
