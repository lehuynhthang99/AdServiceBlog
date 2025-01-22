using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public class BannerAdController : AdControllerBase
    {
        protected BannerAdModelBase _bannerAdModel;

        protected bool _isOnAdLoadFail;

        protected BannerAdModelBase CreateModel()
        {
            return _adFactory.CreateBannerAdModel();
        }
        public override bool IsLoadedAd()
        {
            return _bannerAdModel != null ? _bannerAdModel.IsLoaded() : false;
        }

        public override void RequestAd()
        {
            if (_bannerAdModel == null)
            {
                _bannerAdModel = CreateModel();
                _bannerAdModel.RegisterCallback(HandleAdLoadSuccess, HandleAdLoadFail);
            }
        }

        protected virtual void Update()
        {
            //when banner loading failed
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

        protected virtual void HandleAdLoadFail()
        {
            _isOnAdLoadFail = true;
        }

        protected void HandleAdLoadSuccess()
        {
        }
    }
}