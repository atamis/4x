﻿using UnityEngine;
using System.Collections.Generic;
using System;
using game.actor;
using game.actor.commands;
using game.math;
using game.world;
using game.world.units;
using game.world.buildings;

namespace game.ui {
	class UIManager : MonoBehaviour {
		private static Texture2D UI_Move = Resources.Load<Texture2D>("Textures/T_UI_Move");
		private static Texture2D UI_Scan = Resources.Load<Texture2D>("Textures/T_UI_Scan");
		private static Texture2D UI_Build = Resources.Load<Texture2D>("Textures/T_UI_Build");
		private static Texture2D UI_Purify = Resources.Load<Texture2D>("Textures/T_UI_Purify");
		private static Texture2D UI_MoveH = Resources.Load<Texture2D>("Textures/T_UI_MoveH");
		private static Texture2D UI_ScanH = Resources.Load<Texture2D>("Textures/T_UI_ScanH");
		private static Texture2D UI_BuildH = Resources.Load<Texture2D>("Textures/T_UI_BuildH");
		private static Texture2D UI_PurifyH = Resources.Load<Texture2D>("Textures/T_UI_PurifyH");
		private static Texture2D UI_MoveC = Resources.Load<Texture2D>("Textures/T_UI_MoveC");
		private static Texture2D UI_ScanC = Resources.Load<Texture2D>("Textures/T_UI_ScanC");
		private static Texture2D UI_BuildC = Resources.Load<Texture2D>("Textures/T_UI_BuildC");
		private static Texture2D UI_PurifyC = Resources.Load<Texture2D>("Textures/T_UI_PurifyC");
		private static Texture2D UI_End = Resources.Load<Texture2D>("Textures/T_UI_End");
		private static Texture2D UI_EndH = Resources.Load<Texture2D>("Textures/T_UI_EndH");
		private static Texture2D UI_EndC = Resources.Load<Texture2D>("Textures/T_UI_EndC");

		private GUIStyle ButtonStyle;

		WorldMap w;
		Player p;

		TooltipUI t;
		public HelperUI helper;
		public bool ev_view = true;

		HighlightModel model;

		Hex h_target;
		Unit u_target;
		State state;
		//bool building;

		public enum State {
			Default,
			Selected,
			Moving,
			Building,
		};

		public void init(Player player, WorldMap w) {
			this.p = player;
			this.w = w;

			t = gameObject.AddComponent<TooltipUI> ();
			t.init (this);

			model = new GameObject ("Highlight Model").AddComponent<HighlightModel> ();
			model.init ();

			state = State.Default;
			//building = false;
		}

		void Start () {
			helper = gameObject.AddComponent<HelperUI>();
			helper.init();
		}
			
		void Update () {
			if (Input.GetMouseButtonUp (0)) {
				if ((state == State.Default) || (state == State.Selected)) {
					Hex h = GetHexAtMouse ();
					if (h != null) {
						this.h_target = h;
					}
					state = State.Selected;

				} else if (state == State.Moving) {
					Hex h = GetHexAtMouse ();
					if (h != null) {
						this.h_target = h;
					}

				} else if (state == State.Building) {

				}
			}
		}

		public Hex GetHexAtMouse() {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			HexLoc l = w.l.PixelHex (worldPos);
			if (w.map.ContainsKey (l)) {
				Hex h = w.map [l];
				return h;
			}
			return null;
		}

		private Hex getSelected() {
			return h_target;
		}

		void OnGUI() {
			GUILayout.BeginArea(new Rect (Screen.width*.3f, Screen.height*.8f, Screen.width/2, Screen.height*.9f));
			GUILayout.BeginHorizontal ();

			ButtonStyle = new GUIStyle(GUI.skin.label);

			ButtonStyle.normal.background = UI_Move; ButtonStyle.hover.background = UI_MoveH; ButtonStyle.active.background = UI_MoveC;
			if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .13f))) {
				if (state == State.Selected) {
					if (h_target.unit != null) {
						u_target = h_target.unit;
						state = State.Moving;
					}
				} else if (state == State.Moving) {
					try {
						p.AddCommand (new MoveCommand (p, u_target, getSelected ()));
					} catch (Exception e) { 
						print (e);
					}
					state = State.Default;
					Debug.Log ("Added Move Command");
				} else {
					state = State.Default;
				}

			}

			ButtonStyle.normal.background = UI_Build; ButtonStyle.hover.background = UI_BuildH; ButtonStyle.active.background = UI_BuildC;
			if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .13f))){
				if (state == State.Selected) {
					if (h_target.unit != null) {
						u_target = h_target.unit;
						state = State.Building;
						Debug.Log ("Switched to Build State");
					}
				} else if (state == State.Building) {
					state = State.Default;
				}
			}

			ButtonStyle.normal.background = UI_Scan; ButtonStyle.hover.background = UI_ScanH; ButtonStyle.active.background = UI_ScanC;
			if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .13f))) {
				if (state == State.Selected) {
					if (h_target.unit != null) {
						try {
							p.AddCommand (new ScanCommand (p, h_target.unit, h_target));
						} catch (Exception e) {
							Debug.Log (e);
						}

					}
				}
				Debug.Log ("Added Scan Command");
			}

			ButtonStyle.normal.background = UI_Purify; ButtonStyle.hover.background = UI_PurifyH; ButtonStyle.active.background = UI_PurifyC;
			if (GUILayout.Button("", ButtonStyle, GUILayout.Width(Screen.width * .08f), GUILayout.Height(Screen.height * .13f))) {
				if (state == State.Selected) {
					if (h_target.unit != null) {
						p.AddCommand (new CleanseCommand (p));
					}
				}
				Debug.Log ("Added Cleanse Command");
			}

			ButtonStyle.normal.background = UI_End; ButtonStyle.hover.background = UI_EndH; ButtonStyle.active.background = UI_EndC;
			if (GUILayout.Button ("", ButtonStyle, GUILayout.Width (Screen.width * .12f), GUILayout.Height (Screen.height * .13f))) {
				p.AddCommand(new EndTurnCommand(p));
				Debug.Log ("Added End Turn Command");

			}
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();

			if (state == State.Building) {
				GUILayout.BeginArea (new Rect (Screen.width * .3f, Screen.height * .7f, Screen.width / 2, Screen.height * .9f));
				GUILayout.BeginHorizontal ();

				if (GUILayout.Button ("Cond", GUILayout.Width (Screen.width * 0.035f), GUILayout.Height (Screen.height * 0.08f))) {
					try {
						p.AddCommand(new BuildCommand(p, u_target, h_target, BuildingType.Conduit));
					} catch (Exception e) {
						print (e);
					}
					state = State.Default;
				}

				if (GUILayout.Button ("Harv", GUILayout.Width (Screen.width * 0.035f), GUILayout.Height (Screen.height * 0.08f))) {
					try {
						p.AddCommand(new BuildCommand(p, u_target, h_target, BuildingType.Harvester));
					} catch (Exception e) {
						print (e);
					}
					state = State.Default;
				}

				if (GUILayout.Button ("Tow", GUILayout.Width (Screen.width * 0.035f), GUILayout.Height (Screen.height * 0.08f))) {
					try {
						p.AddCommand(new BuildCommand(p, u_target, h_target, BuildingType.Conduit));
					} catch (Exception e) {
						print (e);
					}
					state = State.Default;
				}

				if (GUILayout.Button ("Warp", GUILayout.Width (Screen.width * 0.035f), GUILayout.Height (Screen.height * 0.08f))) {
					try {
						p.AddCommand(new BuildCommand(p, u_target, h_target, BuildingType.WarpGate));
					} catch (Exception e) {
						print (e);
					}
					state = State.Default;
				}
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
			}
		}

		class HighlightModel : MonoBehaviour {
			SpriteRenderer sp;

			public void init() {

			}

			void Start() {
				sp = gameObject.AddComponent<SpriteRenderer> ();
				sp.sprite = Resources.Load<Sprite> ("Textures/T_Highlight");
				sp.color = new Color (1.0f, 1.0f, 1.0f, 0.3f);
				sp.enabled = false;
			}

			void Update() {
				sp.enabled = true;
			}
		}
	}
}

