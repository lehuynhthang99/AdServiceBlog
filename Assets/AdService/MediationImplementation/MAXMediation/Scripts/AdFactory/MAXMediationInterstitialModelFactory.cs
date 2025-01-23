using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    [CreateAssetMenu(fileName = "MAXMediationInterstitialModelFactory", menuName = "ScriptableObjects/Ads/Factory/MAXMediationInterstitialModelFactory")]
    public class MAXMediationInterstitialModelFactory : MAXMediationAdModelFactory<InterstitialAdModelBase>
    {
        public override InterstitialAdModelBase CreateAdModel()
        {
            return new MAXMediationInterstitialAdModel(ChosenAdUnitKey);
        }
    }
}