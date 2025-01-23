using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class AdFactoryBase : ScriptableObject
    {
        public abstract Task<bool> InitSDK();
    }
}