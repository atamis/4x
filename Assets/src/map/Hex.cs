using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map.units;
using game.actor;

namespace game.map {
    public enum Biome {
        Highlands, Plains, Forest, Desert, Jungle,

        // MUST BE LAST ASK NICK
        Ocean,
    }

    public static class BiomeExtensions {
        private static Sprite hexagon = Resources.Load<Sprite>("Textures/Hexagon");
        private static Sprite desert = Resources.Load<Sprite>("Textures/T_Hex_Desert");
        private static Sprite forest = Resources.Load<Sprite>("Textures/T_Hex_Forest");
        private static Sprite highlands = Resources.Load<Sprite>("Textures/T_Hex_Highlands");
        private static Sprite jungle = Resources.Load<Sprite>("Textures/T_Hex_Jungle");
        private static Sprite ocean = Resources.Load<Sprite>("Textures/T_Hex_Ocean");
        private static Sprite plains = Resources.Load<Sprite>("Textures/T_Hex_Plains");

        public static Sprite GetSprite(this Biome b) {
            switch (b) {
                case Biome.Highlands:
                    return highlands;
                case Biome.Plains:
                    return plains;
                case Biome.Forest:
                    return forest;
                case Biome.Ocean:
                    return ocean;
                case Biome.Desert:
                    return desert;
                case Biome.Jungle:
                    return jungle;
                default:
                    return hexagon;
            }
        }

        public static float Dropoff(this Biome b) {
            switch (b) {
                case Biome.Highlands:
                    return 1f;
                case Biome.Plains:
                    return 1f;
                case Biome.Forest:
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
                case Biome.Forest:
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
		public bool scanned;

        public bool powered {
            get {
                return pn != null;
            }
        }

        public void init(WorldMap w, HexLoc loc) {
            this.w = w;
            this.loc = loc;
            this.b = Biome.Plains;
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
                
                sp.transform.localScale = new Vector3(1.9f, 1.9f);
            }

            void Update() {
				sp.sprite = h.b.GetSprite(); // make sure we have the right biome sprite

				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
					if (h.scanned == true) {
						sp.color = new Color (h.ev / 10f, 1, 1, 1f);
					} else {
						sp.color = new Color (1, 1, 1);
					}
				} else {
					sp.color = Color.white;
				}
                
				/*
                if (h.corrupted) {
                    sp.color = new Color(0.5f, 0, 0.5f);
                }
                */
            }
        }
    }
}
