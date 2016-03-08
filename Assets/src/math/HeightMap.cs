using UnityEngine;
using System;
using System.Collections;
using System.Text;

namespace game.math {
	static class HeightMap {
		public static float[,] genHeightMap(int size) {
			float[,] map = new float[size+1, size+1];
			return square_diamond (size, map);
		}

		private static float[,] square_diamond(int mapsize, float[,] map) {
			int size = mapsize;
			int halfstep = mapsize / 2;

			while (halfstep > 1) {
				for (int y = 0; y < mapsize; y += size) {
					for (int x = 0; x < mapsize; x += size) {
						map [x, y] = square (x, y, halfstep, mapsize, map);
					}
				}

				for (int y = 0; y < mapsize; y += halfstep) {
					for (int x = (y + halfstep) % size; x < mapsize; x += size) {
						map [x, y] = diamond (x, y, halfstep, mapsize, map);
					}
				}
				size = size / 2;
				halfstep = size / 2;
			}
			return map;
		}

		private static float getValue(int x, int y, int mapsize, float[,] map) {
			//UnityEngine.Debug.Log (String.Format("{0} {1} {2}", x, y, mapsize));
			if (x < 0 || x > mapsize || y < 0 || y > mapsize)
				return -1;
			return map[x, y];
		}

		private static float diamond(int x, int y, int size, int mapsize, float[,] map) {
			//print ("x: " + x + " y: " + (y - size));
			float _a = getValue(x - size, y - size, mapsize, map);
			float _b = getValue(x + size, y - size, mapsize, map);
			float _c = getValue(x + size, y + size, mapsize, map);
			float _d = getValue(x - size, y + size, mapsize, map);

			return UnityEngine.Random.Range (-0.5f, 0.5f) + ((_a + _b + _c + _d) / 4.0f);
		}

		private static float square(int x, int y, int size, int mapsize, float[,] map) {
			//print ("x: " + x + "y: " + y);
			float _a = map [x, y];
			float _b = map [x + size, y];
			float _c = map [x, y + size];
			float _d = map [x + size, y + size];

			return UnityEngine.Random.Range (-0.5f, 0.5f) + ((_a + _b + _c + _d) / 4.0f);
		}
	}
}
