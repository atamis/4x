using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.actor;
using game.map.units;
using UnityEngine;

namespace game.map {
    class WorldMap : MonoBehaviour{
        public Dictionary<HexLoc, Hex> map;
        public Layout l;

        public void init(Layout l) {
            this.l = l;

            map = new Dictionary<HexLoc, Hex>();

            for (int q = 0; q <= 10; q++) {
                for (int r = 0; r <= 10; r++) {
                    HexLoc hl = new HexLoc(q, r, -q - r);
                    map.Add(hl, makeHex(hl));
                }
            }
            map[new HexLoc(10, 10, -20)].corrupted = true;
        }

        private Hex makeHex(HexLoc l) {
            var o = new GameObject(l.ToString());
            o.transform.parent = transform;
            Hex h = o.AddComponent<Hex>();
            h.init(this, l);

            return h;
        }
        
        void Start() {

        }

        void Update() {

        }

        public void NewTurn(Actor old, Actor cur) {
            List<Building> poweredBuildings = new List<Building>();
            foreach (KeyValuePair<HexLoc, Hex> kv in map) {
                kv.Value.powered = false;
                var b = kv.Value.building;
                if (b != null) {
                    b.grided = false;
                    if (b.Powered()) {
                        poweredBuildings.Add(b);
                    }
                }
            }

            foreach (KeyValuePair<HexLoc, Hex> kv in map) {
                kv.Value.NewTurn(old, cur);
            }

            foreach(Building b in poweredBuildings) {
                b.PowerGrid();
            }
        }
    }
}
