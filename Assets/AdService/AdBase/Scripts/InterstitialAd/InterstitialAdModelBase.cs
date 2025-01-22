using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class InterstitialAdModelBase
    {
        protected Action _onAdLoaded;
        protected Action _onAdLoadFail;
        protected Action _onAdClose;

        public abstract void Request();
        public abstract bool IsLoaded();
        public abstract bool TryShowAd();
        public void RegisterCallback(Action onAdLoaded, Action onAdLoadFail, Action onAdClose)
        {
            _onAdLoaded = onAdLoaded;
            _onAdLoadFail = onAdLoadFail;
            _onAdClose = onAdClose;
        }

        protected virtual void HandleFailedLoad()
        {
            _onAdLoadFail?.Invoke();
        }

        protected virtual void HandleAdClose()
        {
            _onAdClose?.Invoke();
        }

        protected virtual void HandleAdLoaded()
        {
            _onAdLoaded?.Invoke();
        }
    }
}