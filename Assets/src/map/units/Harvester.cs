using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;

namespace game.map.units {
    class Harvester : Building {

        public override bool Powered() {
            return true;
        }

        public override bool ProjectsPower() {
            return true;
        }

        public override string GetName() {
            return "Harvester";
        }

        public override void NewTurn(Actor old, Actor cur) {
            if (cur == a) {
                print("Gathering energy!");
            }
        }
    }
}
