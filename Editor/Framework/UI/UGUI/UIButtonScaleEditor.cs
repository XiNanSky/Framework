///* * * * * * * * * * * * * * * * * * * * * * * *
//* DefaultCompany
//* FileName:         Framework.UICompnoent
//* Author:           XiNan
//* Version:          0.1
//* UnityVersion:     2020.3.5f1c1
//* Date:             2021-05-08
//* Time:             10:35:02
//* E-Mail:           1398581458@qq.com
//* Description:        
//* History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.Editor.UICompnoent
//{
//    using Framework.UI;
//    using UnityEditor;

//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(UIScale), true)]

//    public class UIButtonScaleEditor : Base2DEditor
//    {
//        private SerializedProperty Scale;
//        private SerializedProperty ScaleValueStart;
//        private SerializedProperty ScaleValueEnd;
//        private SerializedProperty ScaleTime;
//        private SerializedProperty ScaleEase;

//        protected override void init()
//        {
//            UndoNmae = "UIButtonScale";
//            Scale = serObjs.FindProperty("Scale");
//            ScaleTime = serObjs.FindProperty("ScaleTime");
//            ScaleEase = serObjs.FindProperty("ScaleEase");
//            ScaleValueEnd = serObjs.FindProperty("ScaleValueEnd");
//            ScaleValueStart = serObjs.FindProperty("ScaleValueStart");
//        }

//        protected override void inspector()
//        {
//            ETool.AC.FieldProperty(Scale);
//            ETool.AC.FieldProperty(ScaleTime);
//            ETool.AC.FieldProperty(ScaleEase);
//            ETool.AC.FieldProperty(ScaleValueStart);                 
//            ETool.AC.FieldProperty(ScaleValueEnd);
//        }

//        protected override void change()
//        {
       
//        }
//    }
//}