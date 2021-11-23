/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Kit 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:43:12 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using System.Text;

    /**
   * 提供writeXXX，readXXX，setXXX,getXXX方法<br>
   * <li>writeXXX： 将XXX写入字节缓存，改变写入进度
   * <li>readXXX：从字节缓存中读取XXX，改变读取进度
   * <li>setXXX：需要传入索引，在字节缓存的指定索引处写入一个XXX，不影响缓存读写进度
   * <li>getXXX：需要传入索引，在字节缓存的指定索引处读取一个XXX，不影响缓存读写进度
   * 
   * @author HYZ <huangyz1988@qq.com>
   * @create 2017年2月20日上午5:51:00
   * @version 1.0
   */
    public class ByteKit
    {
        /** 在指定位置读取一个boolean，不改变读取进度 */
        public static bool readBoolean(byte[] array, int index)
        {
            return array[index] != 0;
        }


        /** 在指定位置读取一个byte，不改变读取进度 */
        public static byte readByte(byte[] array, int index)
        {
            return array[index];
        }

        /** 在指定位置读取一个无符号byte，不改变读取进度 */
        public static int readUnsignedByte(byte[] array, int index)
        {
            return array[index] & 0xFF;
        }

        /** 在指定位置读取一个short，不改变读取进度 */
        public static short readShort(byte[] array, int index)
        {
            return (short)readUnsignedShort(array, index);
        }

        /** 在指定位置读取一个无符号short，不改变读取进度 */
        public static int readUnsignedShort(byte[] array, int index)
        {
            return ((array[index] & 0xFF) << 8) | (array[index + 1] & 0xFF);
        }


        /** 在指定位置读取一个int，不改变读取进度 */
        public static int readInt(byte[] array, int index)
        {
            return ((array[index] & 0xFF) << 24) | ((array[index + 1] & 0xFF) << 16) | ((array[index + 2] & 0xFF) << 8)
                | (array[index + 3] & 0xFF);
        }

        /** 在指定位置读取一个int，不改变读取进度 */
        public static long readLong(byte[] array, int index)
        {
            return ((array[index] & 0xFFL) << 56) | ((array[index + 1] & 0xFFL) << 48) | ((array[index + 2] & 0xFFL) << 40)
                | ((array[index + 3] & 0xFFL) << 32) | ((array[index + 4] & 0xFFL) << 24) | ((array[index + 5] & 0xFFL) << 16)
                | ((array[index + 6] & 0xFFL) << 8) | (array[index + 7] & 0xFFL);
        }

        /** 在指定位置读取一个float，不改变读取进度 */
        public static float readFloat_(byte[] array, int index)
        {
            return BitConverter.ToSingle(array, index);
        }

        /** 在指定位置读取一个float，不改变读取进度 */
        public static float readFloat(byte[] array, int index)
        {
            index += 3;
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = array[index--];
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        /** 在指定位置读取一个double，不改变读取进度 */
        public static double readDouble_(byte[] array, int index)
        {
            return BitConverter.ToDouble(array, index);
        }

        /** 在指定位置读取一个double，不改变读取进度 */
        public static double readDouble(byte[] array, int index)
        {
            index += 7;
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = array[index--];
            }
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// 读取一个长度（0至512M）<br>
        /// 原理：以第一个字节二进制前三位来决定长度值占用的字节数（x表示0或1）<br>
        /// <li>1xx_开头：长度值占1个字节，且值只能是剩下的 7=(8-1) 位能表示的范围，即：0~(2^7-1)
        /// <li>01x_开头：长度值占2个字节，且值只能是剩下的 14=(16-2) 位能表示的范围，即：0~(2^14-1)
        /// <li>001_开头：长度值占4个字节，且值只能是剩下的 29=(32-3) 位能表示的范围，即：0~(2^29-1)
        /// </summary>
        public static int readLength(byte[] array, int index)
        {
            int n = array[index] & 0xFF;// 判定字节
            // 第一个字节若小于 0x20否则该字节不能表示为长度
            if (n < 0x20) throw new SystemException("readLength, invalid number:" + n);
            // 第一个字节若大于 0x20（二进制为：0010 0000）则减去 0x20<<(8*3)（补3个字节位数）即为长度值
            if (n < 0x40) return readInt(array, index) & ((1 << 29) - 1);
            // 第一个字节若大于 0x40（二进制为：0100 0000）则减去 0x40<<8（补1个字节位数）即为长度值
            if (n < 0x80) return readUnsignedShort(array, index) & ((1 << 14) - 1);
            // 第一个字节若大于 0x80（二进制为：1000 0000）则减去 0x80即为长度值
            return readUnsignedByte(array, index) & ((1 << 7) - 1);
        }

        /** 按指定编码格式读取一个字符串 */
        public static string readString(byte[] array, int index, String encoding)
        {
            int len = readInt(array, index);
            if (len < 0) return null;
            if (len == 0) return "";
            if (encoding == null) return Encoding.Default.GetString(array, index + 4, len);
            try
            {
                return Encoding.GetEncoding(encoding).GetString(array, index + 4, len);
            }
            catch (Exception e)
            {
                throw new SystemException("invalid charsetName:" + encoding, e);
            }
        }

        /** 按utf-8编码格式读取一个字符串 */
        public static String readUTF(byte[] array, int index)
        {
            return readString(array, index, "utf-8");
        }

        /** 在缓存index处写入一个boolean，不改变写入进度 */
        public static void writeBoolean(byte[] array, bool b, int index)
        {
            array[index] = (byte)(b ? 1 : 0);
        }

        /** 在缓存index处写入一个byte，不改变写入进度 */
        public static void writeByte(byte[] array, int value, int index)
        {
            array[index] = (byte)value;
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public static void writeShort(byte[] array, int value, int index)
        {
            array[index] = (byte)(value >> 8);
            array[index + 1] = (byte)value;
        }

        /** 在缓存index处写入一个int，不改变写入进度 */
        public static void writeInt(byte[] array, int value, int index)
        {
            array[index] = (byte)(value >> 24);
            array[index + 1] = (byte)(value >> 16);
            array[index + 2] = (byte)(value >> 8);
            array[index + 3] = (byte)value;
        }

        /** 在缓存index处写入一个long，不改变写入进度 */
        public static void writeLong(byte[] array, long value, int index)
        {
            array[index] = (byte)(value >> 56);
            array[index + 1] = (byte)(value >> 48);
            array[index + 2] = (byte)(value >> 40);
            array[index + 3] = (byte)(value >> 32);
            array[index + 4] = (byte)(value >> 24);
            array[index + 5] = (byte)(value >> 16);
            array[index + 6] = (byte)(value >> 8);
            array[index + 7] = (byte)value;
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public static void writeFloat_(byte[] array, float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                array[index + i] = bytes[i];
            }
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public static void writeFloat(byte[] array, float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                array[index + i] = bytes[j];
            }
        }

        /** 在缓存index处写入一个long，不改变写入进度 */
        public static void writeDouble_(byte[] array, double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                array[index + i] = bytes[i];
            }
        }

        /** 在缓存index处写入一个long，不改变写入进度 */
        public static void writeDouble(byte[] array, double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                array[index + i] = bytes[j];
            }
        }

        /**
         * 写入一个长度（0至512M）<br>
         * 原理：以第一个字节二进制前三位来决定长度值占用的字节数（x表示0或1）<br>
         * <li>1xx_开头：长度值占1个字节，且值只能是剩下的 7=(8-1) 位能表示的范围，即：0~(2^7-1)
         * <li>01x_开头：长度值占2个字节，且值只能是剩下的 14=(16-2) 位能表示的范围，即：0~(2^14-1)
         * <li>001_开头：长度值占4个字节，且值只能是剩下的 29=(32-3) 位能表示的范围，即：0~(2^29-1)
         */
        public static void writeLength(byte[] array, int index, int len)
        {
            // 不在0~(2^29-1)范围内
            if (len >= 0x20000000 || len < 0) throw new SystemException(" writeLength, invalid len:" + len);
            // 写入时，加上判定位表示的值
            if (len < 0x80)// 1xx范围0~(2^7-1)
                writeByte(array, len | 0x80, index);
            else if (len < 0x4000)// 01x范围0~(2^14-1)
                writeShort(array, len | 0x4000, index);
            else// 001范围0~(2^29-1)
                writeInt(array, len | 0x20000000, index);
        }

        /** 写入一个指定编码类型的字符串 */
        public static void writeString(byte[] array, String str, String encoding, int index)
        {
            if (str == null)
                writeInt(array, -1, index);
            else if (str.Length <= 0)
                writeInt(array, 0, index);
            else
            {
                byte[] bytes;
                if (encoding == null)
                    bytes = Encoding.Default.GetBytes(str);
                else
                {
                    try
                    {
                        bytes = Encoding.GetEncoding(encoding).GetBytes(str);
                    }
                    catch (Exception e)
                    {
                        throw new SystemException("invalid charsetName:" + encoding, e);
                    }
                }
                writeInt(array, bytes.Length, index);
                Array.Copy(bytes, 0, array, index + 4, bytes.Length);
            }
        }

        /** 写入一个utf-8编码类型的字符串 */
        public static void writeUTF(byte[] array, String str, int index)
        {
            writeString(array, str, "utf-8", index);
        }
    }
}