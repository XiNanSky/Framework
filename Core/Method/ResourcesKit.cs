/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Kit 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:41:08 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Kit
{
    using System.Xml;
    using UnityEngine;

    /// <summary> 加载Resources中的资源 </summary>
    public class ResourcesKit
    {
        public static Sprite LoadSprite(string path)
        {
            return (Sprite)Resources.Load(path, typeof(Sprite));
        }

        public static Sprite[] LoadSprites(string path)
        {
            object[] objs = Resources.LoadAll(path, typeof(Sprite));
            Sprite[] sprites = new Sprite[objs.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = (Sprite)objs[i];
            }
            return sprites;
        }

        /// <summary> 加载指定路径的图片 </summary>
        public static Texture2D LoadImage(string path)
        {
            return (Texture2D)Resources.Load(path, typeof(Texture2D));
        }

        /// <summary> 加载指定文件夹中的所有图片 </summary>
        public static Texture2D[] LoadImages(string path)
        {
            object[] objs = Resources.LoadAll(path, typeof(Texture2D));
            Texture2D[] imgs = new Texture2D[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                imgs[i] = (Texture2D)objs[i];
            }
            return imgs;
        }

        /// <summary> 加载XML </summary>
        public static XmlNode LoadXml(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string text = LoadText(path);
            xmlDoc.LoadXml(text);
            return xmlDoc.DocumentElement;
        }

        /// <summary> 加载XML </summary>
        public static XmlNode LoadXmlText(string text)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(text);
            return xmlDoc.DocumentElement;
        }

        /// <summary> 加载文本中的字符串 </summary>
        public static string LoadText(string path)
        {
            return LoadTextAsset(path).text;
        }

        /// <summary> 加载文本中的二进制数组 </summary>
        public static ByteBuffer LoadByteBuffer(string path)
        {
            return new ByteBuffer(LoadBytes(path));
        }

        /// <summary> 加载文本中的二进制数组 </summary>
        public static byte[] LoadBytes(string path)
        {
            return LoadTextAsset(path).bytes;
        }

        /// <summary> 加载文本资源 </summary>
        public static TextAsset LoadTextAsset(string path)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(path);
            return textAsset;
        }

        /// <summary>
        /// 卸载未使用的资源。有些已加载的资源，
        /// 最明显的是纹理，即使在场景没有实例，
        /// 也最占内存。当资源不再需要时，
        /// 你可以使用Resources.UnloadUnusedAssets回收内存。 
        /// </summary>
        public static void Clear()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}