using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.map {
    class BeamManager {
        private WorldMap wm;
        public HashSet<UnorderedPair<HexLoc>> conns;
        private List<BeamModel> models;

        public BeamManager(WorldMap worldMap) {
            this.wm = worldMap;
            this.conns = new HashSet<UnorderedPair<HexLoc>>();
            this.models = new List<BeamModel>();
        }

        public void Add(HexLoc a, HexLoc b) {
            conns.Add(new UnorderedPair<HexLoc>(a, b));
        }

        public void Clear() {
            conns = new HashSet<UnorderedPair<HexLoc>>();

            foreach (BeamModel m in models) {
                m.SelfDestruct();
            }

            models = new List<BeamModel>();
        }

        public void Create() {

            foreach(UnorderedPair<HexLoc> h in conns) {
                var m = new GameObject("BeamModel").AddComponent<BeamModel>();
                m.init(this, h);
                models.Add(m);
            }
        }

        private class BeamModel : MonoBehaviour {
            private BeamManager bm;
            Layout l;
            private UnorderedPair<HexLoc> locs;
            LineRenderer lr;

            public void init(BeamManager bm, UnorderedPair<HexLoc> locs) {
                this.bm = bm;
                this.l = bm.wm.l;
                this.locs = locs;

                lr = gameObject.AddComponent<LineRenderer>();
                lr.useWorldSpace = true;
                lr.SetWidth(0.1f, 0.1f);
                var mat = lr.material;
                mat.color = new Color(0.46f, 1, 0.9f, 0.7f);
                mat.shader = Shader.Find("Sprites/Default");
            }

            void Update() {
                Vector2[] _vecs = (from loc in locs.ToVec() select l.HexPixel(loc)).ToArray();
                Vector3[] vecs = (from v in _vecs select new Vector3(v.x, v.y, Layer.BuildingFX)).ToArray();

                lr.SetPositions(vecs);
            }

            public void SelfDestruct() {
                Destroy(gameObject);
            }
        }
    }

    public class UnorderedPair<T> {
        public T a;
        public T b;

        public UnorderedPair(T a, T b) {
            this.a = a;
            this.b = b;
        }

        public override int GetHashCode() {
            int ah = a.GetHashCode();
            int bh = b.GetHashCode();

            return ah << 2 ^ bh << 4;
        }

        public override bool Equals(object o) {
            if (o.GetType() != typeof(UnorderedPair<T>)) {
                return false;
            }

            var x = (UnorderedPair<T>) o;

            return (x.a.Equals(a) && x.b.Equals(b)) || (x.a.Equals(b) && x.b.Equals(a));
        }

        public T[] ToVec() {
            return new T[] { a, b };
        }

    }
}
