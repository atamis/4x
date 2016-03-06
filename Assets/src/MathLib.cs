using UnityEngine;
using System.Collections;

namespace game.math {
	
	public class HeightMap {
		private int MAPSIZE;
		private float[,] map;

		public float[,] genHeightMap(int size) {
			this.map = new float[size, size];

			square_diamond (size);

			return map;
		}

		float getValue(int x, int y) {
			if (x < 0 || x > MAPSIZE || y < 0 || y > MAPSIZE) 
				return -1;
			return this.map[x, y];
		}

		void square_diamond(int size) {
			int halfstep = size / 2;
			if (halfstep <= 1) {
				return;
			}

			for (int y = 0; y < MAPSIZE; y += size) {
				for (int x = 0; x < MAPSIZE; x += size) {
					map [x, y] = square (x, y, halfstep);
				}
			}

			for (int y = 0; y < MAPSIZE; y += halfstep) {
				for (int x = (y + halfstep) % size; x < MAPSIZE; x += size) {
					map [x, y] = diamond (x, y, halfstep);
				}
			}
			square_diamond (size / 2);
		}

		float diamond(int x, int y, int size) {
			//print ("x: " + x + " y: " + (y - size));
			float _a = getValue(x - size, y - size);
			float _b = getValue(x + size, y - size);
			float _c = getValue(x + size, y + size);
			float _d = getValue(x - size, y + size);

			return Random.Range (-0.5f, 0.5f) + ((_a + _b + _c + _d) / 4.0f);
		}

		float square(int x, int y, int size) {
			//print ("x: " + x + "y: " + y);
			float _a = map [x, y];
			float _b = map [x + size, y];
			float _c = map [x, y + size];
			float _d = map [x + size, y + size];

			return Random.Range (-0.5f, 0.5f) + ((_a + _b + _c + _d) / 4.0f);
		}
	}
}
