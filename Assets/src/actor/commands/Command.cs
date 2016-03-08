using UnityEngine;
using System.Collections;
using game.world;

namespace game.actor.commands {
	class Command {
		public Actor a;

		public Command(Actor a) {
			this.a = a;
		}

		public virtual void Apply(WorldMap w) {

		}

		public virtual void Undo(WorldMap w) {

		}
	}

	class EndTurnCommand : Command {
		public EndTurnCommand(Actor a) : base(a) {

		}
	}
}
