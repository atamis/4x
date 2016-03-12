using UnityEngine;
using System.Collections.Generic;
using game.actor.commands;

namespace game.actor {
	
	class Player : Actor {
		public Queue<Command> commands;

		public Player(string name) : base(name) {
			commands = new Queue<Command> ();

		}

		public void AddCommand(Command c) {
			commands.Enqueue (c);
		}

        public void AddAllCommands<T>(List<T> lst) where T : Command {
            if (lst != null) {
                foreach (Command c in lst) {
                    AddCommand(c);
                }
            }
        }

		public override Command GetNextCommand () {
			if (commands.Count > 0) {
				return commands.Dequeue ();
			}
			return null;
		}
	}
}