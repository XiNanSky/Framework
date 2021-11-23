//namespace Framework.Editor.ToolWindow
//{
//    using System.IO;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;
//    using UnityEditor;
//    using UnityEngine;
//    using Framework.Function.PointSwitch;
//    using Framework.Editor;
//    using System.Text.RegularExpressions;

//    public class PointSwitchStepDataEditor : BaseWindowEditor
//    {
//        private static new EditorWindow window;
//        [MenuItem("ETool/Windows/Point Switch Editor %#x")]
//        public static void Open()
//        {
//            window = window ?? GetWindow<PointSwitchStepDataEditor>(true, "Point Switch Editor", true);//创建窗口
//            window.wantsMouseMove = true;
//            window.Show(true);//展示         
//        }

//        public PointSwitchStepDataEditor()
//        {
//            minSize = new Vector2(900, 800);
//            objects = new PointSwitchStepOperates();
//            operatesFoldout = new List<bool>();
//        }

//        private string filename;
//        private string filepath;
//        private string itemname;

//        private struct Content
//        {
//            public bool isAssigned;
//            public HandleType handle;
//            public string title;
//            public string lable;
//            public string animatorName;
//            public string context;
//            public string enlargedPos;
//            public Vector3 cameraPos;
//        };
//        private Content tempData = new Content();//保存数据

//        private PointSwitchStepOperates objects = new PointSwitchStepOperates();
//        private List<bool> operatesFoldout;

//        protected override void onEnable()
//        {
//            filepath = Application.streamingAssetsPath;
//            filename = EditorPrefs.GetString("filename");
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
//            });

//            vector = ETool.BE.ScrollView(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    objects.porjectname = ETool.AC.FieldText("项目名称", objects.porjectname);
//                    objects.time = ETool.AC.FieldInt("时间", objects.time);
//                });
//                ETool.BE.Horizontal(() =>
//                {
//                    objects.model = ETool.AC.PopupEnum("模式", objects.model);
//                    objects.type = ETool.AC.PopupEnum("类型", objects.type);
//                });
//                for (int i = 0; i < objects.operates.Count; i++)
//                {
//                    objects.operates[i].step = i;
//                    OperateView(objects.operates[i]);
//                }
//            }, vector, GStyleTool.scrollView);
//        }

//        private void OperateView(PointSwitchStepOperate operate)
//        {
//            if (operate == null || operatesFoldout.Count < operate.step) return;
//            operatesFoldout[operate.step] = ETool.BE.FoldoutHeaderGroup(() =>
//            {
//                ETool.BE.Vertical(() =>
//                {
//                    ETool.BE.Horizontal(() =>
//                    {
//                        operate.handle = ETool.AC.PopupEnum("操作方式", operate.handle);
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("标题"); operate.title = ETool.AC.FieldText(operate.title);
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("标签"); operate.lable = ETool.AC.FieldText(operate.lable);
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("动作名"); operate.animatorname = ETool.AC.FieldText(operate.animatorname);
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("内容"); operate.context = ETool.AC.TextArea(operate.context, GUILayout.Height(30));
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("放大界面消息"); operate.enlargedPos = ETool.AC.TextArea(operate.enlargedPos, GUILayout.Height(30));
//                    });
//                    ETool.BE.Vertical(() =>
//                    {   //答题
//                        ETool.BE.Horizontal(() =>
//                        {
//                            ETool.AC.LabelPrefix("问题描述"); operate.questionmsg = ETool.AC.TextArea(operate.questionmsg, GUILayout.Height(30));
//                        });
//                        ETool.BE.Vertical(() =>
//                        {
//                            ETool.BE.Horizontal(() =>
//                            {
//                                ETool.AC.LabelPrefix("问题选项");
//                                operate.questionindex = ETool.AC.SliderInt("正确选项", operate.questionindex, 0, operate.chosee.Count - 1);
//                                if (ETool.AC.Button("Add", GUILayout.Width(60)))
//                                {
//                                    operate.chosee.Add(default);
//                                }
//                            });

//                            for (int i = 0; i < operate.chosee.Count; i++)
//                            {
//                                ETool.BE.Horizontal(() =>
//                                {
//                                    operate.chosee[i] = ETool.AC.FieldText($"NO : {i}", operate.chosee[i]);
//                                    if (ETool.AC.Button("Del", GUILayout.Width(60))) operate.chosee.RemoveAt(i);
//                                });
//                            }
//                        }, GStyleTool.box);
//                        ETool.BE.Horizontal(() =>
//                        {
//                            ETool.AC.LabelPrefix("错误提示"); operate.questionerrorhint = ETool.AC.TextArea(operate.questionerrorhint, GUILayout.Height(30));
//                        });
//                        ETool.BE.Horizontal(() =>
//                        {
//                            ETool.AC.LabelPrefix("正确提示"); operate.questioncorrecthint = ETool.AC.TextArea(operate.questioncorrecthint, GUILayout.Height(30));
//                        });
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        if (ETool.AC.Button("删除"))
//                        {
//                            objects.operates.RemoveAt(operate.step);
//                            operatesFoldout[operate.step] = false;
//                        }
//                        if (ETool.AC.Button("复制")) { Copy(operate); }
//                        if (tempData.isAssigned)
//                        {
//                            if (ETool.AC.Button("粘贴")) { Paste(operate); }
//                        }
//                    });
//                }, GStyleTool.UIContent);
//            }, operatesFoldout[operate.step], operate.step.ToString());
//        }

//        private void ShowAll()
//        {
//            for (int i = 0; i < operatesFoldout.Count; i++)
//            {
//                operatesFoldout[i] = true;
//            }
//        }

//        private void HideAll()
//        {
//            for (int i = 0; i < operatesFoldout.Count; i++)
//            {
//                operatesFoldout[i] = false;
//            }
//        }

//        private void FindItem(string id)
//        {
//            Regex reg = new Regex(@"(^[\-0-9][0-9]*?)$");//判断是否能类型转换
//            if (reg.IsMatch(id))
//            {
//                int idNum = int.Parse(id);
//                if (idNum < 0 || idNum >= operatesFoldout.Count || (idNum - (int)idNum) != 0)
//                {
//                    EditorUtility.DisplayDialog("Find", idNum + " is illegal, you can only enter a positive integer numbers less than " + operatesFoldout.Count, "确定");
//                }
//                else
//                {
//                    operatesFoldout[idNum] = true;
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
//                if (idNum < 0 || idNum >= operatesFoldout.Count || (idNum - (int)idNum) != 0)
//                {
//                    Debug.Log(idNum + " is illegal, you can only enter a positive numbers less than " + operatesFoldout.Count);
//                }
//                else
//                {
//                    if (operatesFoldout[idNum] == true)
//                    {
//                        operatesFoldout[idNum] = false;
//                    }
//                }
//            }
//            else
//            {
//                EditorUtility.DisplayDialog("Close", id + " is illegal, you can only enter an integer number.", "确定");
//            }
//        }

//        private void Copy(PointSwitchStepOperate operate)
//        {
//            tempData.isAssigned = true;
//            tempData.handle = operate.handle;
//            tempData.title = operate.title;
//            tempData.context = operate.context;
//            tempData.animatorName = operate.animatorname;
//            tempData.lable = operate.lable;
//            tempData.enlargedPos = operate.enlargedPos;
//        }

//        private void ClearCopy()
//        {
//            if (tempData.isAssigned)
//            {
//                tempData.handle = HandleType.Click;
//                tempData.title = string.Empty;
//                tempData.context = string.Empty;
//                tempData.animatorName = string.Empty;
//                tempData.lable = string.Empty;
//                tempData.enlargedPos = string.Empty;
//                tempData.cameraPos = Vector3.zero;
//                tempData.isAssigned = false;
//            }
//        }

//        private void Paste(PointSwitchStepOperate operate)
//        {
//            operate.handle = tempData.handle;
//            operate.title = tempData.title;
//            operate.context = tempData.context;
//            operate.animatorname = tempData.animatorName;
//            operate.lable = tempData.lable;
//            operate.enlargedPos = tempData.enlargedPos;
//        }

//        private void SaveData()
//        {
//            if (filename != string.Empty && filepath != string.Empty)
//            {
//                FileKit.SavePath(Path.Combine(filepath, string.Concat(filename, ".json")), true, objects.WirteJson());
//                EditorUtility.DisplayDialog("Save", "保存成功", "确定");
//            }
//            else EditorUtility.DisplayDialog("Error", $"保存失败 Path:{Path.Combine(filepath, string.Concat(filename, ".json"))}", "确定");
//        }

//        private void LoadData()
//        {
//            if (FileKit.Exists(Path.Combine(filepath, string.Concat(filename, ".json"))))
//            {
//                var buffers = FileKit.LoadPath(Path.Combine(filepath, string.Concat(filename, ".json")), true);
//                objects.operates.Clear();
//                operatesFoldout.Clear();
//                objects.ReadJson(buffers);
//                for (int i = 0; i < objects.operates.Count; i++)
//                {
//                    operatesFoldout.Add(false);
//                }
//                EditorUtility.DisplayDialog("Load", "读取成功", "确定");
//            }
//            else EditorUtility.DisplayDialog("Error", $"读取失败 Path:{Path.Combine(filepath, filename)}", "确定");
//        }

//        private void AddOperate()
//        {
//            objects.operates.Add(new PointSwitchStepOperate());
//            if (operatesFoldout.Count < objects.operates.Count)
//            {
//                operatesFoldout.Add(false);
//            }
//        }

//        protected override void change()
//        {
//            base.change();
//            EditorPrefs.SetString("filename", filename);
//            //EditorPrefs.SetString("filepath", filepath);
//        }

//        protected override void close()
//        {
//            window = null;
//        }
//    }
//}
