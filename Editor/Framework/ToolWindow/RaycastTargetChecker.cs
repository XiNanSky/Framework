///* * * * * * * * * * * * * * * * * * * * * * * * 
//*Copyright(C) 2021 by xinansky 
//*All rights reserved. 
//*FileName:         Framework.ToolWindow 
//*Author:           XiNan 
//*Version:          0.1 
//*UnityVersion:     2020.3.5f1c1 
//*Date:             2021-07-04 
//*NOWTIME:          22:09:07 
//*Description:        
//*History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.ToolWindow
//{
//    using Framework.Editor;
//    using Framework.UI;
//    using UnityEditor;
//    using UnityEngine;
//    using UnityEngine.UI;

//    /// <summary> 物体显示属性状态 </summary>
//    public enum GameObjectState
//    {
//        None,
//        RaycastTarget,
//        Maskable,
//        Enabled
//    }

//    /// <summary> 批量修改RaycastTarget </summary>
//    public class RaycastTargetChecker : BaseWindowEditor
//    {
//        private static RaycastTargetChecker windows = null;
//        private static UIFullPanel uIPanel;

//        private MaskableGraphic[] graphics;

//        private Color enabledColor = Color.yellow;

//        private Color noneColor = Color.green;

//        private Color raycastTargetColor = Color.blue;

//        private Color maskableColor = Color.red;

//        private GameObjectState state = GameObjectState.None;

//        private bool negation = false;

//        public RaycastTargetChecker()
//        {
//            titleContent = new GUIContent("UIPanel Manamger");
//            minSize = new Vector2(300, 300);
//        }

//        public static void Open(UIFullPanel UIPanel)
//        {
//            uIPanel = UIPanel;
//            windows = windows ?? GetWindow<RaycastTargetChecker>();
//            windows.Show();
//        }

//        protected override void onGUI()
//        {
//            if (uIPanel == null) return;

//            ETool.BE.Vertical(() =>
//            {
//                ETool.AC.Label("属性绘制 颜色设置", GStyleTool.title);
//                raycastTargetColor = ETool.AC.FieldColor("RaycastTarget", raycastTargetColor);
//                maskableColor = ETool.AC.FieldColor("Maskable", maskableColor);
//                noneColor = ETool.AC.FieldColor("None", noneColor);
//                enabledColor = ETool.AC.FieldColor("Enabled", enabledColor);
//                ETool.BE.Horizontal(() =>
//                {
//                    state = (GameObjectState)EditorGUILayout.EnumPopup("属性状态", state);
//                    negation = ETool.AC.Toggle(negation, GUILayout.Width(20));
//                });
//            }, GStyleTool.box);

//            ETool.EA.Space(12);

//            Rect rect = GUILayoutUtility.GetLastRect();
//            GUI.color = new Color(0.0f, 0.0f, 0.0f, 0.25f);
//            GUI.DrawTexture(new Rect(0.0f, rect.yMin + 6.0f, Screen.width, 4.0f), EditorGUIUtility.whiteTexture);
//            GUI.DrawTexture(new Rect(0.0f, rect.yMin + 6.0f, Screen.width, 1.0f), EditorGUIUtility.whiteTexture);
//            GUI.DrawTexture(new Rect(0.0f, rect.yMin + 9.0f, Screen.width, 1.0f), EditorGUIUtility.whiteTexture);
//            GUI.color = Color.white;

//            graphics = uIPanel.GetComponentsInChildren<MaskableGraphic>();

//            vector = ETool.BE.ScrollView(() =>
//            {
//                for (int i = 0; i < graphics.Length; i++)
//                {
//                    switch (state)
//                    {
//                        case GameObjectState.RaycastTarget:
//                            if (negation == graphics[i].raycastTarget) continue;
//                            break;
//                        case GameObjectState.Maskable:
//                            if (negation == graphics[i].maskable) continue;
//                            break;
//                        case GameObjectState.Enabled:
//                            if (negation == graphics[i].enabled) continue;
//                            break;
//                    }
//                    DrawElement(graphics[i]);
//                    EditorUtility.SetDirty(graphics[i]);
//                }
//            }, vector, GStyleTool.scrollView);
//        }

//        /// <summary> 属性控制 </summary>
//        private void DrawElement(MaskableGraphic graphic)
//        {
//            Undo.RecordObject(graphic, "Modify RaycastTarget");       //撤销  ctrl + z
//            Undo.RecordObject(graphic, "Modify Maskable");
//            ETool.BE.Vertical(() =>
//            {
//                graphic.enabled = ETool.BE.ToggleGroup(() =>
//                {
//                    ETool.BE.Horizontal(() =>
//                         {
//                             ETool.AC.Label("GameObject");
//                             ETool.BE.DisabledGroup(() => { ETool.AC.FieldObject(graphic, false); }, true);
//                         });

//                    ETool.BE.Horizontal(() =>
//                         {
//                             ETool.AC.Label("RaycastTarget");
//                             graphic.raycastTarget = ETool.AC.Toggle(graphic.raycastTarget, GUILayout.Width(20));
//                         });

//                    ETool.BE.Horizontal(() =>
//                         {
//                             ETool.AC.Label("Maskable");
//                             graphic.maskable = ETool.AC.Toggle(graphic.maskable, GUILayout.Width(20));
//                         });
//                }, graphic.name, graphic.enabled);
//            }, GStyleTool.content);
//        }

//        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
//        private static void DrawGizmos(MaskableGraphic source, GizmoType gizmoType)
//        {
//            if (windows == null) return;
//            switch (windows.state)
//            {
//                case GameObjectState.None:
//                    DrawGizmosAttribute(source, windows.noneColor);
//                    break;
//                case GameObjectState.RaycastTarget:
//                    DrawGizmosAttribute(source, windows.raycastTargetColor);
//                    break;
//                case GameObjectState.Maskable:
//                    DrawGizmosAttribute(source, windows.maskableColor);
//                    break;
//                case GameObjectState.Enabled:
//                    DrawGizmosAttribute(source, windows.enabledColor);
//                    break;
//            }
//            SceneView.RepaintAll();
//        }

//        private static void DrawGizmosAttribute(MaskableGraphic source, Color color)
//        {
//            if (source.raycastTarget == true)
//            {
//                Vector3[] corners = new Vector3[4];
//                source.rectTransform.GetWorldCorners(corners);
//                Gizmos.color = color;
//                for (int i = 0; i < 4; i++)
//                {
//                    Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
//                }
//                if (Selection.activeGameObject == source.gameObject)
//                {
//                    Gizmos.DrawLine(corners[0], corners[2]);
//                    Gizmos.DrawLine(corners[1], corners[3]);
//                }
//            }
//        }

//        protected override void onEnable()
//        {
//            windows = this;
//        }

//        protected override void onDisable()
//        {
//            windows = null;
//        }
//    }
//}