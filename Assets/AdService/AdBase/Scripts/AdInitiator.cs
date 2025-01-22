using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public class AdInitiator : MonoBehaviour
    {
        [SerializeField] private AdFactoryBase _adFactory;
        
        public async Task<bool> InitSDK()
        {
            return await _adFactory.InitSDK();
        }
    }
}