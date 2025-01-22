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

        protected bool _isOnClosedAd;
        protected bool _isOnAdLoadFail;

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
            if (_isOnClosedAd)
            {
                _isOnClosedAd = false;
                _onCloseAd?.Invoke();
                _onCloseAd = null;
                RequestNewAdAfterSeconds(INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS);
            }

            //when ad loaded failed
            if (_isOnAdLoadFail)
            {
                _isOnAdLoadFail = false;
                RequestNewAdAfterSeconds(MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS);
            }

        }

        protected virtual async void RequestNewAdAfterSeconds(int timeInMs)
        {
            await Task.Delay(timeInMs);

            RequestAd();
        }

        protected virtual void HandleAdClose()
        {
            _isOnClosedAd = true;
        }

        protected virtual void HandleAdLoadFail()
        {
            _isOnAdLoadFail = true;
        }

        protected void HandleAdLoadSuccess()
        {
        }

        protected void ClearCallback()
        {
            _onCloseAd = null;
        }
    }
}