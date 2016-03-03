using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.map.units {
    class PowerNetwork {
        public int power;
        public int warpgates;

        private static int counter = 0;
        public int id;

        public PowerNetwork() {
            id = counter++;
            power = 0;
        }
    }
}
