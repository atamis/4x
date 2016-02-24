using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.actor {
    class Player : Actor {
        public Queue<Command> commands;
        public Player(String name) : base(name) {
            commands = new Queue<Command>();
        }

        public void AddCommand(Command c) {
            commands.Enqueue(c);
        }

        public override Command GetNextCommand() {
            if (commands.Count > 0) {
                return commands.Dequeue();
            }

            return null;
        }
    }
}
