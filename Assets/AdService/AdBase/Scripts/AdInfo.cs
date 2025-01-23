using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    [Serializable]
    public class AdInfo
    {
        public string adUnitId;
        public string adPlacement;
        public string adType;
        public string adNetwork;
        public double revenue;
        public string currencyCode;

        public override string ToString()
        {
            return $"AdUnitId: {adUnitId}; AdPlacement: {adPlacement}; AdType: {adType}; AdNetwork: {adNetwork}; Revenue: {revenue}; CurrencyCode: {currencyCode}";
        }
    }

    [Serializable]
    public class AdErrorInfo
    {
        public int code;
        public string message;
    }
}