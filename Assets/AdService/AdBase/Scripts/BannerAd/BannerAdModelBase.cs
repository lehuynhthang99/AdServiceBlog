using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class BannerAdModelBase
    {
        protected Action _onAdLoaded;
        protected Action _onAdLoadFail;

        public abstract void Request();
        public abstract bool TryShowAd();
        public abstract bool IsLoaded();
        public abstract void Destroy();
        public abstract void Hide();

        public void RegisterCallback(Action onAdLoaded, Action onAdLoadFail)
        {
            _onAdLoaded = onAdLoaded;
            _onAdLoadFail = onAdLoadFail;
        }

        protected virtual void HandleFailedLoad()
        {
            _onAdLoadFail?.Invoke();
        }

        protected virtual void HandleAdLoaded()
        {
            _onAdLoaded?.Invoke();
        }
    }
}