using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor.commands;
using game.world.units;
using game.world;

namespace game.actor {
	class PurifyCommand : Command {

		Unit u;
		Hex target;

		public PurifyCommand(Actor a, Unit u, Hex target) : base(a) {
			this.target = target;
			if (u == null) {
				UnityEngine.Debug.Log ("");
			}
		}

		public override void Apply(WorldMap w) {
			UnityEngine.Debug.Log("Purified " + target);
			target.miasma = null;
		}
	}
}

