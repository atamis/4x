using UnityEngine;
using System.Collections.Generic;
using game.actor.commands;

namespace game.actor {
	class AIActor : Actor {
		Queue<Command> commands;
		private int aggression;

		public AIActor() : base("AI Actor") {
			commands = new Queue<Command> ();
			this.aggression = 3;
		}

		public override void StartTurn() {
			commands.Enqueue(new SpreadCommand(this, aggression));
			commands.Enqueue(new DamageCommand(this));
			commands.Enqueue(new EndTurnCommand(this));
		}

		public override Command GetNextCommand () {
			if (commands.Count > 0) {
				return commands.Dequeue ();
			}
			return null;
		}

	}
}
