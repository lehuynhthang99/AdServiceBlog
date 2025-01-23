using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    [CreateAssetMenu(fileName = "MAXMediationRewardModelFactory", menuName = "ScriptableObjects/Ads/Factory/MAXMediationRewardModelFactory")]
    public class MAXMediationRewardModelFactory : MAXMediationAdModelFactory<RewardAdModelBase>
    {
        public override RewardAdModelBase CreateAdModel()
        {
            return new MAXMediationRewardAdModel(ChosenAdUnitKey);

        }
    }
}