using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class InterstitialAdModelBase
    {
        protected Action<AdInfo> _onAdLoaded;
        protected Action<AdErrorInfo> _onAdLoadFail;
        protected Action<AdInfo> _onAdClose;
        protected Action<AdInfo> _onAdPaid;

        public abstract void Request();
        public abstract bool IsLoaded();
        public abstract bool TryShowAd();
        public void RegisterCallback(Action<AdInfo> onAdLoaded, Action<AdErrorInfo> onAdLoadFail, Action<AdInfo> onAdClose, Action<AdInfo> onAdPaid)
        {
            _onAdLoaded = onAdLoaded;
            _onAdLoadFail = onAdLoadFail;
            _onAdClose = onAdClose;
            _onAdPaid = onAdPaid;
        }

        protected virtual void HandleFailedLoad(AdErrorInfo adErrorInfo)
        {
            _onAdLoadFail?.Invoke(adErrorInfo);
        }

        protected virtual void HandleAdClose(AdInfo adInfo)
        {
            _onAdClose?.Invoke(adInfo);
        }

        protected virtual void HandleAdLoaded(AdInfo adInfo)
        {
            _onAdLoaded?.Invoke(adInfo);
        }

        protected void HandleOnAdPaid(AdInfo adInfo)
        {
            _onAdPaid?.Invoke(adInfo);
        }

    }
}