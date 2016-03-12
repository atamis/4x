using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;
using UnityEngine;

namespace game.world.buildings {
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
            required = type.BuildTotal();
		}

        public override BuildingType? getBuildingType() {
            return null;
        }

        public override string GetName() {
			return "Warp";
		}

		public override string GetTooltip() {
			StringBuilder b = new StringBuilder("Building: ");
			var w = (WarpingBuilding)h.building;
			b.Append("warping ");
			b.Append(w.type.ToString());
			b.Append(", ");
			b.Append(w.power);
			b.Append("/");
			b.Append(w.required);

			return b.ToString();
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
			case BuildingType.Purifier:
				return obj.AddComponent<Purifier>();
			default:
				return obj.AddComponent<Building>();
			}
		}
			
		public override void PreTurn(Actor old, Actor cur) {
			if (power >= required) {
                h.building = null;
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
                        var drain = Math.Min(type.BuildPerTurn(), pn.power);
						pn.power -= drain;
						power += drain;
					}
				}
			}
		}

		internal override void AddModel() {
			model = new GameObject("WarpingBuilding Model").AddComponent<WarpingBuildingModel>();
			model.init(this);
		}


		private class WarpingBuildingModel : BuildingModel {
			private static AudioClip clip = Resources.Load<AudioClip>("Audio/Buildings/Warping In 1");

			private static System.Random positionRand = new System.Random();

			AudioSource au;

			public override void init(Building b) {
				base.init(b);

				au = gameObject.AddComponent<AudioSource>();
				au.clip = clip;

				au.volume = 0.4f;
				au.spatialBlend = 1f;

				au.time = (float) positionRand.NextDouble() * 1.5f;

				au.loop = true;
			}

			void Start() {
				au.Play();
			}

		}
	}
}
