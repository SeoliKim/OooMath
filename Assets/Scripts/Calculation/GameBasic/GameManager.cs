using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace Calculation {
    public class GameManager : MonoBehaviour {
        [SerializeField]public int gameIndex;
        public static GameManager GameManagerInstance;

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
            User.I.AddDailyCalcGameTotal(gameIndex, 1);
        }

        public void UpdateGameState(GameState newState) {
            State = newState;

            switch (newState) {
                case GameState.PreGame:
                    StartCoroutine(HandlePreGame());
                    break;
                case GameState.GetReady:
                    break;
                case GameState.GameOn:
                    break;
                case GameState.LevelPass:
                    break;
                case GameState.Fail:
                    break;
            }
            OnGameStateChanged?.Invoke(newState);
            Debug.Log("update game state" + newState);
        }

        IEnumerator HandlePreGame() {
            yield return new WaitUntil(() => this.gameObject.GetComponent<RoundManager>().enabled);
            UpdateGameState(GameState.GetReady);
        }
    }

    public enum GameState {
        PreGame,
        GetReady,
        GameOn,
        LevelPass,
        Fail
    }
}

