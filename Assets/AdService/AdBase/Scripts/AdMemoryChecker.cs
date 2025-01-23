using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nura.AdServiceBlog
{
    public static class AdMemoryChecker
    {
        public static bool IsDeviceWeak()
        {
#if UNITY_IOS || UNITY_EDITOR
            return false;
#endif
            int memory = SystemInfo.systemMemorySize;
            return memory <= 1024;
        }
    }
}