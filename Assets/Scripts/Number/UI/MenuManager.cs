using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class MenuManager : MonoBehaviour {
        [SerializeField]
        private GameObject _PreGameMenu, _GameMenu,  _FailMenu;

        void Awake() {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            _GameMenu.SetActive(false);
        }


        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

        private void GameManager_OnGameStateChanged(GameState state) {
            //_PreGameMenu.SetActive(state == GameState.PreGame);
            
            _GameMenu.SetActive(state == GameState.GameOn);
            
            _FailMenu.SetActive(state == GameState.Fail);
        }
    }

}
