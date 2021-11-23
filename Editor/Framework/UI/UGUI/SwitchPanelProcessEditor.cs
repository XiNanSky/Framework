///** 
// *Copyright(C) 2021 by xinansky 
// *All rights reserved. 
// *FileName:         Framework.UICompnoent 
// *Author:           XiNan 
// *Version:          0.1 
// *UnityVersion:     2020.3.5f1c1 
// *Date:             2021-06-30 
// *Description:        
// *History:          
//*/

//namespace Framework.Editor.UICompnoent
//{
//    using Framework.UI.Process;
//    using UnityEditor;

//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(OpenPanelProcess), true)]
//    public class SwitchPanelProcessEditor : Base2DEditor
//    {
//        private OpenPanelProcess obj;
//        //private ColorBlock color = new ColorBlock()
//        //{
//        //    normalColor = Color.white,
//        //    highlightedColor = Color.white,
//        //    pressedColor = Color.white,
//        //    selectedColor = Color.white,
//        //    disabledColor = Color.white,
//        //    colorMultiplier = 1,
//        //    fadeDuration = 0.1f,
//        //};

//        protected override void init()
//        {
//            base.init();
//            obj = target as OpenPanelProcess;
//            //color = obj.colors;
//            UndoNmae = "SwitchPanelProcess";
//        }

//        protected override void inspector()
//        {
//            vector = ETool.BE.ScrollView(() =>
//            {
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("正常状态");
//                //    color.normalColor = ETool.AC.FieldColor(color.normalColor);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("高亮状态");
//                //    color.highlightedColor = ETool.AC.FieldColor(color.highlightedColor);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("点击状态");
//                //    color.pressedColor = ETool.AC.FieldColor(color.pressedColor);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("选中状态");
//                //    color.selectedColor = ETool.AC.FieldColor(color.selectedColor);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("禁用状态");
//                //    color.disabledColor = ETool.AC.FieldColor(color.disabledColor);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("颜色系数");
//                //    color.colorMultiplier = ETool.AC.Slider(color.colorMultiplier, 1, 5);
//                //});
//                //ETool.BE.Horizontal(() =>
//                //{
//                //    ETool.AC.LabelPrefix("变化时长");
//                //    color.fadeDuration = ETool.AC.FieldFloat(color.fadeDuration);
//                //});
//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("面板名");
//                    obj.PanelName = ETool.AC.FieldText(obj.PanelName);
//                });
//            }, vector);
//        }

//        protected override void change()
//        {
//            foreach (OpenPanelProcess item in targets)
//            {
//                item.PanelName = obj.PanelName;
//            }
//        }

//        protected override void disable()
//        {
//            base.disable();
//        }
//    }
//}