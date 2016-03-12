using game.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.world {
    class WorldPathfinding {
        public class PathfindingInfo {
            public bool evaluated = false;
            public float gScore = float.PositiveInfinity;
            public float fScore = float.PositiveInfinity;
            public Hex cameFrom = null;

            public override string ToString() {
                return String.Format("[PI gscore={0} fscore={1} from={2}]", gScore, fScore, cameFrom);
            }
        }

        public static LinkedList<Hex> Pathfind(WorldMap w, Hex start, Hex goal) {
            if (!goal.b.Passable()) {
                return null;
            }
            try {
                List<Hex> openHexes = new List<Hex>();
                start.pathfind = new PathfindingInfo();
                start.pathfind.gScore = 0;
                start.pathfind.fScore = fHeuristic(start, goal);
                openHexes.Add(start);

                while (openHexes.Count() > 0) {
                    Hex cur = removeLowestFScore(openHexes);
                    cur.pathfind.evaluated = true;

                    if (cur == goal) {
                        return reconstructPath(goal);
                    }

                    foreach (Hex n in cur.Neighbors()) {
                        var tentativeGscore = cur.pathfind.gScore + 1; // All neighbors are 1 apart.

                        if (n.pathfind == null) {
                            n.pathfind = new PathfindingInfo();
                        }

                        if (n.b.Passable() && tentativeGscore < n.pathfind.gScore) {
                            openHexes.Add(n);
                            //UnityEngine.Debug.Log(n.b.Passable() + " " + n.pathfind.ToString() + " " + tentativeGscore.ToString());

                            n.pathfind.cameFrom = cur;
                            n.pathfind.gScore = tentativeGscore;
                            n.pathfind.fScore = n.pathfind.gScore + fHeuristic(n, goal);

                        }
                    }
                }

                return null;
            } finally {
                foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                    kv.Value.pathfind = null;
                }
            }

        }


        private static LinkedList<Hex> reconstructPath(Hex hex) {
            if (hex.pathfind.cameFrom != null) {
                var ll = reconstructPath(hex.pathfind.cameFrom);
                ll.AddLast(hex);
                return ll;
            } else {
                var ll = new LinkedList<Hex>();
                ll.AddFirst(hex);
                return ll;
            }
        }

        private static float fHeuristic(Hex start, Hex goal) {
            return start.loc.Distance(goal.loc);
        }

        private static Hex removeLowestFScore(List<Hex> lst) {
            Hex lowest = lst.First();
            foreach (Hex h in lst) {
                if (h.pathfind != null && h.pathfind.fScore < lowest.pathfind.fScore) {
                    lowest = h;
                }
            }

            lst.Remove(lowest);

            return lowest;

        }
    }
}
