/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Util 
*Author:           HYZ 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-04 
*NOWTIME:          14:14:24 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Sirenix.OdinInspector;
    using System;

    public class ArrayList<T>
    {
        private const int CAPACITY = 10;

        [ShowInInspector, Label("元素")]
        private T[] array;
        private int size;

        /// <summary> 预留字段</summary>
        [HideInPlayMode] public int LucK;
        /// <summary> 预留字段 </summary>
        [HideInPlayMode] public int LuckX;
        /// <summary> 预留字段 </summary>
        [HideInPlayMode] public int LuckY;
        /// <summary> 预留字段 </summary>
        [HideInPlayMode] public long LuckL;

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public ArrayList() : this(CAPACITY) { }

        public ArrayList(int paramInt)
        {
            if (paramInt < 0)
                throw new SystemException("ArrayList <init>, invalid capatity:" + paramInt);
            array = new T[paramInt];
        }

        public ArrayList(T[] array) : this(array, (array != null) ? array.Length : 0) { }

        public ArrayList(T[] array, int paramInt)
        {
            if (array == null) this.array = new T[CAPACITY];
            else
            {
                if (paramInt > array.Length)
                    throw new SystemException("ArrayList <init>, invalid length:" + paramInt);
                this.array = array;
            }
            size = paramInt;
        }

        public int Count => size;

        public int GetCount(T value)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (value.Equals(array[i]))
                    count++;
            }

            return count;
        }

        public int Capacity()
        {
            return array.Length;
        }

        public T[] GetArray()
        {
            return array;
        }

        private void SetCapacity(int paramInt)
        {
            T[] arrayOfObject1 = this.array;
            int i = arrayOfObject1.Length;
            if (paramInt <= i) return;
            for (; i < paramInt; i = (i << 1) + 1)
                ;
            T[] arrayOfObject2 = new T[i];
            Array.Copy(arrayOfObject1, 0, arrayOfObject2, 0, this.size);
            this.array = arrayOfObject2;
        }

        public T Get(int paramInt)
        {
            return array[paramInt];
        }

        public T GetLast()
        {
            if (size == 0) return default(T);
            return array[size - 1];
        }

        public T GetFirst()
        {
            if (size == 0) return default(T);
            return array[0];
        }

        public bool Contains(T paramObject)
        {
            return (IndexOf(paramObject, 0) >= 0);
        }

        public int IndexOf(T paramObject)
        {
            return IndexOf(paramObject, 0);
        }

        private int IndexOf(T paramObject, int paramInt)
        {
            int i = size;
            if (paramInt >= i) return -1;
            T[] arrayOfObject = array;
            int j;
            if (paramObject == null)
            {
                for (j = paramInt; j < i; ++j)
                {
                    if (arrayOfObject[j] == null) return j;
                }
            }
            else
            {
                for (j = paramInt; j < i; ++j)
                {
                    if (paramObject.Equals(arrayOfObject[j])) return j;
                }
            }
            return -1;
        }

        public T Set(T paramObject, int paramInt)
        {
            if (paramInt >= size)
                throw new SystemException("ArrayList set, invalid index=" + paramInt);
            T localObject = array[paramInt];
            array[paramInt] = paramObject;
            return localObject;
        }

        public bool Add(T paramObject)
        {
            if (size >= array.Length) SetCapacity(size + 1);
            array[(size++)] = paramObject;
            return true;
        }

        public bool Add(T t, int count)
        {
            for (int i = 0; i < count; i++)
                Add(t);
            return true;
        }


        public bool Add(T[] paramObjects)
        {
            if (size + paramObjects.Length > array.Length) SetCapacity(size + paramObjects.Length);
            for (int i = 0; i < paramObjects.Length; i++)
            {
                array[(size++)] = paramObjects[i];
            }
            return true;
        }

        /// <summary> 加到开头 </summary>
        public void AddFirst(params T[] paramObjects)
        {
            if (paramObjects == null || paramObjects.Length == 0)
            {
                return;
            }
            Array.Copy(array, 0, paramObjects, paramObjects.Length, size);
            //array = ArraysKit.Add(paramObjects, array);
            array = paramObjects;
            size = paramObjects.Length;
        }

        public bool AddAt(T paramObject, int paramInt)
        {
            if (size >= array.Length) SetCapacity(size + 1);
            int i = size - paramInt;
            if (i > 0) Array.Copy(array, paramInt, array, paramInt + 1, i);
            Set(paramObject, paramInt);
            size++;
            return true;
        }

        public bool Remove(T paramObject)
        {
            int i = IndexOf(paramObject, 0);
            if (i < 0) return false;
            RemoveAt(i);
            return true;
        }

        public T RemoveAt(int paramInt)
        {
            if (paramInt >= size)
                throw new SystemException("ArrayList remove, invalid index=" + paramInt);
            T[] arrayOfObject = array;
            T localObject = arrayOfObject[paramInt];
            int i = size - paramInt - 1;
            if (i > 0)
                Array.Copy(arrayOfObject, paramInt + 1, arrayOfObject,
                    paramInt, i);
            arrayOfObject[(--size)] = default;
            return localObject;
        }

        /// <summary> 移除第一个 </summary>
        public T RemoveFirst()
        {
            return RemoveAt(0);
        }

        /// <summary> 移除最后一个 </summary>
        public T RemoveLast()
        {
            if (size == 0)
                throw new SystemException("ArrayList remove, invalid size=" + this.size);
            T localObject = array[size - 1];
            array[(--size)] = default;
            return localObject;
        }

        public T[] RemoveAll()
        {
            T[] arrayOfObject = new T[size];
            Array.Copy(array, 0, arrayOfObject, 0, size);
            Clear();
            return arrayOfObject;
        }

        public void Clear()
        {
            T[] arrayOfObject = array;
            for (int i = size - 1; i >= 0; --i)
                arrayOfObject[i] = default;
            size = 0; LucK = -1; LuckL = 0; LuckX = 0; LuckY = 0;
        }

        public T[] ToArray()
        {
            T[] arrayOfObject = new T[size];
            Array.Copy(array, 0, arrayOfObject, 0, size);
            return arrayOfObject;
        }
    }
}