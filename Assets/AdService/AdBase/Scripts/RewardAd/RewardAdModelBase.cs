using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class RewardAdModelBase
    {
        protected Action _onAdRewarded;
        protected Action _onAdLoadFail;
        protected Action _onAdClose;
        protected Action _onAdLoaded;
        
        public abstract bool TryShowAd();
        public abstract void Request();
        public abstract bool IsLoaded();

        public void RegisterCallback(Action onAdLoaded, Action onAdLoadFail, Action onAdClose, Action onAdRewarded)
        {
            _onAdLoaded = onAdLoaded;
            _onAdRewarded = onAdRewarded;
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

        public void HandleOnAdRewarded()
        {
            _onAdRewarded?.Invoke();
        }

    }
}