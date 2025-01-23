using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public class InterstitialAdController : AdControllerBase<InterstitialAdModelBase>
    {

        protected AdInfo _onClosedAdInfo;
        protected AdInfo _onAdPaidInfo;
        protected AdErrorInfo _onAdLoadFailInfo;

        protected Action _onCloseAd;

        protected InterstitialAdModelBase CreateModel()
        {
            return _adModelFactory.CreateAdModel();
        }
        public override bool IsLoadedAd()
        {
            return _adModel != null ? _adModel.IsLoaded() : false;
        }

        public override void RequestAd()
        {
            if (_adModel == null)
            {
                _adModel = CreateModel();
                _adModel.RegisterCallback(HandleAdLoadSuccess, HandleAdLoadFail, HandleAdClose, HandleAdPaid);
            }
        }

        public void RegisterOnCloseAd(Action onCloseAd)
        {
            _onCloseAd += onCloseAd;
        }

        public bool TryShowAd()
        {
            bool result = false;
            if (IsLoadedAd())
            {
                result = _adModel.TryShowAd();
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

            //when ad paid
            if (_onAdPaidInfo != null)
            {
                //TODO: handle ad info if needed

                //------------------------------
                _onAdPaidInfo = null;
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

        protected virtual void HandleAdPaid(AdInfo adInfo)
        {
            _onAdPaidInfo = adInfo;
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