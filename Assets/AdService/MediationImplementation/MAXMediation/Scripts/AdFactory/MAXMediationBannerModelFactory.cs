using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    [CreateAssetMenu(fileName = "MAXMediationBannerModelFactory", menuName = "ScriptableObjects/Ads/Factory/MAXMediationBannerModelFactory")]
    public class MAXMediationBannerModelFactory : MAXMediationAdModelFactory<BannerAdModelBase>
    {
        public override BannerAdModelBase CreateAdModel()
        {
            return new MAXMediationBannerAdModel(ChosenAdUnitKey);

        }
    }
}