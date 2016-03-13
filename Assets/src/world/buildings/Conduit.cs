using UnityEngine;
using System.Collections;
using System;

namespace game.world.buildings {

    class Conduit : Building {
        public override BuildingType? getBuildingType() {
            return BuildingType.Conduit;
        }

        public override string GetName() {
            return "Conduit";
        }

        public override bool ProjectsPower() {
            return true;
        }

        public override bool VisuallyConnects() {
            return true;
        }
    }
}
