using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map;

namespace game.actor {
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
