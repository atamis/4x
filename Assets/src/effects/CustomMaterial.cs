using UnityEngine;
using System.Collections;

namespace game.effects {
	class CustomMaterial : Material {

		public CustomMaterial(Shader s) : base(s) {

		}

		public virtual void tick (params float[] list) {

		}
	}
}
