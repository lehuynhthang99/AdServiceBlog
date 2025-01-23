using Nura.AdServiceBlog.MAXMediation;
using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    [CreateAssetMenu(fileName = "MAXMediationAdFactory", menuName = "ScriptableObjects/Ads/Factory/MAXMediationAdFactory")]
    public class MAXMediationAdFactory : AdFactoryBase
    {
        [Serializable]
        protected class MaxMediationAdUnitKeys
        {
            [SerializeField] protected string _maxMediationBannerID;
            [SerializeField] protected string _maxMediationInterstitialID;
            [SerializeField] protected string _maxMediationRewardID;

            public string MaxMediationBannerID => _maxMediationBannerID;
            public string MaxMediationInterstitialID => _maxMediationInterstitialID;
            public string MaxMediationRewardID => _maxMediationRewardID;
        }

        [SerializeField] protected string _maxMediationSDKKey;

        [Space(12)]
        [SerializeField] protected MaxMediationAdUnitKeys _androidAdUnitKeys;
        [SerializeField] protected MaxMediationAdUnitKeys _iOSAdUnitKeys;
        [SerializeField] protected MaxMediationAdUnitKeys _editorAdUnitKeys;

        [NonSerialized] protected MaxMediationAdUnitKeys _chosenMaxMediationAdUnitKeys;

        protected MaxMediationAdUnitKeys ChosenMaxMediationAdUnitKeys
        {
            get
            {
                if (_chosenMaxMediationAdUnitKeys == null)
                {
#if UNITY_EDITOR
                    _chosenMaxMediationAdUnitKeys = _editorAdUnitKeys;
#elif UNITY_ANDROID
                    _chosenMaxMediationAdUnitKeys = _androidAdUnitKeys;
#elif UNITY_IOS
                    _chosenMaxMediationAdUnitKeys = _iOSAdUnitKeys;
#else
                    _chosenMaxMediationAdUnitKeys = _editorAdUnitKeys;
#endif
                }

                return _chosenMaxMediationAdUnitKeys;
            }
        }

        public override async Task<bool> InitSDK()
        {
            TaskCompletionSource<bool> onInitComplete = new TaskCompletionSource<bool>();
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                onInitComplete.TrySetResult(true);
            };

            MaxSdk.SetSdkKey(_maxMediationSDKKey);
            MaxSdk.InitializeSdk();

#if MAX_MEDIATION_DEBUGGER
            MaxSdk.ShowMediationDebugger();
#endif

            await onInitComplete.Task;

            return true;
        }

        public override BannerAdModelBase CreateBannerAdModel()
        {
            string maxMediationBannerID = ChosenMaxMediationAdUnitKeys.MaxMediationBannerID;
            return new MAXMediationBannerAdModel(maxMediationBannerID);
        }

        public override InterstitialAdModelBase CreateInterstitialAdModel()
        {
            string maxMediationInterstitialID = ChosenMaxMediationAdUnitKeys.MaxMediationInterstitialID;
            return new MAXMediationInterstitialAdModel(maxMediationInterstitialID);
        }

        public override RewardAdModelBase CreateRewardAdModel()
        {
            string maxMediationRewardID = ChosenMaxMediationAdUnitKeys.MaxMediationRewardID;
            return new MAXMediationRewardAdModel(maxMediationRewardID);
        }
    }
}
