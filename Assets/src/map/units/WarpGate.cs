using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;

namespace game.map.units {
    class WarpGate : Building {
        public static readonly int MAX_POWER = 100;
        public static readonly int POWER_GEN = 5;
        public int power = 0;

        public override bool Powered() {
            return true;
        }

        public override bool ProjectsPower() {
            return true;
        }

        public override bool VisuallyConnects() {
            return true;
        }

        public override string GetName() {
            return "WarpGate";
        }

        public override void SpreadPower(PowerNetwork pn) {
            base.SpreadPower(pn);
            pn.warpgates++;
        }

        public override void PreTurn(Actor old, Actor cur) {
            if (pn != null) {
                if (pn.power > MAX_POWER) {
                    power += MAX_POWER;
                    pn.power -= MAX_POWER;
                } else {
                    power += pn.power;
                    pn.power = 0;
                }
            }
        }

        public override void NewTurn(Actor old, Actor cur) {
            if (a == cur) {
                pn.power += POWER_GEN;
            }
        }

        public override void PostTurn(Actor old, Actor cur) {
            if (pn != null) {
                pn.power += power;
                power = 0;
            }
        }


    }
}
