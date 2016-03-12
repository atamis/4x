using UnityEngine;
using game.actor;
using game.world.units;

namespace game.world.buildings {
	class WarpGate : Building {

        public static readonly int MAX_POWER = 200;
		public static readonly int POWER_GEN = 5;
		public static readonly int UNIT_COST = 5; // In turns
		public int power = 0;

		public int unitQueue = 0;
		// In turns.
		public int progress = 0;

		public bool BuildingUnit() {
			return unitQueue > 0;
		}

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

		public override string GetTooltip() {
			if (BuildingUnit()) {
				return GetName() + "(" + unitQueue + " units, " + progress + "/" + UNIT_COST + ")";
			}
			return base.GetTooltip();
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
				if (BuildingUnit()) {
					model.sp.sprite = Resources.Load<Sprite> ("Textures/T_WarpGateOn");
					progress++;
					if (progress == UNIT_COST) {
						progress = 0;
						unitQueue--;

						var u = new GameObject("Warped Unit").AddComponent<Unit>();
						u.init(a, h.wm, h);

					}
				} else {
					model.sp.sprite = Resources.Load<Sprite> ("Textures/T_WarpGate");
					pn.power += POWER_GEN;
				}
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