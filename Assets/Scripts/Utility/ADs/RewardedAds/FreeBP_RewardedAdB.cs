using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBP_RewardedAdB : RewardedAdsButton {
    protected override void GrantAdBonus() {
        PlayFabManager.PlayFabManagerInstance.AddBP(1);
    }
}
