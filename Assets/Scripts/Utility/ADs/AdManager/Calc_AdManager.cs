using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Calculation {
    public class Calc_AdManager : AdManager {

        int count;
        private InterstitialAd InterstitialAd;
        private void Awake() {
            InterstitialAd = gameObject.AddComponent<CalcGame_InterstitialAd>();
        }
        protected void Start() {
            count = 0;
            InterstitialAd.LoadAd();
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state) {
            if(state== GameState.LevelPass) {
                count++;
                if(count>2 && count % 3 == 0) {
                    InterstitialAd.ShowAd();
                }
            }
            if(state == GameState.Fail) {
                if (count > 1) {
                    InterstitialAd.ShowAd();
                }
                SetRewardedAdsButton();
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}
