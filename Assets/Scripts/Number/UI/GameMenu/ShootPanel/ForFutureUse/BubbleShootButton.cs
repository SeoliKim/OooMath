using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Number {
    public class BubbleShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform button = null;
        [SerializeField] private GameObject shadow = null;
        public event Action<Color32> ShootButtonRelease;
        private Color32 color;

        private GameObject player;
        private Thrower thrower;

        private void OnEnable() {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state) {
            if(state == GameState.GameOn) {
                color = button.GetComponent<Image>().color;
                shadow.SetActive(false);
                player = gameObject.GetComponentInParent<UIMessageReceiver>().player;
                thrower = player.GetComponent<Thrower>();
                
            }
        }
        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        public void OnPointerDown(PointerEventData eventData) {
            color = button.GetComponent<Image>().color;
            shadow.SetActive(true);
            thrower.SetThrowMode(true);
        }

        public virtual void OnPointerUp(PointerEventData eventData) {
            ShootButtonRelease?.Invoke(color);
            Debug.Log("ShootButtonRelease: " + this.color );
            thrower.SetThrowMode(false);
            shadow.SetActive(false);
            button.gameObject.SetActive(false);
        }

        
    }
}
