using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.map;

namespace game.actor {
    class SpreadCorruptionCommand : Command {
        private int aggression;
        List<Hex> tilesTurned;

        public SpreadCorruptionCommand(Actor a, int aggression) : base(a) {
            this.aggression = aggression;
            tilesTurned = new List<Hex>();
        }

        // Returns the percent chance a tile will be infected
        public float infectChance() {
            switch (aggression) {
                case 0:
                    return .015f;
                case 1:
                    return .03f;
                case 2:
                    return .08f;
                case 3:
                    return .2f;
            }
            return 0f;
        }

        public override void Apply(WorldMap w) {
            HashSet<Hex> tilesToTurn = new HashSet<Hex>();
            int i = 0;
            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                Hex tile = kv.Value;
                if (tile.corrupted) {
                    i++;
                    foreach (Hex t in tile.Neighbors()) {
                        if (t.b.Passable()) tilesToTurn.Add(t);
                    }
                }

            }
            

            foreach (Hex tile in tilesToTurn) {
                float rand = UnityEngine.Random.value;
                if (rand < infectChance()) {
                    tile.corrupted = true;
                    tilesTurned.Add(tile);
                }
            }

        }

        public override void Undo(WorldMap w) {
            foreach(Hex h in tilesTurned) {
                h.corrupted = false;
            }
        }
    }
}
