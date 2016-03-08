using UnityEngine;
using System.Collections.Generic;
using game.actor.commands;

namespace game.actor {
	class AIActor : Actor {
		Queue<Command> commands;

		public AIActor() : base("AI Actor") {
			commands = new Queue<Command> ();
		}

		public override void StartTurn() {

		}

		public override Command GetNextCommand () {
			if (commands.Count > 0) {
				return commands.Dequeue ();
			}
			return null;
		}

	}
}