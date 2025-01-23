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
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialAdPaidEvent;
        }


        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleAdLoaded(adInfo);
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo maxErrorInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            AdErrorInfo adErrorInfo = MAXMediationAdInfoParser.ParseToBaseAdErrorInfo(maxErrorInfo);

            HandleFailedLoad(adErrorInfo);
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleAdClose(adInfo);
        }

        private void OnInterstitialAdPaidEvent(string adUnitId, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationInterstitialID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleOnAdPaid(adInfo);
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