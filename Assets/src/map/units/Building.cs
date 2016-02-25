using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor;

namespace game.map.units {
    class Building : MonoBehaviour {
        public Hex h {
            get;
            internal set;
        }

        public Actor a;

        private BuildingModel model;

        public void init(Actor a, Hex h) {
            this.a = a;
            h.building = this;

            transform.parent = h.gameObject.transform;
            transform.localPosition = new Vector3(0, 0, 0);

            AddModel();
        }

        internal virtual void AddModel() {
            model = new GameObject("Building Model").AddComponent<BuildingModel>();
            model.init(this);
        }

        public virtual string GetName() {
            return "Building";
        }

        public virtual void NewTurn(Actor old, Actor cur) {
            
        }

        class BuildingModel : MonoBehaviour {
            Building b;
            SpriteRenderer sp;

            public void init(Building b) {
                this.b = b;

                transform.parent = b.gameObject.transform;
                transform.localPosition = new Vector3(0, 0, -1);

                sp = gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/" + b.GetName());

            }
        }

    }
}
