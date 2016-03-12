using UnityEngine;
using System.Collections;

namespace game.world {
	public enum Biome {
		Highlands, Plains, Forest, Desert, Jungle,

		// MUST BE LAST ASK NICK
		Ocean,
	}

	public static class BiomeExtensions {
		private static Sprite hexagon = Resources.Load<Sprite>("Textures/Hexagon");
		private static Sprite desert = Resources.Load<Sprite>("Textures/T_Hex_Desert");
		private static Sprite forest = Resources.Load<Sprite>("Textures/T_Hex_Forest");
		private static Sprite highlands = Resources.Load<Sprite>("Textures/T_Hex_Highlands");
		private static Sprite jungle = Resources.Load<Sprite>("Textures/T_Hex_Jungle");
		private static Sprite ocean = Resources.Load<Sprite>("Textures/T_Hex_Ocean");
		private static Sprite plains = Resources.Load<Sprite>("Textures/T_Hex_Plains");

		public static Sprite GetSprite(this Biome b) {
			switch (b) {
			case Biome.Highlands:
				return highlands;
			case Biome.Plains:
				return plains;
			case Biome.Forest:
				return forest;
			case Biome.Ocean:
				return ocean;
			case Biome.Desert:
				return desert;
			case Biome.Jungle:
				return jungle;
			default:
				return hexagon;
			}
		}

		public static float Dropoff(this Biome b) {
			switch (b) {
			case Biome.Highlands:
				return 0.5f;
			case Biome.Plains:
				return 0.3f;
			case Biome.Forest:
				return 0.25f;
			case Biome.Ocean:
				return 1f;
			case Biome.Desert:
				return 0.3f;
			case Biome.Jungle:
				return 0.25f;
			default:
				return 0f;
			}

		}

		public static bool Passable(this Biome b) {
			return b.PassCost() != -1;
		}

		public static int PassCost(this Biome b) {
			switch(b) {
			case Biome.Highlands:
				return 1;
			case Biome.Plains:
				return 1;
			case Biome.Forest:
				return 1;
			case Biome.Ocean:
				return -1;
			case Biome.Desert:
				return 1;
			case Biome.Jungle:
				return 1;
			default:
				return -1;
			}
		}
	}
}
