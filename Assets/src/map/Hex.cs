using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map.units;
using game.actor;

namespace game.map {
    public enum Biome {
        Passable, Impassable, Corruption
    }

    public static class BiomeExtensions {
        public static Biome Toggle(this Biome b) {
            if (b == Biome.Impassable) {
                return Biome.Passable;
            } else if(b == Biome.Passable){
                return Biome.Corruption;
            } else {
                return Biome.Impassable;
            }
        }
     }

    class Hex : MonoBehaviour {
        WorldMap w;
        public HexLoc loc;
        public Biome b;

        public HashSet<Unit> units;

        HexModel model;

        public void init(WorldMap w, HexLoc loc) {
            this.w = w;
            this.loc = loc;
            this.b = Biome.Passable;

            units = new HashSet<Unit>();

            transform.localPosition = w.l.HexPixel(loc);

            var o = new GameObject("Hex Model");
            o.transform.parent = transform;
            o.transform.localPosition = new Vector2(0, 0);
            model = o.AddComponent<HexModel>();
            model.init(this);
        }

        public void NewTurn(Actor old, Actor cur) {
            foreach(Unit u in units) {
                u.NewTurn(old, cur);
            }
        }

        public List<Hex> Neighbors() {
            List<Hex> n = new List<Hex>();
            
            for(int i = 0; i < 6; i++) {
                HexLoc l = loc.Neighbor(i);
                if (w.map.ContainsKey(l)) {
                    n.Add(w.map[l]);
                }
            }

            return n;
        }

        void Start() {

        }

        void Update() {
        }

        private class HexModel : MonoBehaviour {
            public Hex h;
            SpriteRenderer sp;

            public void init(Hex h) {
                this.h = h;

                sp = gameObject.AddComponent<SpriteRenderer>();

                sp.sprite = Resources.Load<Sprite>("Textures/Hexagon");
                sp.transform.localScale = new Vector3(1.9f, 1.9f);
            }

            void Update() {
                if (h.b == Biome.Impassable) {
                    sp.color = new Color(0.2f, 0.2f, 0.2f);
                } else if (h.b == Biome.Corruption){
                    sp.color = new Color(.5f, 0f, .5f);
                } else {
                    sp.color = new Color(1f, 1f, 1f);
                }
            }
        }
    }
}
