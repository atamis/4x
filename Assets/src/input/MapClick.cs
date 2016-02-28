﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map;
using game.actor;
using game.map.units;

namespace game.input {
    class MapClick : MonoBehaviour {
        WorldMap w;
        Player p;
        private State s;
        private Hex selected;
        private BuildingType buildingType;

        SelectedModel model;

        private enum State {
            // Nothing's happening.
            Default,
            // We've selected a hex
            Selected,
            // We're moving a unit.
            Moving,
            // Select a building type
            SelectBuilding,
            // Build a building
            BuildBuilding
        }

        public void init(WorldMap w, Player p) {
            this.w = w;
            this.p = p;
            this.s = State.Default;
        }

        void Start() {
            model = new GameObject("Selection").AddComponent<SelectedModel>();
            model.init(this);
        }

        void OnGUI() {
            List<string> messages = new List<String>();
            messages.Add("Click to select a hex.");
            messages.Add("State: " + s.ToString());
            messages.Add("Press m to move units.");
            messages.Add("Press b to build buildings.");
            if (s == State.Selected) {
                messages.Add("You selected " + selected);
                messages.Add("Biome: " + selected.b.ToString());
                messages.Add("Passable: " + (selected.b.Passable() ? "yes" : "no"));
                messages.Add("Powered: " + (selected.powered ? "yes" : "no"));

                if (selected.building != null) {
                    var b = selected.building;
                    messages.Add("Building: " + b.GetName());
                    if (b.pn != null) {
                        messages.Add("Power: " + b.pn.power);
                        messages.Add("Network: " + b.pn.id);
                    }
                }

                if (selected.units.Count > 0) {
                    messages.Add("Units: " +
                        selected.units
                        .Select(unit => unit.ToString())
                        .Aggregate<String>((acc, str) => acc + ", " + str));
                }
            }

            if (s == State.SelectBuilding) {
                messages.Add("Select building: 1 Conduit");
                messages.Add("2 Harvester");
                messages.Add("3 WarpGate");
            }

            if (s == State.BuildBuilding) {
                messages.Add("Select hex to build " + buildingType.ToString());
            }

            for(int i = 0; i < messages.Count; i++) {
                GUI.Label(new Rect(10, 30 + i * 20, 300, 20), messages[i]);
            }
        }
        

        void Update() {
            // Each state method returns the next state;
            switch (s) {
                case State.Default:
                    s = UpdateDefault();
                    return;
                case State.Selected:
                    s = UpdateSelected();
                    return;
                case State.Moving:
                    s = UpdateMoving();
                    return;
                case State.SelectBuilding:
                    s = UpdateSelectBuilding();
                    return;
                case State.BuildBuilding:
                    s = UpdateBuildBuilding();
                    return;
            }
        }

        State UpdateDefault() {
            // Select a hex.
            if (Input.GetMouseButtonUp(0)) {
                Hex h = GetHexAtMouse();
                if (h != null) {
                    selected = h;
                    return State.Selected;
                }
            }

            return State.Default;
        }

        State UpdateSelected() {
            // This shouldn't happen, but better than null dereferences.
            if (selected == null) {
                s = State.Default;
            }

            if (Input.GetMouseButtonUp(0)) {
                Hex h = GetHexAtMouse();
                if (h != null) {
                    // Change the selected hex
                    selected = h;
                    return State.Selected;
                } else {
                    // Deselect.
                    return State.Default;
                }
            }

            // We're moving a unit.
            if (Input.GetKeyUp(KeyCode.M)) {
                if (selected.units.Count() < 1) {
                    return State.Selected;
                }
                return State.Moving;
            }

            if (Input.GetKeyUp(KeyCode.B)) {
                return State.SelectBuilding;
            }

            return State.Selected;
        }

        State UpdateMoving() {
            if (selected == null) {
                return State.Default;
            }

            // No longer moving.
            if (Input.GetKeyUp(KeyCode.M)) {
                return State.Selected;
            }

            if (Input.GetMouseButtonUp(0)) {
                Hex h = GetHexAtMouse();

                // Click outside the map to cancel.
                if (h == null) {
                    return State.Selected;
                }

                try {
                    p.AddCommand(new MoveCommand(p, selected.units.First(), h));
                } catch (Exception e) {
                    // Most likely when they tried to move further than they should.
                    print(e);
                }

                // Go back to the last hex we had selected.
                return State.Selected;
            }


            // Just wait.
            return State.Moving;
        }

        State UpdateSelectBuilding() {

            if (Input.GetKeyUp(KeyCode.Alpha1)) {
                buildingType = BuildingType.Conduit;
                return State.BuildBuilding;
            }

            if (Input.GetKeyUp(KeyCode.Alpha2)) {
                buildingType = BuildingType.Harvester;
                return State.BuildBuilding;
            }

            if (Input.GetKeyUp(KeyCode.Alpha3)) {
                buildingType = BuildingType.WarpGate;
                return State.BuildBuilding;
            }


            return State.SelectBuilding;
        }

        State UpdateBuildBuilding() {
            if (Input.GetMouseButtonUp(0)) {
                Hex h = GetHexAtMouse();
                p.AddCommand(new BuildBuildingsCommand(p, h, buildingType));

                return State.Selected;
            }

            return State.BuildBuilding;
        }

        private Hex GetHexAtMouse() {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HexLoc l = w.l.PixelHex(worldPos);
            if (w.map.ContainsKey(l)) {
                Hex h = w.map[l];
                print("You selected " + h);
                return h;
            }
            return null;
        }

        private class SelectedModel : MonoBehaviour {
            MapClick m;
            SpriteRenderer sp;

            public void init(MapClick m) {
                this.m = m;
            }

            void Start() {
                sp = gameObject.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/Circle");
                sp.color = new Color(1.0f, 1.0f, 0.0f, 0.3f);
                sp.enabled = false;
            }

            void Update() {
                if (m.s == State.Selected && m.selected != null) {
                    transform.parent = m.selected.gameObject.transform;
                    // Have to set local position each time because changing
                    // transform parent doesn't move the game object.
                    transform.localPosition = new Vector3(0, 0, 0);
                    sp.enabled = true;
                } else {
                    sp.enabled = false;
                }
            }

        }
    }
}
