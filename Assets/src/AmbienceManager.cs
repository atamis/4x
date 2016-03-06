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
        private static readonly AudioClip jungle = Resources.Load<AudioClip>("Audio/Ambiences/Jungle Ambience");
        private static readonly AudioClip plains = Resources.Load<AudioClip>("Audio/Ambiences/Plains Ambience");

        private static readonly float masterGain = 1f;

        public AudioSource desertAU;
        public AudioSource forestAU;
        public AudioSource mountainAU;
        public AudioSource oceanAU;
        public AudioSource jungleAU;
        public AudioSource plainsAU;

        PlayerCamera pc;
        WorldMap w;


        public void init(PlayerCamera pc, WorldMap w) {
            this.pc = pc;
            this.w = w;

            desertAU = gameObject.AddComponent<AudioSource>();
            forestAU = gameObject.AddComponent<AudioSource>();
            mountainAU = gameObject.AddComponent<AudioSource>();
            oceanAU = gameObject.AddComponent<AudioSource>();
            jungleAU = gameObject.AddComponent<AudioSource>();
            plainsAU = gameObject.AddComponent<AudioSource>();

            desertAU.clip = desert;
            forestAU.clip = forest;
            mountainAU.clip = mountain;
            oceanAU.clip = ocean;
            jungleAU.clip = jungle;
            plainsAU.clip = plains;

            desertAU.spatialBlend = forestAU.spatialBlend = mountainAU.spatialBlend = oceanAU.spatialBlend = jungleAU.spatialBlend = plainsAU.spatialBlend = 0f;
            desertAU.volume = forestAU.volume = mountainAU.volume = oceanAU.volume = jungleAU.volume = plainsAU.volume = 1f;
            desertAU.loop = forestAU.loop = mountainAU.loop = oceanAU.loop = jungleAU.loop = plainsAU.loop = true;
        }

        void Start() {
            desertAU.Play();
            forestAU.Play();
            mountainAU.Play();
            oceanAU.Play();
            jungleAU.Play();
            plainsAU.Play();
        }

        void Update() {
            HexLoc look = w.l.PixelHex(pc.transform.position);

            int desertC = 0;
            int forestC = 0;
            int mountainC = 0;
            int oceanC = 0;
            int jungleC = 0;
            int plainsC = 0;

            int total = 0;

            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                if (kv.Key.Distance(look) < 5) {
                    total++;
                    switch (kv.Value.b) {
                        case Biome.Highlands:
                            mountainC++;
                            break;
                        case Biome.Plains:
                            plainsC++;
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
                            jungleC++;
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

            //print(String.Format("desert {0}, forerst {1}, mountain {2}, ocean {3}, total {4}", desertC / total, forestC / total, mountainC / total, oceanC / total, total));

            var t = (float) total;

            desertAU.volume = desertC / t * masterGain * 0.8f;
            forestAU.volume = forestC / t * masterGain;
            mountainAU.volume = mountainC / t * masterGain;
            oceanAU.volume = oceanC / t * masterGain;
            jungleAU.volume = jungleC / t * masterGain;
            plainsAU.volume = plainsC / t * masterGain;

        }
    }
}
