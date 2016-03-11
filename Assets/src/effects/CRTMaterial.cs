using UnityEngine;
using System.Collections;

namespace game.effects {
	class CRTMaterial : CustomMaterial {

		public CRTMaterial(Shader s) : base(s) {

		}

		public override void tick (params float[] list) {
			this.SetFloat ("_evValue", list[0]);
		}
	}
}
