using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

namespace Calculation {
    public class CalcRound_RewardedAdB : RewardedAdsButton {
        protected override void GrantAdBonus() {
            RoundManager.RoundManagerInstance.DestroyCurrentGame();
            GameManager.GameManagerInstance.UpdateGameState(GameState.GetReady);
        }

    }
}
