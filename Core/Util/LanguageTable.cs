/*
* 类说明：语言文案转换工具
* 
* @author HYZ(huangyz1988@qq.com)
* @version 2014-3-5 下午2:30:31
*/

namespace Framework
{
    using System;
    using System.Collections;
    using System.Xml;

    /// <summary> 语言文案转换工具 </summary>
    public class LanguageTable
    {
        /// <summary> 字典 </summary>
        private Hashtable table;

        /// <summary> 根据配置表初始化字典</summary>
        public LanguageTable(XmlNode xml)
        {
            table = new Hashtable();
            foreach (XmlNode xmlDoc in xml.ChildNodes)
            {
                string id = null, text = null;
                if (xmlDoc.GetType() == typeof(XmlElement) && xmlDoc.LocalName == "sample")
                {
                    foreach (XmlAttribute xmlAtt in xmlDoc.Attributes)
                    {
                        if (xmlAtt.Name == "id") id = xmlAtt.Value;
                        if (xmlAtt.Name == "string") text = xmlAtt.Value;
                    }
                    if (id == null || text == null) throw new Exception("lang init error! " + xmlDoc.OuterXml);
                    if (table.ContainsKey(id)) throw new Exception("duplicate id! " + xmlDoc.OuterXml);
                    if (!id.Contains("#!")) id = "#!" + id;
                    table.Add(id, text);
                }
            }
        }

        /// <summary> 转换语言文案</summary>
        public string Trans(string key)
        {
            if (key == null || key == "") return key;
            if (table == null) return key;
            string text = (string)table[key];
            if (text == null) return key;
            return text;
        }

        /// <summary> 转换带有一个参数的语言文案</summary>
        public string Trans(string key, object value)
        {
            if (table == null) return key;
            string text = (string)table[key];
            if (text == null) return key;
            try { text = string.Format(text, value); }
            catch (Exception) { }
            return text;
        }

        /// <summary> 转换带有一个参数的语言文案</summary>
        public string Trans(string key, object v1, object v2)
        {
            if (table == null) return key;
            string text = (string)table[key];
            if (text == null) return key;
            try { text = string.Format(text, v1, v2); }
            catch (Exception) { }
            return text;
        }

        /// <summary> 转换带有两个参数的语言文案</summary>
        public string Trans(string key, object v1, object v2, object v3)
        {
            if (table == null) return key;
            string text = (string)table[key];
            if (text == null) return key;
            try { text = string.Format(text, v1, v2, v3); }
            catch (Exception) { }
            return text;
        }

        /// <summary> 转换带有多个参数的语言文案</summary>
        public string Trans(string key, object[] values)
        {
            if (table != null)
            {
                string text = (string)table[key];
                if (text != null) key = text;
            }
            try { key = string.Format(key, values); }
            catch (Exception) { }
            return key;
        }
    }
}