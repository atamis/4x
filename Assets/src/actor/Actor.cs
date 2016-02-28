using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.actor {
    class Actor {
        public String name;

        public Actor(string name) {
            this.name = name;
        }

        public virtual void StartTurn() {

        }

        public virtual Command GetNextCommand() {
            return null;
        }
    }

    class PassTurnActor : Actor {
        public PassTurnActor(String name) : base(name) {

        }

        public override Command GetNextCommand() {
            return new EndTurnCommand(this);
        }
    }
}
