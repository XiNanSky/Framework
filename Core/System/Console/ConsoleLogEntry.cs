/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-10                    *
* Nowtime:           18:08:17                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Console
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public class ConsoleLogEntry : IEquatable<ConsoleLogEntry>
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType;

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent;

        /// <summary>
        /// 日志堆栈内容
        /// </summary>
        public string StackContent;

        /// <summary>
        /// 当前日志数量
        /// </summary>
        public int LogCount;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected;

        /// <summary>
        /// 哈希值
        /// </summary>
        private int _HasValue;

        /// <summary>
        /// 是否已经生成了哈希值
        /// </summary>
        private bool _GenerateHasValue;

        // tostring content
        private string _Content;

        public ConsoleLogEntry(string logContent, string stackContent, LogType logType)
        {
            LogContent = logContent;
            StackContent = stackContent;
            LogType = logType;
            LogCount = 1;
            Selected = false;
        }

        public ConsoleLogEntry Reset()
        {
            LogCount = 1;
            Selected = false;
            return this;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_Content))
                _Content = string.Concat(LogContent, "\n\n", StackContent);
            return _Content;
        }

        public bool Equals(ConsoleLogEntry entry)
        {
            return LogContent == entry.LogContent && StackContent == entry.StackContent;
        }

        /// <summary>
        /// 最快获取字符串哈希方法 
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/19250516/2373034"/>
        public override int GetHashCode()
        {
            if (!_GenerateHasValue)
            {
                unchecked
                {
                    _HasValue = 17;
                    _HasValue = _HasValue * 23 + LogContent == null ? 0 : LogContent.GetHashCode();
                    _HasValue = _HasValue * 23 + StackContent == null ? 0 : StackContent.GetHashCode();
                }
            }
            return _HasValue;
        }
    }
}