using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null; // This will remain null for unsupported platforms

    void Awake()
    {
        IronSourceRewardedVideoEvents.onAdRewardedEvent += IronSourceRewardedVideoEvents_onAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += IronSourceRewardedVideoEvents_onAdAvailableEvent;
        IronSource.Agent.shouldTrackNetworkState(true);
        _showAdButton.interactable = false;
        LoadAd(); 
    }

    private void IronSourceRewardedVideoEvents_onAdAvailableEvent(IronSourceAdInfo obj)
    {

    }

    private void RewardedVideoOnAdShowFailedEvent(IronSourceError arg1, IronSourceAdInfo arg2)
    {
        Debug.LogError(arg1.getDescription()); 
    }

    private void Update()
    {
        bool available = IronSource.Agent.isRewardedVideoAvailable();
        _showAdButton.interactable = available; 
    }

    // Call this public method when you want to get an ad ready to show.
    private void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        _showAdButton.onClick.AddListener(ShowAd);
        _showAdButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Then show the ad:
        IronSource.Agent.showRewardedVideo(_adUnitId);
    }

    private void IronSourceRewardedVideoEvents_onAdRewardedEvent(IronSourcePlacement arg1, IronSourceAdInfo arg2)
    {
        PlayerObject.singleton.Player.Coins += 100;
    }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}