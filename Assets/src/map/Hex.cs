using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map.units;
using game.actor;
using game.world;

namespace game.map {
    public enum Biome {
		Highlands, Plains, Forrest, Desert, Jungle, Ocean, Count
    }

    public static class BiomeExtensions {
        private static Sprite sprite = Resources.Load<Sprite>("Textures/Hexagon");

        public static Sprite GetSprite(this Biome b) {
            return sprite;
        }

        public static float Dropoff(this Biome b) {
            switch (b) {
                case Biome.Highlands:
                    return 1f;
                case Biome.Plains:
                    return 1f;
                case Biome.Forrest:
                    return 1f;
                case Biome.Ocean:
                    return 1f;
                case Biome.Desert:
                    return -1f;
                case Biome.Jungle:
                    return 1f;
                default:
                    return -1f;
            }

        }

        public static bool Passable(this Biome b) {
            return b.PassCost() != -1;
        }

        public static int PassCost(this Biome b) {
            switch(b) {
                case Biome.Highlands:
                    return 1;
                case Biome.Plains:
                    return 1;
                case Biome.Forrest:
                    return 1;
                case Biome.Ocean:
                    return 1;
                case Biome.Desert:
                    return -1;
                case Biome.Jungle:
                    return 1;
                default:
                    return -1;
            }
        }
     }

    class Hex : MonoBehaviour {
        WorldMap w;
        public HexLoc loc;

        public Biome b;

        // TODO: use enum instead?
        public bool corrupted;

        public HashSet<Unit> units;

        HexModel model;
        public Building building;
        public PowerNetwork pn;

		public float ev { get; set; }
		Node node;
		bool scanned;

        public bool powered {
            get {
                return pn != null;
            }
        }

        public void init(WorldMap w, HexLoc loc) {
            this.w = w;
            this.loc = loc;
            this.b = Biome.Forrest;
            this.corrupted = false;

            units = new HashSet<Unit>();

            transform.localPosition = w.l.HexPixel(loc);

            var o = new GameObject("Hex Model");
            o.transform.parent = transform;
            model = o.AddComponent<HexModel>();
            model.init(this);
        }

        internal void PreTurn(Actor old, Actor cur) {
            if (building != null) {
                building.PreTurn(old, cur);
            }

            foreach(Unit u in units) {
                u.PreTurn(old, cur);
            }
        }

        public void NewTurn(Actor old, Actor cur) {
            if (building != null) {
                building.NewTurn(old, cur);
            }
            foreach(Unit u in units) {
                u.NewTurn(old, cur);
            }
        }

        internal void PostTurn(Actor old, Actor cur) {
            if (building != null) {
                building.PostTurn(old, cur);
            }

            foreach (Unit u in units) {
                u.PostTurn(old, cur);
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

		public void scan() {
			this.node.setVisible();
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


                transform.localPosition = new Vector3(0, 0, Layer.Board);

                sp = gameObject.AddComponent<SpriteRenderer>();

                sp.sprite = Resources.Load<Sprite>("Textures/Hexagon");
                sp.transform.localScale = new Vector3(1.9f, 1.9f);
            }

            void Update() {
                Color c;
                switch(h.b) {
                case Biome.Highlands:
                    c = Color.gray;
                    break;
                case Biome.Plains:
                    c = Color.yellow;
                    break;
                case Biome.Forrest:
                    c = Color.green;
                    break;
                case Biome.Ocean:
                    c = Color.blue;
                    break;
                case Biome.Desert:
                    c = Color.white;
                    break;
                case Biome.Jungle:
                    c = Color.green;
                    break;
                default:
                    c = Color.black;
                    break;
                }

                if (h.corrupted) {
                    c = new Color(0.5f, 0, 0.5f);
                }

                sp.color = c;
            }
        }
    }
}
