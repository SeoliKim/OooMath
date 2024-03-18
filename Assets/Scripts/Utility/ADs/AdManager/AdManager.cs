using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{

    #region Rewarded Ads
    [SerializeField] protected RewardedAdsButton rewardedAdsButton;

    protected void SetRewardedAdsButton() {
        rewardedAdsButton.SetRewardedAdsButton();
        rewardedAdsButton.LoadAd();
    }
    #endregion


}
