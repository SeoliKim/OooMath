using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Number { 
public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
        [SerializeField] public int gameIndex;

    public GameState State;
    public static event Action<GameState> OnGameStateChanged;


    void Awake() {
        GameManagerInstance = this;
        UpdateGameState(GameState.PreGame);
    }

    public void Nullify() {
            OnGameStateChanged = null;
        }

    private void Start() {

    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.PreGame:
                break;
            case GameState.GameOn:
                break;
            case GameState.Fail:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log("update game state" + newState);
    }
    }

public enum GameState {
    PreGame,
    GameOn,
    Fail
}
}
