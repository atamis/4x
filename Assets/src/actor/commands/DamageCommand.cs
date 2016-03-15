using System;
using System.Collections.Generic;
using System.Linq;
using game.math;
using game.world;

namespace game.actor.commands {
	class DamageCommand : Command {
		public DamageCommand(Actor a) : base(a) {
		}

		public override void Apply(WorldMap w) {
			foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
				Hex tile = kv.Value;
				int l = -1;
				if (tile.miasma != null) {
					l = tile.miasma.level;
				}
				switch (l) {
				case -1:
					break;
				case 1:
					if (tile.building != null) {
						tile.building.disabled = true;
					}
					if ((tile.units.Count > 0) && (w.time == GameTime.Night)) {
                        while (tile.units.Count > 0) {
                            tile.units.First().Die();
                        }
					}
					break;
				default:
                    if (tile.building != null) {
                        tile.building.disabled = true;
                    }
                    if ((tile.units.Count > 0) && (w.time == GameTime.Night)) {
                        while (tile.units.Count > 0) {
                            tile.units.First().Die();
                        }
                    }
                    break;
				}
			}
		}

		public override void Undo(WorldMap w) {
			throw new NotImplementedException ("Entropy is hard to reverse");
		}
	}
}
