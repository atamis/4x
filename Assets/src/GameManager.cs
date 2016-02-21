using UnityEngine;
using System.Collections.Generic;
using game.map;

namespace game {
    public class GameManager : MonoBehaviour {

        PlayerCamera pc;
        private HashSet<HexLoc> hexes;
        private Layout l;

        // Use this for initialization
        void Start() {

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);


            hexes = new HashSet<HexLoc>();
            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));

            HexLoc l = new HexLoc(0, 0, 0);
            print(l);
            print((l + HexLoc.Direction(0)));
            print(l.Neighbor(0));

            for (int i = 0; i < 10; i++) {
                print(l.Neighbor(i));
            }


            for (int q = 0; q <= 10; q++) {
                for (int r = 0; r <= 10; r++) {
                    hexes.Add(new HexLoc(q, r, -q - r));
                }
            }
        }
            




        // Update is called once per frame
        void Update() {
            foreach (Transform children in transform) {
                Destroy(children.gameObject);
            }


            foreach (HexLoc h in hexes) {
                GameObject o = new GameObject(h.ToString());
                o.transform.parent = gameObject.transform;
                o.transform.localPosition = l.HexPixel(h);

                var sp = o.AddComponent<SpriteRenderer>();
                sp.sprite = Resources.Load<Sprite>("Textures/Hexagon");
            }
        }
    } 
}