using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public class InterstitialAdController : AdControllerBase
    {
        protected InterstitialAdModelBase _interstitialAdModel;

        protected AdInfo _onClosedAdInfo;
        protected AdErrorInfo _onAdLoadFailInfo;

        protected Action _onCloseAd;

        protected InterstitialAdModelBase CreateModel()
        {
            return _adFactory.CreateInterstitialAdModel();
        }
        public override bool IsLoadedAd()
        {
            return _interstitialAdModel != null ? _interstitialAdModel.IsLoaded() : false;
        }

        public override void RequestAd()
        {
            if (_interstitialAdModel == null)
            {
                _interstitialAdModel = CreateModel();
                _interstitialAdModel.RegisterCallback(HandleAdLoadSuccess, HandleAdLoadFail, HandleAdClose);
            }
        }

        public void RegisterOnCloseAd(Action onCloseAd)
        {
            _onCloseAd += onCloseAd;
        }

        public bool ShowAd()
        {
            bool result = false;
            if (IsLoadedAd())
            {
                result = _interstitialAdModel.TryShowAd();
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

            //when ad loaded failed
            if (_onAdLoadFailInfo != null)
            {
                //TODO: handle ad eror info if needed

                //------------------------------
                _onAdLoadFailInfo = null;
                RequestNewAdAfterSeconds(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS);
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

        protected void HandleAdLoadSuccess(AdInfo adInfo)
        {
        }

        protected void ClearCallback()
        {
            _onCloseAd = null;
        }
    }
}