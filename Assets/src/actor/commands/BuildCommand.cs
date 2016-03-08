using UnityEngine;
using System.Collections;
using game.world;
using game.world.units;
using game.world.buildings;

namespace game.actor.commands {
	class BuildCommand : Command {
		Unit u;
		Hex target;
		BuildingType t;

		public BuildCommand(Actor a, Unit u, Hex h, BuildingType t) : base(a) {
			this.u = u;
			this.target = h;
			this.t = t;
		}

		public override void Apply(WorldMap w) {
			WarpingBuilding b = new GameObject ("Warping " + t.ToString ()).AddComponent<WarpingBuilding> ();
			b.init (a, target, t);
		}
	}
}