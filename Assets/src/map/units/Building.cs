using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.actor;

namespace game.map.units {
    public enum BuildingType {
        Conduit, Harvester, WarpGate
    }

    class Building : MonoBehaviour {
        public bool grided;
        public Hex h {
            get;
            internal set;
        }

        public PowerNetwork pn;

        public Actor a;

        private BuildingModel model;

        public virtual void init(Actor a, Hex h) {
            this.a = a;
            this.h = h;
            h.building = this;
            this.grided = false;
            pn = null;

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

        public virtual bool Powered() {
            return h.powered;
        }

        public virtual bool ProjectsPower() {
            return false;
        }

        public virtual bool VisuallyConnects() {
            return false;
        }

        internal void Destroy() {
            Destroy(gameObject);
        }

        public virtual void SpreadPower(PowerNetwork pn) {
            this.grided = true;

            if (this.pn == null) {
                this.pn = pn;
            }

            if (Powered() && ProjectsPower()) {
                HashSet<Hex> hexes = h.Neighbors().Aggregate(new HashSet<Hex>(),
                    (HashSet<Hex> acc, Hex hs) => {
                        acc.Add(h);
                        foreach (Hex n in hs.Neighbors()) {
                            acc.Add(n);
                        }
                        return acc;
                    });

                foreach(Hex hs in hexes) {
                    hs.pn = pn;
                }

                foreach(Hex hs in hexes) {
                    if (hs.building != null && !hs.building.grided) {
                        if (VisuallyConnects() && hs.building.VisuallyConnects()) {
                            h.w.bm.Add(h.loc, hs.loc);
                        }

                        hs.building.SpreadPower(pn);
                    }
                }
            }
        }

        public virtual void PreTurn(Actor old, Actor cur) {
            
        }


        public virtual void NewTurn(Actor old, Actor cur) {
            if (a == cur) {

            }
        }


        public virtual void PostTurn(Actor old, Actor cur) {
            
        }

        public void SelfDestruct() {
            Destroy(model);
            h.building = null;
            Destroy(gameObject);
        }

        class BuildingModel : MonoBehaviour {
            Building b;
            SpriteRenderer sp;

            public void init(Building b) {
                this.b = b;

                transform.parent = b.gameObject.transform;
                transform.localPosition = new Vector3(0, 0, Layer.Buildings);

                sp = gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/T_" + b.GetName());

            }
        }

    }
}
