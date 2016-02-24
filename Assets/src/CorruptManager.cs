using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using game.map;

// Handles the behavior for the Corruption
namespace game {
	class CorruptManager : MonoBehaviour {
		WorldMap w;
		private int aggression;
		private int MAP_WIDTH = 10;
		private int MAP_HEIGHT = 10;

		public void init (WorldMap w) {
			this.w = w;
			aggression = 1;
		}

		// Returns the percent chance a tile will be infected
		public float infectChance(){
			switch(aggression){
				case 0: 
					return .015f;
					break;
				case 1: 
					return .03f;
					break;
				case 2:
					return .08f;
					break;
				case 3: 
					return .2f;
					break;
			}
			return 0f;
		}

		public void spawnSpores(){
			//TODO
		}

		public void spawnUnits(){
			//TODO
		}

		public void spreadCreep(){
			List<Hex> tilesToTurn = new List<Hex>();
			for (int q = 0; q <= MAP_WIDTH; q++) {
                for (int r = 0; r <= MAP_HEIGHT; r++) {
                    HexLoc loc = new HexLoc(q, r, -q - r);
                    Hex tile = w.map[loc];
                    if(tile.b == Biome.Corruption){
                    	foreach (Hex t in tile.Neighbors()){
                    		if(t.b == Biome.Passable) tilesToTurn.Add(t);
                    	}
                    }
                }
			}
			foreach (Hex tile in tilesToTurn){
				float rand = UnityEngine.Random.value;
				if (rand < infectChance()){
					tile.b = Biome.Corruption;
				}
			}
		}

		//Call when the next turn for corruption should be processed
		public void processTurn(){
			spreadCreep();
			spawnSpores();
			spawnUnits();
		}
	}
}
