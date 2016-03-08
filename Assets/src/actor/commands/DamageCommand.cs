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
				int l = tile.miasma.level;
				switch (l) {
				case 1:
					if (tile.building != null) {
						tile.building.disabled = true;
					}
					if ((tile.unit != null) && (w.time == GameTime.Night)) {
						tile.unit.Die ();
					}
					break;
				default:
					if (tile.building != null) {
						tile.building.Die ();
					}
					if ((tile.unit != null) && (w.time == GameTime.Night)) {
						tile.unit.Die ();
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
