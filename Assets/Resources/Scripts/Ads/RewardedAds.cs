using UnityEngine;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
        LoadAd(); 
    }

    // Call this public method when you want to get an ad ready to show.
    private void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        _showAdButton.onClick.AddListener(ShowAd);
        _showAdButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        IronSource.Agent.showRewardedVideo(_adUnitId);
        IronSourceRewardedVideoEvents.onAdRewardedEvent += IronSourceRewardedVideoEvents_onAdRewardedEvent;
        //Cooldown
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