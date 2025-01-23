using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    public class MAXMediationAdInfoParser : MonoBehaviour
    {
        private const string MAX_REVENUE_CURRENCY_CODE = "USD";
        private static readonly AdInfo EmptyAdInfo = new AdInfo();
        private static readonly AdErrorInfo EmptyAdErorrInfo = new AdErrorInfo();
        public static AdInfo ParseToBaseAdInfo(MaxSdk.AdInfo maxAdInfo)
        {
            if (maxAdInfo == null)
            {
                return EmptyAdInfo;
            }

            return new AdInfo()
            {
                adUnitId = maxAdInfo.AdUnitIdentifier,
                adPlacement = maxAdInfo.Placement,
                currencyCode = MAX_REVENUE_CURRENCY_CODE,
                adType = maxAdInfo.AdFormat,
                adNetwork = maxAdInfo.NetworkName,
                revenue = maxAdInfo.Revenue,
            };
        }

        public static AdErrorInfo ParseToBaseAdErrorInfo(MaxSdk.ErrorInfo maxErrorInfo)
        {
            if (maxErrorInfo == null)
            {
                return EmptyAdErorrInfo;
            }

            return new AdErrorInfo()
            {
                code = (int)maxErrorInfo.Code,
                message = maxErrorInfo.Message,
            };
        }
    }
}