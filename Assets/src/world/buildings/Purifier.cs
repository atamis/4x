using UnityEngine;
using System.Collections;
using game.actor;
using System.Collections.Generic;


namespace game.world.buildings {

    class Purifier: Building {

        public override bool ProjectsPower() {
            return false;
        }

        public override bool VisuallyConnects() {
            return true;
        }

        public override void PreTurn(Actor old, Actor cur) {
            if(this.Powered()){
                List<Hex> miasmaNeighbors = new List<Hex> ();
                foreach (Hex t in h.Neighbors()){
                    if (t.miasma != null){
                        miasmaNeighbors.Add(t);
                        print("Adding corrupted tile");
                    }
                }
                if(miasmaNeighbors.Count != 0){
                    miasmaNeighbors[(int)Random.Range(0f, (float) miasmaNeighbors.Count-.01f)].miasma.Die();
                }
            }
        }

        public override string GetName() {
			return "Purifier";
		}
    }
}
