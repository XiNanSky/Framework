using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// 目前暂时只用来设置MaterialPropertyBlock
    /// </summary>
    public class HsvFilter
    {
       
        public HsvFilter()
        {

        }
        //调试版本，可调参数
        public static void SetPropertyTest(DisplayObject target, float varRange, float varH, float varS, float varV, string varClr = null)
        {
            if (target == null)
                return;
            var mTmp = target.material;
            target.graphics.meshRenderer.SetPropertyBlock(null);
            if (varClr != null && varClr != "")
                mTmp.SetColor("_Color", ColorKit.HexToColor(varClr));
            mTmp.SetFloat("_varRange", varRange);
            mTmp.SetFloat("_m_H", varH);
            mTmp.SetFloat("_m_S", varS);
            mTmp.SetFloat("_m_V", varV);
        }
        public static void SetProperty(DisplayObject target, float varRange, float varH, float varS, float varV, string varClr = null)
        {
            if (target == null)
                return;
            MaterialPropertyBlock block = null;
            if ((target is Image) || (target is MovieClip))
                block = target.graphics.materialPropertyBlock;
            else
                block = target.paintingGraphics.materialPropertyBlock;

            if(varClr != null && varClr != "")
                block.SetColor("_Color", ColorKit.HexToColor(varClr));
            block.SetFloat("_varRange", varRange);
            block.SetFloat("_m_H", varH);
            block.SetFloat("_m_S", varS);
            block.SetFloat("_m_V", varV);
        }
        public static MaterialPropertyBlock GetMb(DisplayObject target)
        {
            if (target == null)
                return null;
            MaterialPropertyBlock block = null;
            if ((target is Image) || (target is MovieClip))
                block = target.graphics.materialPropertyBlock;
            else
                block = target.paintingGraphics.materialPropertyBlock;

            return block;
        }
    }
}
