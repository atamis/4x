using UnityEngine;
using System.Collections;

namespace game.world.buildings {

    class Conduit : Building {
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
