using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public class RewardAdController : AdControllerBase
    {
        protected RewardAdModelBase _rewardAdModel;
        
        protected AdInfo _onClosedAdInfo;
        protected AdErrorInfo _onAdLoadFailInfo;
        protected AdInfo _onAdWatchSuccessInfo;

        protected Action _onCloseAd;
        protected Action _onWatchAdSuccess;

        protected RewardAdModelBase CreateModel()
        {
            return _adFactory.CreateRewardAdModel();
        }

        public override bool IsLoadedAd()
        {
            return _rewardAdModel != null ? _rewardAdModel.IsLoaded() : false;
        }

        public override void RequestAd()
        {
            if (_rewardAdModel == null)
            {
                _rewardAdModel = CreateModel();
                _rewardAdModel.RegisterCallback(HandleAdLoadSuccess, HandleAdLoadFail, HandleAdClose, HandleAdLoadSuccess);
            }
        }

        public void RegisterOnCloseAd(Action onCloseAd)
        {
            _onCloseAd += onCloseAd;
        }
        
        public void RegisterOnWatchAdSuccess(Action onWatchAdSuccess)
        {
            _onWatchAdSuccess += onWatchAdSuccess;
        }

        public bool ShowAd()
        {
            bool result = false;
            if (IsLoadedAd())
            {
                result = _rewardAdModel.TryShowAd();
            }

            if (!result)
            {
                ClearCallback();
            }

            return result;
        }

        protected virtual void Update()
        {
            //when ad is closed
            if (_onClosedAdInfo != null)
            {
                //TODO: handle ad info if needed

                //------------------------------
                _onClosedAdInfo = null;
                _onCloseAd?.Invoke();
                _onCloseAd = null;
                RequestNewAdAfterSeconds(INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS);
            }

            //when ad loaded fail
            if (_onAdLoadFailInfo != null)
            {
                //TODO: handle ad error info if needed

                //------------------------------
                _onAdLoadFailInfo = null;
                RequestNewAdAfterSeconds(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS);
            }

            //when ad close
            if (_onClosedAdInfo != null)
            {
                //TODO: handle ad info if needed

                //------------------------------
                _onClosedAdInfo = null;
                _onCloseAd?.Invoke();
                _onCloseAd = null;
                RequestNewAdAfterSeconds(INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS);
            }

            //when reward ad watch success
            if (_onAdWatchSuccessInfo != null)
            {
                //TODO: handle ad info if needed

                //------------------------------
                _onAdWatchSuccessInfo = null;
                _onWatchAdSuccess?.Invoke();
                _onWatchAdSuccess = null;
            }

        }

        protected virtual async void RequestNewAdAfterSeconds(int timeInMs)
        {
            await Task.Delay(timeInMs);

            RequestAd();
        }

        protected virtual void HandleAdClose(AdInfo adInfo)
        {
            _onClosedAdInfo = adInfo;
        }

        protected virtual void HandleAdLoadFail(AdErrorInfo adErrorInfo)
        {
            _onAdLoadFailInfo = adErrorInfo;
        }

        protected virtual void HandleAdWatchSuccess(AdInfo adInfo)
        {
            _onAdWatchSuccessInfo = adInfo;
        }

        protected void HandleAdLoadSuccess(AdInfo adInfo)
        {

        }


        protected void ClearCallback()
        {
            _onCloseAd = null;
            _onWatchAdSuccess = null;
        }
    }
}