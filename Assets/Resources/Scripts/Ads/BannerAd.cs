using System;
using UnityEngine;
using UnityEngine.UI;


public class BannerAd : MonoBehaviour
{
    [SerializeField] Button _hideBannerButton;

    void Start()
    {
        _hideBannerButton.gameObject.SetActive(false);

        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM, "Banner Android"); 
    }

    private void BannerOnAdScreenPresentedEvent(IronSourceAdInfo obj)
    {
    }

    private void BannerOnAdLeftApplicationEvent(IronSourceAdInfo obj)
    {
    }

    private void BannerOnAdScreenDismissedEvent(IronSourceAdInfo obj)
    {
    }

    private void BannerOnAdClickedEvent(IronSourceAdInfo obj)
    {
    }

    private void BannerOnAdLoadFailedEvent(IronSourceError obj)
    {
        Debug.LogError(obj.getDescription()); 
    }

    private void BannerOnAdLoadedEvent(IronSourceAdInfo obj)
    {
        Debug.LogWarning("Banner loaded");

        _hideBannerButton.onClick.AddListener(HideBannerAd);
        _hideBannerButton.gameObject.SetActive(true);
        _hideBannerButton.interactable = true;

        ShowBannerAd();
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        IronSource.Agent.displayBanner();
    }

    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd() => IronSource.Agent.hideBanner(); 

    void OnDestroy()
    {
        _hideBannerButton.onClick.RemoveAllListeners();
        IronSource.Agent.destroyBanner(); 
    }
}