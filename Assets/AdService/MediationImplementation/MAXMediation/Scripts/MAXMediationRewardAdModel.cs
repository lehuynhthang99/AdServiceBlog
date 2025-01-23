using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    public class MAXMediationRewardAdModel : RewardAdModelBase
    {
        protected string _maxMediationRewardedID;
        public MAXMediationRewardAdModel(string maxMediationRewardedID)
        {
            _maxMediationRewardedID = maxMediationRewardedID;

            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleAdLoaded(adInfo);
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo maxErrorInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            AdErrorInfo adErrorInfo = MAXMediationAdInfoParser.ParseToBaseAdErrorInfo(maxErrorInfo);

            HandleFailedLoad(adErrorInfo);
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleAdClose(adInfo);
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo maxAdInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            AdInfo adInfo = MAXMediationAdInfoParser.ParseToBaseAdInfo(maxAdInfo);

            HandleOnAdRewarded(adInfo);
        }

        public override void Request()
        {
            MaxSdk.LoadRewardedAd(_maxMediationRewardedID);
        }

        public override bool TryShowAd()
        {
            if (MaxSdk.IsRewardedAdReady(_maxMediationRewardedID))
            {
                MaxSdk.ShowRewardedAd(_maxMediationRewardedID);
                return true;
            }

            return false;
        }

        public override bool IsLoaded()
        {
            return MaxSdk.IsRewardedAdReady(_maxMediationRewardedID);
        }
    }
}