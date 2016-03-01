﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.map.units {
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