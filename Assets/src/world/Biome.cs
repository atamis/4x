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

		public static Color GetColor(this Biome b) {
			switch (b) {
			case Biome.Highlands:
				return new Color (.56f, .63f, .51f, 1);
			case Biome.Plains:
				return new Color (.72f, .72f, .72f, 1);
			case Biome.Forest:
				return new Color (.23f, .52f, .14f, 1);
			case Biome.Ocean:
				return new Color (.17f, .35f, .58f, 1);
			case Biome.Desert:
				return new Color (.77f, .51f, .25f, 1);
			case Biome.Jungle:
				return new Color (.08f, .23f, .11f, 1);
			default:
				return new Color(1, 1, 1, 0);
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
