/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Kit 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:00:40 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using System.Text;
    using UnityEngine;

    public static class TimeKit
    {
        /// <summary> 年、月、日单位时间（秒） </summary>
        public const int DAY = 86400, HOUR = 3600, MIN = 60;

        /// <summary> 年、月、日单位时间（毫秒） </summary>
        public const long DAY_MILLS = DAY * 1000L, HOUR_MILLS = HOUR * 1000L, MIN_MILLS = MIN * 1000L, SECOND_MILLS = 1000L, TWO_SECOND_MILLS = 2000L;

        /// <summary> 
        /// 格林威治时间UTC参照点：1970年1月1日0时0分0秒
        /// </summary>
        public static readonly DateTime GREENWICH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary> 
        /// 格林威治时间UTC参照点：1970年1月1日0时0分0秒
        /// </summary>
        public static readonly DateTime GREENWICH1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        /// <summary>
        /// 1毫秒
        /// </summary>
        public static TimeSpan Milliseconds { get; } = new TimeSpan(0, 0, 0, 0, 1);

        /// <summary>
        /// 1秒钟
        /// </summary>
        public static TimeSpan Second { get; } = new TimeSpan(0, 0, 1);

        /// <summary>
        /// 1分钟
        /// </summary>
        public static TimeSpan Minute { get; } = new TimeSpan(0, 1, 0);

        /// <summary>
        /// 1小时
        /// </summary>
        public static TimeSpan Hour { get; } = new TimeSpan(1, 0, 0);

        /// <summary>
        /// 1天
        /// </summary>
        public static TimeSpan Day { get; } = new TimeSpan(1, 0, 0, 0, 0);


        /// <summary>  </summary>
        public static long _remoteCurrentTimeMillisDistance = -1;

        /// <summary> 
        /// 服务器端的当前时间毫秒数,-1时为未取得服务器的时间 Java
        /// </summary>
        public static long CurrentTimeMillis
        {
            get
            {
                if (_remoteCurrentTimeMillisDistance == -1) return -1;
                return _remoteCurrentTimeMillisDistance + (int)(Time.realtimeSinceStartup * 1000);
            }
            set => _remoteCurrentTimeMillisDistance = value - (int)(Time.realtimeSinceStartup * 1000);
        }

        public static string Format(int time)
        {
            return Format(time * 1000L, "g");
        }


        public static long GetTodayStart()
        {
            DateTime t = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            long time = (t.ToUniversalTime().Ticks - GREENWICH.Ticks) / 10000;
            return time;
        }

        public static long GetTodayServerStart()
        {
            long ticks = GREENWICH.Ticks + CurrentTimeMillis * 10000L;
            DateTime dt = TimeZoneInfo.ConvertTimeToUtc(new DateTime(ticks, DateTimeKind.Utc));
            DateTime t = Convert.ToDateTime(dt.ToString("yyyy-MM-dd 00:00:00"));
            long time = (t.ToUniversalTime().Ticks - GREENWICH.Ticks) / 10000;
            return time;
        }

        /// <summary> 
        /// 判断是否同日
        /// </summary>
        public static bool IsToday(long time1, long time2)
        {

            long ticks = GREENWICH.Ticks + time1 * 10000L;
            DateTime dt1 = TimeZoneInfo.ConvertTimeToUtc(new DateTime(ticks, DateTimeKind.Utc));

            ticks = GREENWICH.Ticks + time2 * 10000L;
            DateTime dt2 = TimeZoneInfo.ConvertTimeToUtc(new DateTime(ticks, DateTimeKind.Utc));

            if (dt1.Date == dt2.Date) return true;

            return false;
        }

        /// <summary> 
        /// 通过指定时间获取DateTime
        /// </summary>
        public static DateTime GetDateTime(long time)
        {
            long ticks = GREENWICH1.Ticks + time * 10000;
            return new DateTime(ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// 获取时间搓
        /// </summary>
        public static long GetTime(DateTime time)
        {
            return (time.Ticks - GREENWICH1.Ticks) / 10000;
        }

        public static string Format(long time)
        {
            return Format(time, "g");
        }

        /// <summary> 获取时间(s)的字符串表示 </summary>
        /// <param name="time">时间(s)</param>
        /// <param name="format">格式</param>
        public static string Format(int time, string format)
        {
            return Format(time * 1000L, format);
        }

        /// <summary> 格式化时间，参数：格林威治时间，格式化格式（具体见文件末尾） </summary>
        public static string Format(long time, string format)
        {
            if (format == null) format = "g";
            long ticks = GREENWICH.Ticks + time * 10000L;
            DateTime dt = TimeZoneInfo.ConvertTimeToUtc(new DateTime(ticks, DateTimeKind.Utc));
            return dt.ToString(format);
        }

        /// <summary> 获取时间倒计时字符串表示(ms)（例如：01:59:08） </summary>
        public static string GetCountdown(long time)
        {
            return getCountdown((int)(time / 1000));
        }

        public static DateTime[] getDateTimesArray(DateTime time)
        {
            DateTime[] dates = new DateTime[42];
            int days = DateTime.DaysInMonth(time.Year, time.Month);
            DateTime firstDay = new DateTime(time.Year, time.Month, 1);
            int weekDay = (int)firstDay.DayOfWeek;
            int lastDays = weekDay == 0 ? 7 : weekDay;
            int index = 0;
            DateTime temp;
            for (int i = weekDay; i > 0; i--)
            {
                temp = firstDay.AddDays(-i);
                dates[index++] = temp;
            }
            dates[index++] = firstDay;
            for (int i = 1; i < 42 - lastDays; i++)
            {
                temp = firstDay.AddDays(i);
                dates[index++] = temp;
            }
            return dates;
        }

        /// <summary> 
        /// 获取当前时间
        /// </summary>
        public static string getCurrTime()
        {
            return Format(CurrentTimeMillis, "yyyy/MM/dd HH:mm:ss");
        }

        /// <summary> 
        /// 获取时间倒计时字符串表示(s)（例如：01:59:08）
        /// </summary>
        public static string getCountdown(int time)
        {
            if (time <= 0) return "00:00:00";
            int hour = time / HOUR;
            int min = (time % HOUR) / MIN;
            int sec = time % MIN;
            return getCountdown(hour, min, sec);
        }

        /// <summary> 
        /// 多少时间前
        /// </summary>
        public static string getPreHumanityTime(long time)
        {
            if (time <= 0) return "--";
            time = CurrentTimeMillis - time;
            if (time <= MIN_MILLS) return "";
            long day = time / DAY_MILLS;
            time %= DAY_MILLS;
            long hour = time / HOUR_MILLS;
            time %= HOUR_MILLS;
            long min = time / MIN_MILLS;
            StringBuilder buff = new StringBuilder();
            if (day > 0) buff.Append(day).Append("天 ");
            if (day > 0 || hour > 0) buff.Append(hour).Append("小时 ");
            if (day > 0 || hour > 0 || min > 0) buff.Append(min).Append("分前"); ;
            return buff.ToString();
        }

        /// <summary> 
        /// 获取时间倒计时字符串表示（例如：01:59:08）
        /// </summary>
        public static string getCountdown(int hour, int min, int sec)
        {
            if (hour < 0) hour = 0;
            if (min < 0) min = 0;
            if (sec < 0) sec = 0;
            StringBuilder buff = new StringBuilder();
            if (hour < 10) buff.Append('0');
            buff.Append(hour).Append(':');
            if (min < 10) buff.Append('0');
            buff.Append(min).Append(':');
            if (sec < 10) buff.Append('0');
            buff.Append(sec);
            return buff.ToString();
        }

        /// <summary> 
        /// 与当前时间比较 如果小于当前时间为Ture
        /// </summary>
        public static bool CompareNowTime(this DateTime dateTime)
        {
            return DateTime.Now > dateTime;
        }

        /// <summary> 程序执行时间测试 </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns> 返回(秒)单位 </returns> 比如: 0.00239秒
        public static string ExecDateDiff(long dateBegin, long dateEnd)
        {
            var ts = new TimeSpan(dateEnd) - new TimeSpan(dateBegin);
            return ts.TotalMilliseconds.ToString();
        }

        /// <summary> 程序执行时间测试 </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns> 返回(秒)单位 </returns> 比如: 0.00239秒
        public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            return ExecDateDiff(dateBegin.Ticks, dateEnd.Ticks);
        }

        /// <summary>
        /// 求离最近发表时间的函数
        /// </summary>
        /// <returns> 返回时间描述 </returns>
        public static string DateStringFromNow(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60) return dt.ToShortDateString();
            else if (span.TotalDays > 30) return "1个月前";
            else if (span.TotalDays > 14) return "2周前";
            else if (span.TotalDays > 7) return "1周前";
            else if (span.TotalDays > 1) return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            else if (span.TotalHours > 1) return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            else if (span.TotalMinutes > 1) return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            else if (span.TotalSeconds >= 1) return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            else return "1秒前";
        }

        /// <summary>
        /// 反加两个日期之间任何一个时间单位
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new
            TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="writeDate">输入日期</param>
        /// <param name="n">比较天数</param>
        /// <returns>大于天数返回true，小于返回false</returns>
        public static bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);
            WriteDate = WriteDate.AddDays(n);
            return Today < WriteDate;
        }

        /// <summary> 
        /// 获取倒计时
        /// </summary>
        public static string GetDisTime(long dateBegin, long dateEnd, string formmat = "HH:mm:ss")
        {
            var v = new TimeSpan(dateEnd) - new TimeSpan(dateBegin);
            return v.ToString(formmat);
        }

        /// <summary> 
        /// 时间磋转换TimeSpan
        /// </summary>
        public static TimeSpan RevertMillsecToTimeSpane(long msec)
        {
            long day = 0;
            long hour = 0;
            long minute = 0;
            long second = 0;
            int millsec = 0;
            if (msec > 1000)
            {
                second = msec / 1000;
                millsec = (int)(msec % 1000);
            }
            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            if (hour > 24)
            {
                day = hour / 24;
                hour = hour % 24;
            }
            return new TimeSpan((int)day, (int)hour, (int)minute, (int)second, millsec);
        }
    }
}

/*
  format参数格式详细用法:
  1.标准格式代表字符
    格式字符    关联属性/说明
    d         短日期 (样式示例：08/30/2006)
    D         长日期 (样式示例：Wednesday, 30 August 2006)
    f         完整日期和时间（长日期和短时间） (样式示例：Wednesday, 30 August 2006 23:21)
    F         完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 23:22:02)
    g         常规（短日期和短时间） (样式示例：08/30/2006 23:22)
    G         常规（短日期和长时间） (样式示例：08/30/2006 23:23:11)
    m,M       MonthDayPattern
    r,R       RFC1123Pattern
    s         使用当地时间的 SortableDateTimePattern（基于 ISO 8601）
    t         短时间（无秒） (样式示例：23:24
    T         长时间（带秒） (样式示例：23:24:30
    u         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：2006-08-30 23:25:10Z)
    U         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 15:25:37)
    y,Y       YearMonthPattern
  
  2.自定义格式符号
    下表列出了可被合并以构造自定义模式的模式。这些模式是区分大小写的；例如，识别“MM”，但不识别“mm”。
    如果自定义模式包含空白字符或用单引号括起来的字符，则输出字符串页也将包含这些字符。
    未定义为格式模式的一部分或未定义为格式字符的字符按其原义复制。
    格式符号    说明
    d         月中的某一天。一位数的日期没有前导零。
    dd        月中的某一天。一位数的日期有一个前导零。
    ddd       周中某天的缩写名称，在 AbbreviatedDayNames 中定义。
    dddd      周中某天的完整名称，在 DayNames 中定义。
    M         月份数字。一位数的月份没有前导零。
    MM        月份数字。一位数的月份有一个前导零。
    MMM       月份的缩写名称，在 AbbreviatedMonthNames 中定义。
    MMMM      月份的完整名称，在 MonthNames 中定义。
    y         不含纪元的年份，且无前导零。（例如：2008年，纪元为20，非纪元年份为08，显示8）
    yy        不含纪元的年份。且有前导零。（例如：2008年，纪元为20，非纪元年份为08，显示08）
    yyyy      包含纪元的四位数年份。（例如：2008年，纪元为20，非纪元年份为08，显示2008）
    gg        时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。
    h         12 小时制的小时。一位数的小时数无前导零。
    hh        12 小时制的小时。一位数的小时数有前导零。
    H         24 小时制的小时。一位数的小时数无前导零。
    HH        24 小时制的小时。一位数的小时数有前导零。
    m         分钟。一位数的分钟数无前导零。
    mm        分钟。一位数的分钟数有前导零。
    s         秒。一位数的秒数无前导零。
    ss        秒。一位数的秒数有前导零。
    f         秒的小数精度为一位。其余数字被截断。
    ff        秒的小数精度为两位。其余数字被截断。
    fff       秒的小数精度为三位。其余数字被截断。
    ffff      秒的小数精度为四位。其余数字被截断。
    fffff     秒的小数精度为五位。其余数字被截断。
    ffffff    秒的小数精度为六位。其余数字被截断。
    fffffff   秒的小数精度为七位。其余数字被截断。
    t         在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。
    tt        在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。
    z         时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数无前导零。例如，太平洋标准时间是“-8”。
    zz        时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。
    zzz       时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。
    :         在 TimeSeparator 中定义的默认时间分隔符。
    /         在 DateSeparator 中定义的默认日期分隔符。

  3.特殊情况注意：
    只有2中列出的格式符号才能用于创建自定义模式；在1中列出的标准格式字符不能用于创建自定义模式。
    1中定义的标准格式简称使用时都是一个字符，2中自定义模式使用时至少需要两个字符；
    若使用自定义格式符号为一个字符的为避免和标准格式简称冲突，需要在前面加上“%”
    例如:
    format(time,"d");       此时的“d”是标准格式简称，返回1中定义的短日期模式。
    format(time,"%d");      此时的“d”是自定义符号，“%”指定自定义模式，返回月中的某天。
    format(time,"d ");      此时的“d”是自定义符号，字符数大于等于2，返回后面跟有一个空白字符的月中的某天。
    2中的格式符号可以随意组合
    例如:
    format(time,"yyyy年MM月")
    format(time,"yyyy/MM/dd HH:mm:ss") 
*/

/*
    TimeSpan 结构  表示一个时间间隔。

    命名空间:System 程序集:mscorlib（在 mscorlib.dll 中）

    说明： 
    1.DateTime值类型代表了一个从公元0001年1月1日0点0分0秒到公元9999年12月31日23点59分59秒之间的具体日期时刻。因此，
    你可以用DateTime值类型来描述任何在想象范围之内的时间。TimeSpan值包含了许多属性与方法，用于访问或处理一个TimeSpan值，

    其中的五个重载方法之一的结构 TimeSpan( int days, int hours, int minutes, int seconds )

    下面的列表涵盖了其中的一部分方法及属性解释

    Add：与另一个TimeSpan值相加。

    Days:返回用天数计算的TimeSpan值。

    Duration:获取TimeSpan的绝对值。

    Hours:返回用小时计算的TimeSpan值

    Milliseconds:返回用毫秒计算的TimeSpan值。

    Minutes:返回用分钟计算的TimeSpan值。

    Negate:返回当前实例的相反数。

    Seconds:返回用秒计算的TimeSpan值。

    Subtract:从中减去另一个TimeSpan值。

    Ticks:返回TimeSpan值的tick数。

    TotalDays:返回TimeSpan值表示的天数。

    TotalHours:返回TimeSpan值表示的小时数。

    TotalMilliseconds:返回TimeSpan值表示的毫秒数。

    TotalMinutes:返回TimeSpan值表示的分钟数。

    TotalSeconds:返回TimeSpan值表示的秒数。

    负数

    上面是较晚的日期减较早的日期，所以各属性值为正数，如果是较早的日期减较晚的日期，则属性值为负数。

    ASP.NET 中，两个时间相减，得到一个 TimeSpan 实例，TimeSpan 有一些属性：Days、TotalDays、Hours、TotalHours、Minutes、TotalMinutes、Seconds、TotalSeconds、Ticks，注意没有 TotalTicks。

    举例说明

    •时间 1 是 2010-1-2 8:43:35；

    时间 2 是 2010-1-12 8:43:34。

    用时间 2 减时间 1，得到一个 TimeSpan 实例。

    那么时间 2 比时间 1 多 9 天 23 小时 59 分 59 秒。

    那么，Days 就是 9，Hours 就是 23，Minutes 就是 59，Seconds 就是 59。

    再来看 Ticks，Tick 是一个计时周期，表示一百纳秒，即一千万分之一秒，那么 Ticks 在这里表示总共相差多少个时间周期，即：9 * 24 * 3600 * 10000000 + 23 * 3600 * 10000000 +59 * 60 * 10000000 + 59 * 10000000 = 8639990000000。3600 是一小时的秒数。

    TotalDays 就是把 Ticks 换算成日数，即：8639990000000 / (10000000 * 24 * 3600) = 9.99998842592593。

    TotalHours 就是把 Ticks 换算成小时数，即：8639990000000 / (10000000 * 3600) = 239.999722222222。

    TotalMinutes 就是把 Ticks 换算成分钟数，即：8639990000000 / (10000000 * 60) = 14399.9833333333。

    TotalSeconds 就是把 Ticks 换算成秒数，即：8639990000000 / (10000000) = 863999。

    1. Date数值必须以数字符号"#"括起来。

    2. Date数值中的日期数据可有可无，如果有必须符合格式"m/d/yyyy"。

    3. Date数值中的时间数据可有可无，如果有必须和日期数据通过空格分开，并且时分秒之间以":"分开。

    一．DateTime和TimeSpan的关系和区别：

    DateTime和TimeSpan是Visual Basic .Net中用以处理时间日期类型数据的二个主要的结构，这二者的区别在于，
    DatTime表示一个固定的时间，而TimeSpan表示的是一个时间间隔， 
    即一段时间。在下面介绍的程序示例中，TimeSpan就用以当前时间和给定时间之差。

    DateTime结构和TimeSpan结构提供了丰富的方法和属性，

    属性 说明
    Date 获取此实例的日期部分。
    Day 获取此实例所表示的日期为该月中的第几天。
    DayOfWeek 获取此实例所表示的日期是星期几。
    DayOfYear 获取此实例所表示的日期是该年中的第几天。
    Hour 获取此实例所表示日期的小时部分。
    Millisecond 获取此实例所表示日期的毫秒部分。
    Minute 获取此实例所表示日期的分钟部分。
    Month 获取此实例所表示日期的月份部分。
    Now 创建一个DateTime实例，它是此计算机上的当前本地日期和时间。
    Second 获取此实例所表示日期的秒部分。
    TimeOfDay 获取此实例的当天的时间。
    Today 获取当前日期。
    Year 获取此实例所表示日期的年份部分。

    Add 将指定的TimeSpan的值加到此实例的值上。
    AddDays 将指定的天数加到此实例的值上。
    AddHours 将指定的小时数加到此实例的值上。
    AddMilliseconds 将指定的毫秒数加到此实例的值上。
    AddMinutes 将指定的分钟数加到此实例的值上。
    AddMonths 将指定的月份数加到此实例的值上。
    AddSeconds 将指定的秒数加到此实例的值上。
    AddYears 将指定的年份数加到此实例的值上。
    DaysInMonth 返回指定年份中指定月份的天数。
    IsLeapYear 返回指定的年份是否为闰年的指示。
    Parse 将日期和时间的指定字符串表示转换成其等效的DateTime实例。
    Subtract 从此实例中减去指定的时间或持续时间。
    ToLongDateString 将此实例的值转换为其等效的长日期字符串表示形式。
    ToLongTimeString 将此实例的值转换为其等效的长时间字符串表示形式。
    ToShortTimeString 将此实例的值转换为其等效的短时间字符串表示形式。
    ToShortDateString 将此实例的值转换为其等效的短日期字符串表示形式。
 */


//  TimeSpan ToString formmat
//  c: 00:00:00
//  g: 0:00:00
//  G: 0:00:00:00.0000000
//  hh\:mm\:ss: 00:00:00
//  %m' min.': 0 min.

