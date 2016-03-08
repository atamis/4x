/*
 * generates the world
 * tracks the day-night time
 *
 *
 *
 */

/*
 * TODO
 * ocean boundries
 * corruption seeds
 * start biome
 * original node
 * river / ocean Biomes (Ocean Boundry, River Terrain)
 */

using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.actor;
using game.world;
using game.world.buildings;
using game.world.units;

namespace game {
    class WorldManager {
		GameManager gm;
		WorldMap wm;
		Player player;
        public int ticks { get; set; } // tracks whether it is day or night

		public WorldManager(GameManager gm, WorldMap wm, Player player) {
			this.gm = gm;
			this.wm = wm;

            this.ticks = 0;
        }
    }
}
