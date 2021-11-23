namespace Framework
{
    using Debug = UnityEngine.Debug;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 控制台开关
        /// </summary>
        public static bool DeveloperConsoleVisible
        {
            get => Debug.developerConsoleVisible;
            set => Debug.developerConsoleVisible = value;
        }

        /// <summary>
        /// 调试输出开关
        /// 如果想要设置 请前往 Build Settings 里面有 Development Build 选项
        /// </summary>
        public static bool IsDebugBuild => Debug.isDebugBuild;


        /// <summary>
        /// 输出
        /// </summary>
        public static void I(object obj)
        {
            Debug.Log(obj);
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="obj">info</param>
        /// <param name="color">颜色 16进制自带#</param>
        public static void I(object obj, string color)
        {
            Debug.Log($"<color={color}>{obj}</color>");
        }

        /// <summary>
        /// 输出 断言
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void IAssertion(object message)
        {
            Debug.LogAssertion(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertionFormat(Object context, string format, params object[] args)
        {
            Debug.LogAssertionFormat(context, format, args);
        }
    }
}
