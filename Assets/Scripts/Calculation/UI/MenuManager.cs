using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class MenuManager : MonoBehaviour {
        [SerializeField]
        private GameObject _PreGameMenu, _GetReadyMenu, _GameMenu, _PassMenu, _FailMenu;


        void Awake() {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            _GetReadyMenu.SetActive(false);
            _GameMenu.SetActive(false);
            _PassMenu.SetActive(false);
            _FailMenu.SetActive(false);
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

        private void GameManager_OnGameStateChanged(GameState state) {
            //_PreGameMenu.SetActive(state == GameState.PreGame);
            _GetReadyMenu.SetActive(state == GameState.GetReady);
            _GameMenu.SetActive(state == GameState.GameOn);
            _PassMenu.SetActive(state == GameState.LevelPass);
            _FailMenu.SetActive(state == GameState.Fail);
        }


    }
}
