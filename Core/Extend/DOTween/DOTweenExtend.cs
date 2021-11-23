/** 
 *Copyright(C) 2021 by DefaultCompany 
 *All rights reserved. 
 *FileName:         Framework.DOTweenExt 
 *Author:           XiNan 
 *Version:          0.1 
 *UnityVersion:     2020.3.5f1c1 
 *Date:             2021-05-01 
 *Description:      DOTween管理类 使用DOTween请使用此类 中间类  
 *History:          
*/


namespace Framework
{
#if DOTween
    using DG.Tweening;
    using UnityEngine;

    public static class DOTweenExtend
    {
        /// <summary> 围绕Z轴缩放一圈 </summary>
        public static void DAScaleZ(this Transform trans, float times)
        {
            trans.DOScaleZ(0, times).OnComplete(() =>
            {
                trans.DOScaleZ(1, times);
            });
        }

        /// <summary> 围绕X轴缩放一圈 </summary>
        public static void DAScaleX(this Transform trans, float times)
        {
            trans.DOScaleX(0, times).OnComplete(() =>
            {
                trans.DOScaleX(1, times);
            });
        }

        /// <summary> 围绕Y轴缩放一圈 </summary>
        public static void DAScaleY(this Transform trans, float times)
        {
            trans.DOScaleY(0, times).OnComplete(() =>
            {
                trans.DOScaleY(1, times);
            });
        }

        /// <summary> 围绕XY轴缩放一圈 </summary>
        public static void DAScaleXY(this Transform trans, float times)
        {
            DAScaleX(trans, times);
            DAScaleY(trans, times);
        }

        /// <summary> 围绕XY轴缩放一圈 </summary>
        public static void DAScaleXZ(this Transform trans, float times)
        {
            DAScaleX(trans, times);
            DAScaleZ(trans, times);
        }

        /// <summary> 围绕XY轴缩放一圈 </summary>
        public static void DAScaleYZ(this Transform trans, float times)
        {
            DAScaleY(trans, times);
            DAScaleZ(trans, times);
        }

        /// <summary> 围绕XYZ轴缩放一圈 </summary>
        public static void DAScaleXYZ(this Transform trans, float times)
        {
            trans.DOScale(0, times).OnComplete(() =>
            {
                trans.DOScale(1, times);
            });
        }
    }
#endif
}