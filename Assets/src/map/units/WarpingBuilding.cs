using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;
using UnityEngine;

namespace game.map.units {
    class WarpingBuilding : Building {
        public static readonly int BUILD_COST = 5;
        public int power = 0;
        public BuildingType type;

        public void init(Actor a, Hex h, BuildingType type) {
            base.init(a, h);
            this.type = type;
        }

        public override string GetName() {
            return "Warping";
        }

        public override bool ProjectsPower() {
            return false;
        }

        private Building addBuildingComponent(GameObject obj) {
            switch (type) {
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


        public override void PreTurn(Actor old, Actor cur) {
            if (power >= BUILD_COST) {
                print("Building");
                Building b = addBuildingComponent(new GameObject("New " + type.ToString()));

                b.init(a, h);
                Destroy(this.gameObject);
            }
        }

        public override void PostTurn(Actor old, Actor cur) {
            if (cur == a) {
                pn.power -= 1;
                power += 1;
                print("Now have " + power);
            }
        }
    }
}
