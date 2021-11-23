/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:40:18 
*Description:      数据传输包装类
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections;

namespace Framework
{
    [Serializable]
    public class BytesSerializable
    {
        [NonSerialized]
        private Hashtable data;

        public BytesSerializable() { }

        public void DataValueSet(object key, object value)
        {
            if (data == null)
                data = new Hashtable();
            if (data.ContainsKey(key))
                data[key] = value;
            else
                data.Add(key, value);
        }

        private bool DataValueIsExist(object key)
        {
            if (data == null || !data.ContainsKey(key))
                return false;
            return true;
        }


        public object DataValueGet(object key)
        {
            if (!DataValueIsExist(key))
                return null;
            return data[key];
        }

        public bool DataValueRemove(object key)
        {
            if (!DataValueIsExist(key)) return false;
            data.Remove(key);
            return true;
        }

        /** 提供数据读取存储方法 */

        public virtual void BytesRead(ByteBuffer data) { }

        public virtual void BytesWrite(ByteBuffer data) { }
    }
}