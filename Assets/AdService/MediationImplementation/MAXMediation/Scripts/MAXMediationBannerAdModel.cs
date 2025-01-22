using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    public class MAXMediationBannerAdModel : BannerAdModelBase
    {
        protected string _maxMediationBannerID;
        protected bool _isLoadedAd;

        public MAXMediationBannerAdModel(string maxMediationBannerID)
        {
            _maxMediationBannerID = maxMediationBannerID;

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnAdLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnAdFailedToLoad;
        }

        public override bool IsLoaded()
        {
            return _isLoadedAd;
        }

        private void OnAdLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationBannerID, adUnitId))
            {
                return;
            }

            _isLoadedAd = true;
            HandleAdLoaded();

            TryShowAd();
        }

        private void OnAdFailedToLoad(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (!string.Equals(_maxMediationBannerID, adUnitId))
            {
                return;
            }

            _isLoadedAd = false;
            HandleFailedLoad();
        }

        public override void Destroy()
        {
            MaxSdk.DestroyBanner(_maxMediationBannerID);
        }

        public override void Request()
        {
            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
            MaxSdk.CreateBanner(_maxMediationBannerID, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(_maxMediationBannerID, Color.white);
        }

        public override bool TryShowAd()
        {
            try
            {
                MaxSdk.ShowBanner(_maxMediationBannerID);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public override void Hide()
        {
            MaxSdk.HideBanner(_maxMediationBannerID);
        }

    }
}