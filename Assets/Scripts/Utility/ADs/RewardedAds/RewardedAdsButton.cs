using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
    [SerializeField] GameObject _ShowAdsButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    
    public void SetRewardedAdsButton() {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        //Disable the button until the ad is ready to show:
        _ShowAdsButton.SetActive(false);
    }

    // Load content to the Ad Unit:
    public void LoadAd() {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId + "Advertisement.isInitialized: " + Advertisement.isInitialized);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId) {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId)) {
            // Enable the button for users to click:
            _ShowAdsButton.SetActive(true);
        }
    }

    // Implement a method to execute when the user clicks the button:
    //** Add to Event Trigger
    public void ShowAd() {
        // Disable the button:
        _ShowAdsButton.SetActive(true);
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            GrantAdBonus();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
    }

    protected virtual void GrantAdBonus() {
        //override grant bonus after completing ad
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

}
