using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Number {
    public class BubbleButtonManager : MonoBehaviour {

        public event Action<Color32> ShootButtonRelease;
        private Color32 color;

        private GameObject player;
        private Thrower thrower;

        private void OnEnable() {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            player = gameObject.GetComponentInParent<UIMessageReceiver>().player;
            thrower = player.GetComponent<Thrower>();
            thrower.SetBubbleButtonManager(this);
            if (!(GameManager.GameManagerInstance.State== GameState.GameOn)) {
                Debug.LogError("Some error in receiving player and thrower, current Gamestate is " + GameManager.GameManagerInstance.State + "PLayer is " + player);
            } 
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state) {
             if (state == GameState.GameOn) {
                   
             }
        }



        public void ButtonDown() {
            thrower.SetThrowMode(true);
        }
        
        public void ButtonClick(GameObject button) {
            color = button.GetComponent<Image>().color;
            ShootButtonRelease?.Invoke(color);
                Debug.Log("ShootButtonRelease: " + this.color);
            }
    }
}
