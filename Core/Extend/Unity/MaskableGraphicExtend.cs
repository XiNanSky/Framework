/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-04
* Time:             00:34:28
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary> MaskableGraphic扩展类 </summary>
    public static class MaskableGraphicKit
    {
        /// <summary> 
        /// 设置材质
        /// </summary>
        public static void SetMat<T>(this T grap, Material material) where T : MaskableGraphic
        {
            grap.material = material;
            grap.SetMaterialDirty();
        }

        /// <summary> 
        /// 设置材质
        /// </summary>
        /// <param name="path">材质文件路径</param>
        //public static void SetMat<T>(this T grap, string path) where T : MaskableGraphic
        //{
        //    grap.material = UIMaterialKit.GetMaterial(path);
        //    grap.SetMaterialDirty();
        //}

        /// <summary> 
        /// 材质清空
        /// </summary>
        public static void MatClear<T>(this T grap) where T : MaskableGraphic
        {
            grap.material = null;
            grap.SetMaterialDirty();
        }
    }
}