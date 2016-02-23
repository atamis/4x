using UnityEngine;
using System.Collections.Generic;
using game.map;
using game.input;

namespace game {
    public class GameManager : MonoBehaviour {

        PlayerCamera pc;
        private Layout l;
        private WorldMap w;
        MapClick mc;
        CorruptManager cm;

        // Use this for initialization
        void Start() {

            this.l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));


            w = new GameObject("World Map").AddComponent<WorldMap>();
            w.init(l);

            pc = new GameObject("Player Camera").AddComponent<PlayerCamera>();
            pc.init(Camera.main);

            mc = new GameObject("Map Click").AddComponent<MapClick>();
            mc.init(w);

            cm = new GameObject("Corruption Manager").AddComponent<CorruptManager>();
            cm.init(this);

            
        }
            




        // Update is called once per frame
        void Update() {
            
        }
    } 
}