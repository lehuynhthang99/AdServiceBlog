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
    [CreateAssetMenu(fileName = "MAXMediationInitiatorFactory", menuName = "ScriptableObjects/Ads/Factory/MAXMediationInitiatorFactory")]
    public class MAXMediationInitiatorFactory : AdFactoryBase
    {

        [SerializeField] protected string _maxMediationSDKKey;

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
    }
}
