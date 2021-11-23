//namespace Framework.Editor.ToolWindow
//{
//    using System.IO;
//    using UnityEditor;
//    using UnityEngine;
//    using Framework.Function.PointSwitch;
//    using Framework.Editor;
//    using System.Text.RegularExpressions;
//    using System.Text;
//    using System.Collections.Generic;
//    using HighlightingSystem;

//    public class PointSwitchStepGameObjectEditor : BaseWindowEditor
//    {
//        private static new EditorWindow window;
//        [MenuItem("ETool/Windows/Point Switch Gameobject Editor %#v")]
//        public static void Open()
//        {
//            window = window ?? GetWindow<PointSwitchStepGameObjectEditor>(true, "Point Switch Gameobject Editor", true);//创建窗口
//            window.wantsMouseMove = true;
//            window.Show(true);//展示         
//        }

//        private int buttonWidth = 120;

//        public PointSwitchStepGameObjectEditor()
//        {
//            minSize = new Vector2(900, 800);
//            objects = new PointSwitchStepGameObjectDatas();
//        }

//        private string filename;
//        private string filepath;
//        private string itemname;

//        //保存数据
//        private Transform hightLighter;
//        private PointSwitchStepGameObjectData tempData = new PointSwitchStepGameObjectData();

//        private PointSwitchStepGameObjectDatas objects = new PointSwitchStepGameObjectDatas();

//        protected override void onEnable()
//        {
//            filepath = Application.streamingAssetsPath;
//            filename = EditorPrefs.GetString("PointSwicthStepObjDataFileName");
//            itemname = EditorPrefs.GetString("itemname");
//            tempData.isAssigned = false;
//        }

//        protected override void onGUI()
//        {
//            ETool.BE.Horizontal(() =>
//            {
//                ETool.AC.FieldLabel($"路径 : {filepath}");
//                ETool.EA.Separator();
//                filename = ETool.AC.FieldText("文件名", filename);
//            });

//            ETool.BE.Horizontal(() =>
//            {
//                if (ETool.AC.Button("Load")) { LoadData(); }
//                if (ETool.AC.Button("Save")) { SaveData(); }
//                if (ETool.AC.Button("Add")) { AddOperate(); }
//                if (ETool.AC.Button("Show")) { ShowAll(); }
//                if (ETool.AC.Button("Hide")) { HideAll(); }
//            });

//            ETool.BE.Horizontal(() =>
//            {
//                itemname = ETool.AC.FieldText("序号", itemname);
//                if (ETool.AC.Button("Find by ID")) { FindItem(itemname); }
//                if (ETool.AC.Button("Close by ID")) { CloseItem(itemname); }
//            });

//            ETool.BE.Horizontal(() =>
//            {
//                if (ETool.AC.Button("Clear Copied Content")) { ClearCopy(); }
//                if (ETool.AC.Button("Clear Copied HighLighter")) { ClearHighLighter(); }
//            });

//            vector = ETool.BE.ScrollView(() =>
//            {
//                for (int i = 0; i < objects.operates.Count; i++)
//                {
//                    objects.operates[i].Step = i;
//                    OperateView(objects.operates[i]);
//                }
//            }, vector, GStyleTool.scrollView);
//        }

//        private void OperateView(PointSwitchStepGameObjectData operate)
//        {
//            if (operate == null) return;
//            ETool.BE.Vertical(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.Label($"Step : {operate.Step}");

//                    if (ETool.AC.Button("Tween", GUILayout.Width(buttonWidth))) operate.infoOnoff = !operate.infoOnoff;

//                    if (ETool.AC.Button("Highlighter", GUILayout.Width(buttonWidth))) operate.highlighterOnoff = !operate.highlighterOnoff;

//                    if (!tempData.isAssigned) {
//                        if (ETool.AC.Button("Copy", GUILayout.Width(buttonWidth))) { Copy(operate); }
//                    } else {
//                        if (ETool.AC.Button("Paste", GUILayout.Width(buttonWidth))) { Paste(operate); }
//                    }

//                    if (ETool.AC.Button("Del", GUILayout.Width(buttonWidth)))
//                    {
//                        objects.operates.RemoveAt(operate.Step);
//                    }
//                });

//                if (operate.infoOnoff)
//                {
//                    ETool.BE.Vertical(() =>
//                    {
//                        TweenView(operate);
//                    }, GStyleTool.UIContent);
//                }

//                if (operate.highlighterOnoff)
//                {   //物体高亮
//                    ETool.BE.Vertical(() =>
//                    {
//                        if (ETool.AC.Button("Add")) operate.HighlighterTrans.Add(default);

//                        for (int i = 0; i < operate.HighlighterTrans.Count; i++)
//                        {
//                            operate.HighlighterTrans[i] = HighlighterTransListView(i, operate.HighlighterTrans[i], operate);
//                        }
//                    }, GStyleTool.UIContent);
//                }
//            }, GStyleTool.button);
//        }

//        private void TweenView(PointSwitchStepGameObjectData operate)
//        {
//            //渐变设置
//            ETool.BE.Horizontal(() =>
//            {
//                operate.Overlay = ETool.AC.Toggle("覆盖显示", operate.Overlay);
//                operate.OccIuder = ETool.AC.Toggle("遮罩", operate.OccIuder);
//                operate.ForceRender = ETool.AC.Toggle("渲染闭合物体", operate.ForceRender);
//            });
//            ETool.BE.Horizontal(() =>
//            {
//                operate.TweenGradient = ETool.AC.FieldGradient("渐变颜色", operate.TweenGradient);
//                operate.ConstantColor = ETool.AC.FieldColor("描边颜色", operate.ConstantColor);
//            });
//            ETool.BE.Horizontal(() =>
//            {
//                operate.TweenLoopMode = ETool.AC.PopupEnum("渐变循环模式", operate.TweenLoopMode);
//                operate.TweenDuration = ETool.AC.Slider("渐变时长", operate.TweenDuration, 0, 10);
//            });
//            ETool.BE.Horizontal(() =>
//            {
//                operate.TweenEasing = ETool.AC.PopupEnum("渐变显示模式", operate.TweenEasing);
//                operate.screenSpace_Z = ETool.AC.Slider("深度值", operate.screenSpace_Z, 0, 2);
//            });
//            ETool.BE.Horizontal(() =>
//            {
//                operate.Target = ETool.AC.FieldObject(operate.Target, "镜头聚焦物体", true);
//                operate.Delay = ETool.AC.Slider("渐变等待延时", operate.Delay, 0, 10);
//            });
//        }

//        private Transform HighlighterTransListView(int index, Transform trans, PointSwitchStepGameObjectData operate)
//        {
//            ETool.BE.Horizontal(() =>
//            {
//                trans = ETool.AC.FieldObject(trans, index.ToString(), true);
//                if (trans == null || trans ==default)
//                    ETool.AC.TextArea("", GUILayout.Width(120));

//                else
//                    ETool.AC.TextArea(trans.GetInstanceID().ToString(), GUILayout.Width(120));

//                if (!tempData.isCopied) {
//                    if (ETool.AC.Button("Copy", GUILayout.Width(90))) {
//                        CopyHighLighter(operate, index);
//                    }
//                } else {
//                    if (ETool.AC.Button("Paste", GUILayout.Width(90))) {
//                        trans = hightLighter;
//                    }
//                }
//                if (ETool.AC.Button("Del", GUILayout.Width(90)))
//                {
//                    operate.HighlighterTrans.Remove(trans);
//                }
//            });
//            return trans;
//        }

//        private void ShowAll()
//        {
//            for (int i = 0; i < objects.operates.Count; i++)
//            {
//                objects.operates[i].infoOnoff = true;
//                objects.operates[i].highlighterOnoff = true;
//            }
//        }

//        private void HideAll()
//        {
//            for (int i = 0; i < objects.operates.Count; i++)
//            {
//                objects.operates[i].infoOnoff = false;
//                objects.operates[i].highlighterOnoff = false;
//            }
//        }

//        private void FindItem(string id)
//        {
//            Regex reg = new Regex(@"(^[\-0-9][0-9]*?)$");//判断是否能类型转换
//            if (reg.IsMatch(id))
//            {
//                int idNum = int.Parse(id);
//                if (idNum < 0 || idNum > objects.operates.Count)
//                {
//                    EditorUtility.DisplayDialog("Find", idNum + " is illegal, you can only enter a positive integer numbers less than " + objects.operates.Count, "确定");
//                }
//                else
//                {
//                    objects.operates[idNum].highlighterOnoff = true;
//                    objects.operates[idNum].infoOnoff = true;
//                }
//            }
//            else
//            {
//                EditorUtility.DisplayDialog("Find", id + " is illegal, you can only enter an integer number.", "确定");
//            }
//        }

//        private void CloseItem(string id)
//        {
//            Regex reg = new Regex(@"(^[\-0-9][0-9]*?)$");//判断是否能类型转换
//            if (reg.IsMatch(id))
//            {
//                int idNum = int.Parse(id);
//                if (idNum < 0 || idNum > objects.operates.Count)
//                {
//                    Debug.Log(idNum + " is illegal, you can only enter a positive numbers less than " + objects.operates.Count);
//                }
//                else
//                {
//                    if (objects.operates[idNum].infoOnoff)
//                    {
//                        objects.operates[idNum].highlighterOnoff = false;
//                        objects.operates[idNum].infoOnoff = false;
//                    }
//                }
//            }
//            else
//            {
//                EditorUtility.DisplayDialog("Close", id + " is illegal, you can only enter an integer number.", "确定");
//            }
//        }

//        private void Copy(PointSwitchStepGameObjectData operate)
//        {
//            tempData.Overlay = operate.Overlay;
//            tempData.OccIuder = operate.OccIuder;
//            tempData.ForceRender = operate.ForceRender;
//            tempData.TweenGradient = operate.TweenGradient;
//            tempData.ConstantColor = operate.ConstantColor;
//            tempData.TweenLoopMode = operate.TweenLoopMode;
//            tempData.TweenDuration = operate.TweenDuration;
//            tempData.TweenEasing = operate.TweenEasing;
//            tempData.screenSpace_Z = operate.screenSpace_Z;
//            tempData.Target = operate.Target;
//            tempData.Delay = operate.Delay;

//            tempData.isAssigned = true;
//        }

//        private void ClearCopy()
//        {
//            if (tempData.isAssigned)
//            {
//                tempData = new PointSwitchStepGameObjectData();

//                tempData.isAssigned = false;
//            }
//        }

//        private void Paste(PointSwitchStepGameObjectData operate)
//        {
//            operate.Overlay = tempData.Overlay;
//            operate.OccIuder = tempData.OccIuder;
//            operate.ForceRender = tempData.ForceRender;
//            operate.TweenGradient = tempData.TweenGradient;
//            operate.ConstantColor = tempData.ConstantColor;
//            operate.TweenLoopMode = tempData.TweenLoopMode;
//            operate.TweenDuration = tempData.TweenDuration;
//            operate.TweenEasing = tempData.TweenEasing;
//            operate.screenSpace_Z = tempData.screenSpace_Z;
//            operate.Target = tempData.Target;
//            operate.Delay = tempData.Delay;
//        }

//        private void CopyHighLighter(PointSwitchStepGameObjectData operate, int index) {
//            hightLighter = operate.HighlighterTrans[index];

//            tempData.isCopied = true;
//        }

//        private void ClearHighLighter() {
//            if (tempData.isCopied) {
//                hightLighter = null;

//                tempData.isCopied = false; ;
//            }
//        }

//        private void SaveData()
//        {
//            if (filename != string.Empty && filepath != string.Empty)
//            {
//                FileKit.SavePath(Path.Combine(filepath, string.Concat(filename, ".PSStepObjData")), true, objects.WirteJson());
//                EditorUtility.DisplayDialog("Save", "保存成功", "确定");
//            }
//            else EditorUtility.DisplayDialog("Error", $"保存失败 Path:{Path.Combine(filepath, string.Concat(filename, ".PSStepObjData"))}", "确定");
//        }

//        private void LoadData()
//        {
//            var path = Path.Combine(filepath, string.Concat(filename, ".PSStepObjData"));
//            if (FileKit.Exists(path))
//            {
//                objects.ReadJson(FileKit.LoadPath(path, true));
//                EditorUtility.DisplayDialog("Load", "读取成功", "确定");
//            }
//            else EditorUtility.DisplayDialog("Error", $"读取失败 Path:{path}", "确定");
//        }

//        private void AddOperate()
//        {
//            objects.operates.Add(new PointSwitchStepGameObjectData());
//        }

//        protected override void change()
//        {
//            base.change();
//            EditorPrefs.SetString("PointSwicthStepObjDataFileName", filename);
//        }

//        protected override void close()
//        {
//            window = null;
//        }
//    }
//}
