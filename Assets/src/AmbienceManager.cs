using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map;
using game.input;

namespace game {
    class AmbienceManager : MonoBehaviour {
        private static readonly AudioClip desert = Resources.Load<AudioClip>("Audio/Ambiences/Desert Ambience");
        private static readonly AudioClip forest = Resources.Load<AudioClip>("Audio/Ambiences/Forest Ambience");
        private static readonly AudioClip mountain = Resources.Load<AudioClip>("Audio/Ambiences/Mountain Ambience");
        private static readonly AudioClip ocean = Resources.Load<AudioClip>("Audio/Ambiences/Water Ambience");

        public AudioSource desertAU;
        public AudioSource forestAU;
        public AudioSource mountainAU;
        public AudioSource oceanAU;

        PlayerCamera pc;
        WorldMap w;


        public void init(PlayerCamera pc, WorldMap w) {
            this.pc = pc;
            this.w = w;

            desertAU = gameObject.AddComponent<AudioSource>();
            forestAU = gameObject.AddComponent<AudioSource>();
            mountainAU = gameObject.AddComponent<AudioSource>();
            oceanAU = gameObject.AddComponent<AudioSource>();

            desertAU.clip = desert;
            forestAU.clip = forest;
            mountainAU.clip = mountain;
            oceanAU.clip = ocean;

            desertAU.spatialBlend = forestAU.spatialBlend = mountainAU.spatialBlend = oceanAU.spatialBlend = 0f;
            desertAU.volume = forestAU.volume = mountainAU.volume = oceanAU.volume = 1f;
            desertAU.loop = forestAU.loop = mountainAU.loop = oceanAU.loop = true;
        }

        void Start() {
            desertAU.Play();
            forestAU.Play();
            mountainAU.Play();
            oceanAU.Play();
        }

        void Update() {
            HexLoc look = w.l.PixelHex(pc.transform.position);

            int desertC = 0;
            int forestC = 0;
            int mountainC = 0;
            int oceanC = 0;

            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                if (kv.Key.Distance(look) < 5) {
                    switch (kv.Value.b) {
                        case Biome.Highlands:
                            mountainC++;
                            break;
                        case Biome.Plains:
                            break;
                        case Biome.Forest:
                            forestC++;
                            break;
                        case Biome.Ocean:
                            oceanC++;
                            break;
                        case Biome.Desert:
                            desertC++;
                            break;
                        case Biome.Jungle:
                            break;
                        default:
                            break;
                    }

                }
            }
            
            // Number of hexes 5 distance away from the center hex.
            // Includes center hex (probably).
            // When all tiles contribute some sound, this can be
            // all the counts added together.
            float total = 71;

            print(String.Format("desert {0}, forerst {1}, mountain {2}, ocean {3}, total {4}", desertC / total, forestC / total, mountainC / total, oceanC / total, total));

            desertAU.volume = desertC / total;
            forestAU.volume = forestC / total;
            mountainAU.volume = mountainC / total;
            oceanAU.volume = oceanC / total;

        }
    }
}
