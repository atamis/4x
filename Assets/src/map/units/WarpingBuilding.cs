﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;
using UnityEngine;

namespace game.map.units {
    class WarpingBuilding : Building {
        public int power = 0;
        public int required = 5;
        public float progress {
            get {
                return power / required;
            }
        }
        public BuildingType type;

        public void init(Actor a, Hex h, BuildingType type) {
            base.init(a, h);
            this.type = type;
        }

        public override string GetName() {
            return "Warp";
        }

        public override bool ProjectsPower() {
            return false;
        }

        public override bool VisuallyConnects() {
            return true;
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
            if (power >= required) {
                print("Building");
                Building b = addBuildingComponent(new GameObject("New " + type.ToString()));

                b.init(a, h);
                Destroy(this.gameObject);
            }
        }

        public override void PostTurn(Actor old, Actor cur) {
            if (cur == a) {
                // TODO: fix power draw (null dereference).
                if (pn != null) {
                    if (pn.power > 0 && pn.warpgates > 0) {
                        pn.warpgates--;
                        pn.power -= 1;
                        power += 1;
                    }
                }
            }
        }
    }
}
