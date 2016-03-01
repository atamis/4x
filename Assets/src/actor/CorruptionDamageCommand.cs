using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map;

namespace game.actor {
    class CorruptionDamageCommand : Command {

        public CorruptionDamageCommand(Actor a) : base(a) {

        }

        public override void Apply(WorldMap w) {
            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                Hex tile = kv.Value;
                if (tile.corrupted) {
                    if (tile.building != null) {
                        tile.building.SelfDestruct();
                    }
                    while (tile.units.Count() > 0) {
                        tile.units.First().SelfDestruct();
                    }
                }
            }
        }

        public override void Undo(WorldMap w) {
            throw new NotImplementedException("Andrew is too lazy, and entropy is hard to reverse");
        }
    }
}
