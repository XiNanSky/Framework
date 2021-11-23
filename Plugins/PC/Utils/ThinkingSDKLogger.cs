using ThinkingSDK.PC.Config;
using UnityEngine;

namespace ThinkingSDK.PC.Utils
{
    public class ThinkingSDKLogger
    {
        public ThinkingSDKLogger()
        {

        }
        public static void Print(string str)
        {
            if (ThinkingSDKPublicConfig.IsPrintLog())
            {
                Debug.Log(string.Concat("<color=#6495ED>[ThinkingSDK Unity_PC_V", ThinkingSDKAppInfo.LibVersion(), "] ", str, "</color>"));
            }
        }
    }
}
