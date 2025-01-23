using Nura.AdServiceBlog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog.MAXMediation
{
    public abstract class MAXMediationAdModelFactory<T> : AdModelFactoryBase<T>
    {
        [SerializeField] protected string _androidAdUnitKey;
        [SerializeField] protected string _iOSAdUnitKey;
        [SerializeField] protected string _editorAdUnitKey;

        [NonSerialized] protected string _chosenKey;

        protected string ChosenAdUnitKey
        {
            get
            {
                if (_chosenKey == null)
                {
#if UNITY_EDITOR
                    _chosenKey = _editorAdUnitKey;
#elif UNITY_ANDROID
                    _chosenKey = _androidAdUnitKey;
#elif UNITY_IOS
                    _chosenKey = _iOSAdUnitKey;
#else
                    _chosenKey = _editorAdUnitKey;
#endif
                }

                return _chosenKey;
            }
        }
    }
}