using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.Client
{
    public class AdClientManager : MonoBehaviour
    {
        private static AdClientManager _instance;
        private const int DELAY_RETRY_IN_MS = 5000;
        private const int DELAY_REQUEST_AD = 2000;
        [SerializeField] private AdInitiator _adInitiator;
        [SerializeField] private BannerAdController _bannerAdController;
        [SerializeField] private RewardAdController _rewardAdController;

        [Space(12)]
        [SerializeField] private InterstitialAdController _normalInterstitialAdController;
        [SerializeField] private InterstitialAdController _lowEndInterstitialAdController;

        private InterstitialAdController _chosenInterstitialAdController = null;
        public static AdClientManager Instance { get => _instance; }
        public static bool IsAlive => _instance;

        private void Awake()
        {
            if (_instance)
            {
                Debug.LogError("Already has an instance");
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void OnDestroy()
        {
            if (this == _instance)
            {
                _instance = null;
            }
        }

        private void Start()
        {
            InitializeAds();
        }

        private async void InitializeAds()
        {
            //init SDK
            int maxRetry = 3;
            while (true)
            {
                bool result = await _adInitiator.InitSDK();
                if (result)
                {
                    break;
                }

                maxRetry--;
                if (maxRetry == 0)
                {
                    Debug.Log("Can't init Ad mediation");
                    return;
                }

                await Task.Delay(DELAY_RETRY_IN_MS);
            }

            //request ad
            bool isLowEndDevice = AdMemoryChecker.IsDeviceWeak();

            if (!isLowEndDevice)
            {
                await Task.Delay(DELAY_REQUEST_AD);
                _bannerAdController.RequestAd();
            }

            await Task.Delay(DELAY_REQUEST_AD);
            _rewardAdController.RequestAd();  
            
            await Task.Delay(DELAY_REQUEST_AD);
            if (isLowEndDevice)
            {
                _chosenInterstitialAdController = _lowEndInterstitialAdController;
            }
            else
            {
                _chosenInterstitialAdController = _normalInterstitialAdController;
            }
            _chosenInterstitialAdController.RequestAd();
        }

        public void ShowRewardAd(Action onWatchSuccess, Action onWatchError)
        {
            bool isSuccess = false;
            if (_rewardAdController.IsLoadedAd())
            {
                _rewardAdController.RegisterOnWatchAdSuccess(onWatchSuccess);
                isSuccess = _rewardAdController.TryShowAd();
            }

            if (!isSuccess)
            {
                onWatchError?.Invoke();
            }
        }

        public void ShowInterstitialAd(Action onAdClose, Action onWatchError)
        {
            bool isSuccess = false;
            if (_chosenInterstitialAdController != null && _chosenInterstitialAdController.IsLoadedAd())
            {
                _chosenInterstitialAdController.RegisterOnCloseAd(onAdClose);
                isSuccess = _chosenInterstitialAdController.TryShowAd();
            }

            if (!isSuccess)
            {
                onWatchError?.Invoke();
            }
        }
    }
}