using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class BannerAdModelBase
    {
        protected Action<AdInfo> _onAdLoaded;
        protected Action<AdInfo> _onAdPaid;
        protected Action<AdErrorInfo> _onAdLoadFail;

        public abstract void Request();
        public abstract bool TryShowAd();
        public abstract bool IsLoaded();
        public abstract void Destroy();
        public abstract void Hide();

        public void RegisterCallback(Action<AdInfo> onAdLoaded, Action<AdErrorInfo> onAdLoadFail, Action<AdInfo> onAdPaid)
        {
            _onAdLoaded = onAdLoaded;
            _onAdLoadFail = onAdLoadFail;
            _onAdPaid = onAdPaid;
        }

        protected virtual void HandleFailedLoad(AdErrorInfo adErrorInfo)
        {
            _onAdLoadFail?.Invoke(adErrorInfo);
        }

        protected virtual void HandleAdLoaded(AdInfo adInfo)
        {
            _onAdLoaded?.Invoke(adInfo);
        }

        protected virtual void HandleAdPaid(AdInfo adInfo)
        {
            _onAdPaid?.Invoke(adInfo);
        }
    }
}