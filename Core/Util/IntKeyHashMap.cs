/*
* 类说明：条目
* 
* @author HYZ(huangyz1988@qq.com)
* @version 2014-3-5 下午2:30:31
*/

namespace Framework
{
    using System;

    public class IntKeyHashMap<T>
    {
        /** 容量 */
        public const int CAPACITY = 16;
        /** 加载因子 */
        public const float LOAD_FACTOR = 0.75F;

        /** 条目数组 */
        private Entry<T>[] array;
        /** 当前条目数量 */
        public int count;

        /** 加载因子 */
        private readonly float loadFactor;

        /** 扩容阈值 */
        private int threshold;

        /** 模版 */
        private Entry<T> entry = new Entry<T>(0, default(T));

        public IntKeyHashMap() : this(CAPACITY, LOAD_FACTOR)
        {
        }

        public IntKeyHashMap(int capacity) : this(capacity, LOAD_FACTOR)
        {
        }

        public IntKeyHashMap(int capacity, float load_factor)
        {
            if (capacity < 1) throw new ArgumentException("<init>, invalid capatity:" + capacity);
            if (load_factor <= 0.0F) throw new ArgumentException("<init>, invalid loadFactor:" + load_factor);
            threshold = (int)(capacity * load_factor);
            if (threshold <= 0) throw new ArgumentException("<init>, invalid threshold:" + capacity + " " + load_factor);
            loadFactor = load_factor;
            array = new Entry<T>[capacity];
        }

        public int size()
        {
            return count;
        }

        public T get(int key)
        {
            Entry<T> entry = array[(key & int.MaxValue) % array.Length];
            while (entry != null)
            {
                if (entry.key == key) return entry.value;
                entry = entry.next;
            }
            return default(T);
        }

        public T put(int key, T value)
        {
            int i = (key & int.MaxValue) % array.Length;
            Entry<T> entry = array[i];
            if (entry != null)
            {
                while (true)
                {
                    if (entry.key == key)
                    {
                        T oldValue = entry.value;
                        entry.value = value;
                        return oldValue;
                    }
                    if (entry.next == null) break;
                    entry = entry.next;
                }
                entry.next = new Entry<T>(key, value);
            }
            else
            {
                array[i] = new Entry<T>(key, value);
            }
            count += 1;
            if (count >= threshold) rehash(array.Length + 1);
            return default(T);
        }

        public T remove(int key)
        {
            int i = (key & int.MaxValue) % array.Length;
            Entry<T> entry1 = array[i];
            Entry<T> entry2 = null;
            while (entry1 != null)
            {
                if (entry1.key == key)
                {
                    T value = entry1.value;
                    if (entry2 != null)
                        entry2.next = entry1.next;
                    else
                        array[i] = entry1.next;
                    this.count -= 1;
                    return value;
                }
                entry2 = entry1;
                entry1 = entry1.next;
            }
            return default(T);
        }

        public void rehash(int v)
        {
            int capacity = array.Length;
            if (v <= capacity) return;
            while (capacity < v)
                capacity = (capacity << 1) + 1;
            Entry<T>[] entrys = new Entry<T>[capacity];
            int key = 0;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                Entry<T> entry = array[i];
                while (entry != null)
                {
                    Entry<T> entry1 = entry.next;
                    key = (entry.key & int.MaxValue) % capacity;
                    Entry<T> entry2 = entrys[key];
                    entrys[key] = entry;
                    entry.next = entry2;
                    entry = entry1;
                }
            }
            array = entrys;
            threshold = (int)(capacity * loadFactor);
        }

        public int select(Selector<Entry<T>> selector)
        {
            Entry<T> entry = null;
            int j = 0;
            for (int k = array.Length - 1; k >= 0; k--)
            {
                Entry<T> entry1 = array[k];
                while (entry1 != null)
                {
                    int i = selector(entry1);
                    Entry<T> entry2 = entry1.next;
                    if (i == SelectType.FALSE)
                    {
                        entry1 = entry2;
                    }
                    else if (i == SelectType.TRUE)
                    {
                        if (entry != null)
                            entry.next = entry2;
                        else
                            array[k] = entry2;
                        count -= 1;
                        j = i;
                        entry1 = entry2;
                    }
                    else
                    {
                        if (i == SelectType.TRUE_BREAK)
                        {
                            if (entry != null)
                                entry.next = entry2;
                            else
                                array[k] = entry2;
                            count -= 1;
                        }
                        return i;
                    }
                }
            }
            return j;
        }

        public void clear()
        {
            for (int i = array.Length - 1; i >= 0; i--)
                array[i] = null;
            count = 0;
        }

        public int[] keyArray()
        {
            int[] keys = new int[count];
            int i = array.Length - 1;
            for (int j = 0; i >= 0; i--)
            {
                Entry<T> entry = array[i];
                while (entry != null)
                {
                    keys[j++] = entry.key;
                    entry = entry.next;
                }
            }
            return keys;
        }

        public Entry<T>[] entryArray()
        {
            Entry<T>[] values = new Entry<T>[count];
            int i = array.Length - 1;
            for (int j = 0; i >= 0; i--)
            {
                Entry<T> entry = array[i];
                while (entry != null)
                {
                    values[j++] = entry;
                    entry = entry.next;
                }
            }
            return values;
        }

        public T[] valueArray()
        {
            T[] values = new T[count];
            int i = array.Length - 1;
            for (int j = 0; i >= 0; i--)
            {
                Entry<T> entry = array[i];
                while (entry != null)
                {
                    values[j++] = entry.value;
                    entry = entry.next;
                }
            }
            return values;
        }

        public int valueArray(T[] values)
        {
            int i = values.Length > count ? count : values.Length;
            if (i == 0) return 0;
            int j = 0;
            for (int k = array.Length - 1; k >= 0; k--)
            {
                Entry<T> entry = array[k];
                while (entry != null)
                {
                    values[j++] = entry.value;
                    if (j >= i) return j;
                    entry = entry.next;
                }
            }
            return j;
        }

        public override string ToString()
        {
            return base.ToString() + "[size=" + count + ", capacity=" + array.Length + "]";
        }
    }

    /**
    * 类说明：条目
    * 
    * @author HYZ(huangyz1988@qq.com)
    * @version 2014-3-5 下午2:30:31
    */
    public class Entry<T>
    {

        public int key;
        public Entry<T> next;
        public T value;

        public Entry(int key, T value)
        {
            this.key = key;
            this.value = value;
        }

        public int getKey()
        {
            return key;
        }

        public T getValue()
        {
            return value;
        }
    }

    /**
     * 类说明：
     * 
     *  通过委托方法，对T类型对象做出赛选操作，返回赛选结果类型
     *  以下是可能的结果：
     *   {SelectType.FALSE}         不符合赛选
     *   {SelectType.TRUE}          符合赛选
     *   {SelectType.FALSE_BREAK}   不符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个不符合条件的对象
     *   {SelectType.TRUE_BREAK}    符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个符合条件的对象
     * 
     * @author HYZ [huangyz1988@qq.com]
     * @version 2018年11月23日 上午12:08:57
     */
    public delegate int Selector<in T>(T t);

    /*
     * 类说明：
     * 
     * <pre>
     *  通过{@linkplain #select select(T t)}方法，对T类型对象做出赛选操作，返回赛选结果类型
     *  以下是可能的结果：
     *   {@linkplain #FALSE FALSE}         不符合赛选
     *   {@linkplain #TRUE TRUE}           符合赛选
     *   {@linkplain #FALSE_BREAK FALSE_BREAK}   不符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个不符合条件的对象
     *   {@linkplain #TRUE_BREAK TRUE_BREAK}    符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个符合条件的对象
     * </pre>
     * 
     * @author HYZ [huangyz1988@qq.com]
     * @version 2018年11月23日 上午12:08:57
     */
    public class SelectType
    {

        /** 赛选结果状态值：不符合选择 */
        public const int FALSE = 0;
        /** 赛选结果状态值：符合选择 */
        public const int TRUE = 1;
        /** 赛选结果状态值：不符合赛选并中断进一步赛选 */
        public const int FALSE_BREAK = 2;
        /** 赛选结果状态值：符合赛选并中断进一步赛选 */
        public const int TRUE_BREAK = 3;
    }
}