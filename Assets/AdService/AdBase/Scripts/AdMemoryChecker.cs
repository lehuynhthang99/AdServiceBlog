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
            int memory = SystemInfo.systemMemorySize;
            return memory <= 1024;
        }
    }
}