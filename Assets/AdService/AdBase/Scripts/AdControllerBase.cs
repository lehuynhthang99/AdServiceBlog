using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class AdControllerBase : MonoBehaviour
    {
        public const int MIN_SECONDS_TO_REQUEST_WHEN_REQUEST_FAILED_IN_MS = 10000; //10s
        public const int INTERVAL_LOAD_NEXT_AD_AFTER_WATCH_IN_MS = 1000; //1s

        [SerializeField] protected AdFactoryBase _adFactory;

        public abstract void RequestAd();
        public abstract bool IsLoadedAd();
    }
}