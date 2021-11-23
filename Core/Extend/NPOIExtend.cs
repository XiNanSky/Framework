/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-04                    *
* Nowtime:           02:53:17                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
#if NPOI
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using System.Data;

    public static class NPOIExtend
    {
        /// <summary>
        /// NPOI Excel ISheet 导入成 DataTable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ToDataTable(this ISheet sheet)
        {
            if (sheet == null) return null;
            //表头  
            var header = sheet.GetRow(sheet.FirstRowNum + 1);
            var columns = new List<int>();
            var dt = new DataTable();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn(string.Concat("Columns", i.ToString())));
                }
                else dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// NPOI 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetValueType(this ICell cell)
        {
            if (cell == null) return null;
            switch (cell.CellType)
            {
                case CellType.Blank:   //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String:  //STRING:  
                    return cell.StringCellValue;
                case CellType.Error:   //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default: return "=" + cell.CellFormula;
            }
        }
    }
#endif
}