using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map.units;
using game.map;
using UnityEngine;

namespace game.actor {
    class BuildBuildingsCommand : Command {
        Hex h;
        BuildingType t;

        public BuildBuildingsCommand(Actor a, Hex h, BuildingType t)
            : base(a) {
            this.h = h;
            this.t = t;
        }

        private Building addBuildingComponent(GameObject obj) {
            switch(t) {
                case BuildingType.WarpGate:
                    return obj.AddComponent<WarpGate>();
                case BuildingType.Conduit:
                    return obj.AddComponent<Conduit>();
                case BuildingType.Harvester:
                    return obj.AddComponent<Harvester>();
                default:
                    return obj.AddComponent<Building>();
            }
        }

        public override void Apply(WorldMap w) {
            Building b = addBuildingComponent(new GameObject("New Building"));

            b.init(a, h);
        }

        public override void Undo(WorldMap w) {
            var b = h.building;
            h.building = null;
            b.Destroy();
        }
    }
}
