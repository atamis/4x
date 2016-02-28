using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.actor {
    class AIActor : Actor {
        Queue<Command> commands;
        private int aggression;

        public AIActor() : base("AI Actor") {
            commands = new Queue<Command>();
            this.aggression = 3;
        }

        public override void StartTurn() {
            commands.Enqueue(new SpreadCorruptionCommand(this, aggression));
            commands.Enqueue(new EndTurnCommand(this));
        }

        public override Command GetNextCommand() {
            if (commands.Count > 0) {
                return commands.Dequeue();
            }
            return null;
        }
    }
}
