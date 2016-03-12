using UnityEngine;
using game.actor.commands;

namespace game.actor {
	class Actor {
		string name;

		public Actor(string name) {
			this.name = name;
		}

		public virtual void StartTurn() {

		}

		public virtual Command GetNextCommand() {
			return null;
		}

		public string GetName() {
			return name;
		}
	}

	class PassTurnActor : Actor {
		public PassTurnActor(string name) : base(name) {

		}

		public override Command GetNextCommand () {
			return new EndTurnCommand (this);
		}
	}
}