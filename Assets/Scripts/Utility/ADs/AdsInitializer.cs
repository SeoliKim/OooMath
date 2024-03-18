using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener {
    string _androidGameId= "4876162";
    string _iOSGameId = "4876163";
    [SerializeField] bool _testMode;
    private string _gameId;

    void Awake() {
        InitializeAds();
    }

    public void InitializeAds() {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        if (Advertisement.isSupported && !Advertisement.isInitialized) {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete() {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

    

