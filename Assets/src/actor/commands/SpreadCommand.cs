﻿using System; 
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using game.math;
using game.world;

namespace game.actor.commands {
	class SpreadCommand : Command {
		int aggression;

		public SpreadCommand(Actor a, int aggression) : base(a) {
			this.aggression = aggression;
		}

		public float infectChance() {
			switch (aggression) {
			case 0:
				return .015f;
			case 1:
				return .03f;
			case 2:
				return .08f;
			case 3:
				return .2f;
			}
			return 0f;
		}

		public override void Apply (WorldMap w) {
			HashSet<Hex> tiles = new HashSet<Hex> ();
			//int i = 0;
			foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
				Hex tile = kv.Value;
				if (tile.miasma != null && tile.miasma.canSpread() ) {
					//i++;
					foreach (Hex t in tile.Neighbors()) {
						if (t.b.Passable ()) tiles.Add (t);
					}
				}
			}
			foreach (Hex t in tiles) {
				float rand = UnityEngine.Random.value;
				if (rand < infectChance()) {
					Miasma m = new GameObject ("Miasma").AddComponent<Miasma> ();
					m.init (w, t);
				}
			}
		}

		public override void Undo(WorldMap w) {
			throw new NotImplementedException ("Entropy is hard to reverse");
		}
	}
}