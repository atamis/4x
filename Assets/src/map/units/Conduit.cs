using System;
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
    }

	class ConduitModel : MonoBehaviour {
		private SpriteRenderer sp;
		Conduit cond;

		public void init(Conduit c) {
			this.cond = cond;
			sp = gameObject.AddComponent<SpriteRenderer>();
		}
	}

	class ConnectionModel : MonoBehaviour {
		private SpriteRenderer sp;
		Conduit cond;

		public void init(Conduit c) {
			this.cond = cond;
			sp = gameObject.AddComponent<SpriteRenderer>();
		}
	}
}
