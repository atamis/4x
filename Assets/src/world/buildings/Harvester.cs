using UnityEngine;
using System.Collections;
using game.actor;

namespace game.world.buildings {

    class Harvester : Building {

        private static readonly int POWER_GEN = 5;

        public override bool Powered() {
            return true;
        }

        public override bool ProjectsPower() {
            return true;
        }

        public override bool VisuallyConnects() {
            return true;
        }

        public override void NewTurn(Actor old, Actor cur) {
            if (a == cur) {
                pn.power += POWER_GEN;
            }
        }


        public override string GetName() {
			return "Harvester";
		}
    }
}