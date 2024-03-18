using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] protected int gameType;
    //0=calculation, 1= number
    public int GetGameType() {
        return gameType;
    }

    [SerializeField] protected int gameIndex;
    public int GetGameIndex() {
        return gameIndex;
    }

    [SerializeField] protected int sceneIndex;
    public int GetSceneIndex() {
        return sceneIndex;
    }

    [SerializeField] protected int bPCost =1;
    public int GetBPCost() {
        return bPCost;
    }


}
