using UnityEngine;
using System.Collections;
using game.world;
using game.world.units;
using game.world.buildings;

namespace game.actor.commands {
	class BuildCommand : Command {
		Unit u;
		Hex h;
		BuildingType t;

		public BuildCommand(Actor a, Unit u, Hex h, BuildingType t) : base(a) {
			this.u = u;
			this.h = h;
			this.t = t;
		}

		public override void Apply(WorldMap w) {
			WarpingBuilding b = new GameObject ("Warping " + t.ToString ()).AddComponent<WarpingBuilding> ();
			b.init (a, h, t);
		}

		public override void Undo(WorldMap w) {
            var b = h.building;
            h.building = null;
            b.Die();
        }
	}
}
