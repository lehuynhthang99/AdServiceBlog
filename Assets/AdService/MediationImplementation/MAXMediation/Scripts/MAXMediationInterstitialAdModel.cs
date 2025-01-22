using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    public class MAXMediationInterstitialAdModel : InterstitialAdModelBase
    {
        protected string _maxMediationInterstitialID;
        public MAXMediationInterstitialAdModel(string maxMediationInterstitialID)
        {
            _maxMediationInterstitialID = maxMediationInterstitialID;

            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        }


        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            HandleAdLoaded();
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            HandleFailedLoad();
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            HandleAdClose();
        }

        public override bool IsLoaded()
        {
            return MaxSdk.IsInterstitialReady(_maxMediationInterstitialID);
        }

        public override void Request()
        {
            MaxSdk.LoadInterstitial(_maxMediationInterstitialID);
        }

        public override bool TryShowAd()
        {
            if (IsLoaded())
            {
                MaxSdk.ShowInterstitial(_maxMediationInterstitialID);
                return true;
            }

            return false;
        }
    }
}