using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public abstract class AdModelFactoryBase<T> : ScriptableObject
    {
        public abstract T CreateAdModel();
    }
}