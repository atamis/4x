using UnityEngine;
using System.Collections;
using game.actor;
using System;

namespace game.world.buildings {

    class Harvester : Building {

        public override BuildingType? getBuildingType() {
            return BuildingType.Harvester;
        }

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
            if (pn != null) {
                if (h.node != null) {
                    pn.power += getBuildingType().Value.PowerGen();
                } else {
                    pn.power += 1;
                }
                
            }
        }


        public override string GetName() {
			return "Harvester";
		}

    }
}
