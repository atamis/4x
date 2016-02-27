using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.map.units {
    class WarpGate : Building {
        public override bool Powered() {
            return true;
        }

        public override bool ProjectsPower() {
            return true;
        }

        public override string GetName() {
            return "WarpGate";
        }


    }
}
