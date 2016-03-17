using game.actor;
using game.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.world.buildings {
    public static class ObjectExtensions {
        public static T Tap<T, Y>(this T o, Func<T, Y> func) {
            func.Invoke(o);
            return o;
        }
    }

    class AnnihilationManager : MonoBehaviour {
        //private Actor a;
        private WorldMap w;
        private State state;
        private List<WarpGate> gates;
        private float lastSearch;

        private GameObject model;
        private List<LineRenderer> lines;
        private int curLine;

        private LineRenderer mainBeam;
        private Queue<Hex> hexQueue;

        //private Color beamColor = new Color(1f, 0.41f, 0.3f, 0.8f);
        private Color beamColor = new Color(1f, 0f, 0f, 0.5f);

        private enum State {
            Searching, Connecting, Annihilating
        }

        public void init(WorldMap w, Actor a) {
            this.w = w;
            //this.a = a;
            this.state = State.Searching;
            lastSearch = 0;
        }

        void Update() {
            switch(state) {
                case State.Searching:
                    state = updateSearching();
                    break;
                case State.Connecting:
                    state = updateConnecting();
                    break;
                case State.Annihilating:
                    state = updateAnnihilating();
                    break;
            }
        }

        private State updateAnnihilating() {
            if (hexQueue.Count == 0) {
                mainBeam.enabled = false;
                return State.Annihilating; // THE ANNIHILATION NEVER STOPS
            }
            Hex h = hexQueue.Dequeue();
            if (h.annihilated) {
                return updateAnnihilating();
            }

            h.annihilated = true;
            h.reveal();
            mainBeam.SetPosition(1, setZ(w.l.HexPixel(h.loc)));
            if (h.miasma != null) {
                h.miasma.Die();
            }

            foreach(Hex n in h.Neighbors()) {
                if (!n.annihilated) {
                    hexQueue.Enqueue(n);
                }
            }

            return State.Annihilating; // THE ANNIHILATION NEVER STOPS
        }

        private void startAnnihilating() {
            hexQueue = new Queue<Hex>();

            foreach (WarpGate wg in gates) {
                hexQueue.Enqueue(wg.h);
            }

            var vecs = gates.Select(wg => setZ(w.l.HexPixel(wg.h.loc)));
            var total = Vector3.zero;
            var avg = Vector3.zero;
            int i = 0;

            foreach (Vector3 vec in vecs) {
                total += vec;
                i++;
                avg = total / (float)i;
            }

            
            mainBeam = new GameObject("God Beam").AddComponent<LineRenderer>();
            mainBeam.useWorldSpace = true;
            mainBeam.SetWidth(0.2f, 0.2f);

            mainBeam.SetPosition(0, avg);
            mainBeam.SetPosition(1, avg);

            var mat = mainBeam.material;
            mat.color = new Color(0.84f, 0.12f, 0.15f);
            mat.shader = Shader.Find("Sprites/Default");

        }


        private State updateConnecting() {
            if (curLine >= lines.Count) {
                startAnnihilating();
                return State.Annihilating;
            }
            var lr = lines[curLine++];
            lr.enabled = true;
            return State.Connecting;
        }

        private void startConnecting() {
            model = new GameObject("Annihilation Beams");
            lines = new List<LineRenderer>(gates.Count * gates.Count);

            foreach (WarpGate wg1 in gates) {
                foreach (WarpGate wg2 in gates) {
                    addLine(wg1, wg2);
                }
            }
            curLine = 0;
        }

        private void addLine(WarpGate wg1, WarpGate wg2) {
            var o = new GameObject("Annihilation Beam Model");
            o.transform.parent = model.transform;
            var lr = o.AddComponent<LineRenderer>();
            lr.useWorldSpace = true;
            lr.SetWidth(0.2f, 0.2f);

            lr.SetPosition(0, setZ(w.l.HexPixel(wg1.h.loc)));
            lr.SetPosition(1, setZ(w.l.HexPixel(wg2.h.loc)));

            var mat = lr.material;
            mat.color = beamColor;
            mat.shader = Shader.Find("Sprites/Default");
            lr.enabled = false;

            lines.Add(lr);
        }

        private Vector3 setZ(Vector2 v) {
            return new Vector3(v.x, v.y, Layer.BuildingFX);
        }

        private State updateSearching() {
            if (Time.frameCount < lastSearch + 10) {
                return State.Searching;
            }

            lastSearch = Time.frameCount;

            var tempGates = new List<WarpGate>(6);

            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                var h = kv.Value;
                if (h.building != null && h.building.GetType() == typeof(WarpGate)) {
                    var wg = (WarpGate)h.building;
                    tempGates.Add(wg);
                }
            }

            if (tempGates.Count() >= 6) {
                gates = tempGates;
                startConnecting();
                return State.Connecting;
            }
			return State.Searching;
        }
    }
}
