/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-04                    *
* Nowtime:           02:48:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Kit
{
    using System.Collections.Generic;
    using System.IO;

#if NPOI

    using NPOI.SS.UserModel;

    /// <summary>
    /// Excel 表头
    /// </summary>
    public class ExcelHead
    {
        /// <summary>
        /// 注释描述 第一行
        /// </summary>
        public string content;

        /// <summary>
        /// 属性名 第二行
        /// </summary>
        public string columnname;

        /// <summary>
        /// 类型 第三行
        /// </summary>
        public string columntype;

        public ExcelHead() { }

        public ExcelHead(string content, string columnname, string columntype)
        {
            this.content = content;
            this.columnname = columnname;
            this.columntype = columntype;
        }
    }

    /// <summary>
    /// Excel 方法库
    /// </summary>
    public static class ExcelKit
    {
        /// <summary>
        /// NPOI 读取 Excel 的全部 Sheet
        /// </summary>
        public static List<ISheet> ReadExcelToSheet_NPOI(string @path)
        {
            if (!File.Exists(@path)) { return null; }
            using (FileStream fs = File.Open(@path, FileMode.Open, FileAccess.Read))
            {
                var Mybook = WorkbookFactory.Create(fs, ImportOption.TextOnly);
                var sheets = new List<ISheet>();
                foreach (var item in Mybook.GetAllNames())
                {
                    sheets.Add(Mybook.GetSheet(item.SheetName));
                }
                Mybook.Close();
                return sheets;
            }
        }
    }

#endif
}