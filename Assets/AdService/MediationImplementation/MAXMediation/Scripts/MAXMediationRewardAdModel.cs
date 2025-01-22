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

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleAdLoaded();
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleFailedLoad();
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleAdClose();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            if (!string.Equals(_maxMediationRewardedID, adUnitId))
            {
                return;
            }

            HandleOnAdRewarded();
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