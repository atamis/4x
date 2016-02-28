using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game {
    // GUI is its own magic unity layer.

    class Layer {
        public readonly static int Board = 0;
        public readonly static int Corruption = -1;
        public readonly static int Buildings = -2;
        public readonly static int Units = -3;
        public readonly static int UnitsFX = -4;
        public readonly static int BuildingFX = -5;
    }

    class LayerV {
        public readonly static Vector3 Board = new Vector3(0, 0, Layer.Board);
        public readonly static Vector3 Corruption = new Vector3(0, 0, Layer.Corruption);
        public readonly static Vector3 Buildings = new Vector3(0, 0, Layer.Buildings);
        public readonly static Vector3 Units = new Vector3(0, 0, Layer.Units);
        public readonly static Vector3 UnitsFX = new Vector3(0, 0, Layer.UnitsFX);
        public readonly static Vector3 BuildingFX = new Vector3(0, 0, Layer.BuildingFX);
    }
}
