/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Kit 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:58:00 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Kit
{
    using UnityEngine;

    /// <summary> 屏蔽字 </summary>
    public class MaskWordKit
    {
        private static string[] words = new string[] { };

        /// <summary> 替换屏蔽字为* </summary>
        public static string Replace(string str)
        {
            for (int i = 0; i < words.Length; i++)
            {
                int index = str.IndexOf(words[i]);
                if (index < 0) continue;
                string value = "";
                if (index > 0) value = str.Substring(0, index);
                for (int j = 0; j < words[i].Length; j++)
                {
                    value += '*';
                }
                if (str.Length > index + words[i].Length) value += str.Substring(index + words[i].Length);
                str = value;
            }
            return str;
        }

        /// <summary> 加载屏蔽文本 </summary>
        public static void LoadMaskWord(string path)
        {
            if (words.Length == 0)
            {
                string text = ((TextAsset)Resources.Load(path)).text;
                words = text.Split(new char[] { '\n' });
            }
        }

        /// <summary> 加载屏蔽文本 </summary>
        public static void LoadMaskWord(TextAsset text)
        {
            if (words.Length == 0)
            {
                words = text.text.Split(new char[] { '\n' });
            }
        }
    }
}