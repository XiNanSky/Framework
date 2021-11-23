/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-04                    *
* Nowtime:           02:49:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Extend
{
#if NPOI
    using NPOI.SS.UserModel;
#endif
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    /// <summary>
    /// DateTabel 扩展方法
    /// </summary>
    public static class DataTableExtend
    {
#if NPOI
        /// <summary>
        /// NPOI Datable 导出成 Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcelISheet_NPOI(this DataTable dt, string file)
        {
            string fileExt = Path.GetExtension(file).ToLower();
            if (!fileExt.Contains(".xlsx") && !fileExt.Contains(".xls")) return;
            IWorkbook workbook = WorkbookFactory.Create(file);
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }
#endif
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static string GetStringValue(this DataTable data, int rowIndex, string pColumnName)
        {

            return GetValue(data, rowIndex, pColumnName).TOString();
        }

        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static int GetIntValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).TOInt();
        }

        /// <summary>
        /// 获取浮点型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static float GetFloatValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).TOFloat();
        }

        /// <summary>
        /// 获取Boolean
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static bool GetBooleanValue(this DataTable data, int rowIndex, string pColumnName)
        {
            string v = GetStringValue(data, rowIndex, pColumnName).ToLower();
            return v.TOBoolean();
        }

        public static decimal GetDecimalValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).TODecimal();
        }

        public static object GetValue(this DataTable data, int rowIndex, string pColumnName)
        {
            if (data == null || rowIndex < 0 || rowIndex >= data.Rows.Count) return null;

            return data.Rows[rowIndex].GetValue(pColumnName);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static string GetStringValue(this DataRow row, string pColumnName)
        {
            return GetValue(row, pColumnName).ToString();
        }

        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static int GetIntValue(this DataRow row, string pColumnName)
        {
            return GetStringValue(row, pColumnName).TOInt();
        }

        /// <summary>
        /// 获取浮点型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static float GetFloatValue(this DataRow row, string pColumnName)
        {
            return GetStringValue(row, pColumnName).TOFloat();
        }

        /// <summary>
        /// 获取Boolean
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static bool GetBooleanValue(this DataRow row, string pColumnName)
        {
            string v = GetStringValue(row, pColumnName);
            return v.TOBoolean();
        }

        public static object GetValue(this DataRow row, string pColumnName)
        {
            if (row == null) return null;
            if (pColumnName.IsNullOrEmpty()) return null;
            if (!row.Table.Columns.Contains(pColumnName)) return null;

            return row[pColumnName];
        }

        /// <summary>
        /// 判断是否为空或者没有数据
        /// </summary>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            return dt == null || dt.Rows.Count <= 0;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        public static bool IsNotEmpty(this DataTable dt)
        {
            return dt.IsNullOrEmpty() == false;
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        public static string Printf(this DataTable data)
        {
            StringBuilder str = new StringBuilder();
            foreach (DataRow dr2 in data.Rows)
            {
                foreach (var item in dr2.ItemArray)
                {
                    str.Append(item.TOString()).Append('|');
                }
                str.AppendLine();
            }
            return str.ToString();
        }

        /// <summary>
        /// 去除空行
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DataTable RemoveEmpty(this DataTable dt)
        {
            var removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        rowdataisnull = false;
                    }
                }
                if (rowdataisnull) removelist.Add(dt.Rows[i]);
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
            return dt;
        }
    }
}