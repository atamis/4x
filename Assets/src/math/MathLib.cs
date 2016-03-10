using UnityEngine;
using System.Collections.Generic;

namespace game.math {
	static class MathLib {
		public static Vector2 GetNearest(Vector2 point, List<Vector2> points) {
			Vector2 nearest = new Vector2(0, 0);
			float prev = Mathf.Infinity;
			foreach (Vector2 v in points) {
				float distance = Vector2.Distance (point, v);
				//print("Distance from " + point + " to " + v + ":" + distance);
				if (distance < prev) {
					nearest = v;
					prev = distance;
				}
			}
			return nearest;
		}
	}  
}
