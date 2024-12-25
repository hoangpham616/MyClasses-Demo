/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIConfigIDEditorWindow (version 2.15)
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace MyClasses.UI.Tool
{
    public class MyUGUIConfigIDEditorWindow : EditorWindow
    {
        #region ----- Variable -----

        private MyUGUIConfigGroups _groups;
        private Vector2 _scrollPosition;

        #endregion

        #region ----- EditorWindow MonoBehaviour -----

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            titleContent = new GUIContent("[MyClasses] ID Configuration");
            minSize = new Vector2(512, 512);

            if (!Directory.Exists("Assets/Resources/" + MyUGUIManagerBase.CONFIG_DIRECTORY))
            {
                Directory.CreateDirectory("Assets/Resources/" + MyUGUIManagerBase.CONFIG_DIRECTORY);
            }

            if (!Directory.Exists("Assets/Resources/" + MyUGUIManagerBase.HUD_DIRECTORY))
            {
                Directory.CreateDirectory("Assets/Resources/" + MyUGUIManagerBase.HUD_DIRECTORY);
            }

            if (!Directory.Exists("Assets/Resources/" + MyUGUIManagerBase.SCENE_DIRECTORY))
            {
                Directory.CreateDirectory("Assets/Resources/" + MyUGUIManagerBase.SCENE_DIRECTORY);
            }

            if (!Directory.Exists("Assets/Resources/" + MyUGUIManagerBase.POPUP_DIRECTORY))
            {
                Directory.CreateDirectory("Assets/Resources/" + MyUGUIManagerBase.POPUP_DIRECTORY);
            }

            if (!Directory.Exists("Assets/Resources/" + MyUGUIManagerBase.TOAST_NOTIFICATION_DIRECTORY))
            {
                Directory.CreateDirectory("Assets/Resources/" + MyUGUIManagerBase.TOAST_NOTIFICATION_DIRECTORY);
            }

            _LoadAssetFile();
            _CorrectValues();
        }

        /// <summary>
        /// OnGUI.
        /// </summary>
        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Directory");
            _groups.Directory = EditorGUILayout.TextField(_groups.Directory, GUILayout.Width(400));
            EditorGUILayout.LabelField(string.Empty);
            EditorGUILayout.EndVertical();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, new GUILayoutOption[0]);
            for (int i = 0, countI = _groups.ListGroup.Count; i < countI; i++)
            {
                MyUGUIConfigGroup group = _groups.ListGroup[i];

                group.IsFoldOut = EditorGUILayout.Foldout(group.IsFoldOut, group.Name + " (" + group.ListIDName.Count + ")");
                if (group.IsFoldOut)
                {
                    int removeIndex = -1;

                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(true);
                    for (int j = 0; j < group.NumDefault; j++)
                    {
                        EditorGUILayout.TextField(group.ListIDName[j], GUILayout.Width(400));
                    }
                    EditorGUI.EndDisabledGroup();
                    for (int j = group.NumDefault, countJ = group.ListIDName.Count; j < countJ; j++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        group.ListIDName[j] = EditorGUILayout.TextField(group.ListIDName[j], GUILayout.Width(400));
                        if (GUILayout.Button("-", GUILayout.Width(30)))
                        {
                            if (group.ListIDName.Count > group.NumDefault)
                            {
                                removeIndex = j;
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(17);
                    if (GUILayout.Button("+", GUILayout.Width(387)))
                    {
                        group.ListIDName.Add(string.Empty);
                        group.ListIDNumber.Add(group.ListIDNumber[group.ListIDNumber.Count - 1] + 1);
                    }
                    GUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;

                    if (removeIndex >= 0)
                    {
                        group.ListIDName.RemoveAt(removeIndex);
                        group.ListIDNumber.RemoveAt(removeIndex);
                    }
                }

                if (i < countI - 1)
                {
                    EditorGUILayout.LabelField(string.Empty);
                }
            }
            EditorGUILayout.EndScrollView();

            EditorUtility.SetDirty(_groups);

            EditorGUILayout.LabelField(string.Empty);
            if (GUILayout.Button("Apply", GUILayout.Width(100)))
            {
                _GenerateScript();
            }
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Load the asset file.
        /// </summary>
        private void _LoadAssetFile()
        {
            if (_groups != null)
            {
                return;
            }

            string filePath = "Assets/Resources/" + MyUGUIManagerBase.CONFIG_DIRECTORY + typeof(MyUGUIConfigGroups).Name + ".asset";
            _groups = AssetDatabase.LoadAssetAtPath(filePath, typeof(MyUGUIConfigGroups)) as MyUGUIConfigGroups;
            if (_groups == null)
            {
                _groups = ScriptableObject.CreateInstance<MyUGUIConfigGroups>();
                _groups.ListGroup = new List<MyUGUIConfigGroup>();
                _groups.ListGroup.Add(new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "EUnitySceneID",
                    NumDefault = 0,
                    ListIDName = new List<string>() { "MainUnityScene" },
                    ListIDNumber = new List<int>() { 0 }
                });
                _groups.ListGroup.Add(new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "ESceneID",
                    NumDefault = 0,
                    ListIDName = new List<string>() { "LoadingScene", "MainScene", "GameScene" },
                    ListIDNumber = new List<int>() { 0, 1, 2 }
                });
                _groups.ListGroup.Add(new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "ESubSceneID",
                    NumDefault = 0,
                    ListIDName = new List<string>() { "None" },
                    ListIDNumber = new List<int>() { 0 }
                });
                _groups.ListGroup.Add(new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "EPopupID",
                    NumDefault = 3,
                    ListIDName = new List<string>() { "Dialog0ButtonPopup", "Dialog1ButtonPopup", "Dialog2ButtonsPopup" },
                    ListIDNumber = new List<int>() { 0, 1, 2 }
                });
                _groups.ListGroup.Add(new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "EToastNotificationID",
                    NumDefault = 0,
                    ListIDName = new List<string>(),
                    ListIDNumber = new List<int>()
                });
                AssetDatabase.CreateAsset(_groups, filePath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                if (_groups.ListGroup[2].Name == "ESubSceneID")
                {
                    if (_groups.ListGroup[2].ListIDName.Count == 0 || !_groups.ListGroup[2].ListIDName.Contains("None"))
                    {
                        _groups.ListGroup[2].ListIDName.Insert(0, "None");
                        if (!_groups.ListGroup[2].ListIDNumber.Contains(0))
                        {
                            _groups.ListGroup[2].ListIDNumber.Insert(0, 0);
                        }
                        else
                        {
                            _groups.ListGroup[2].ListIDNumber.Insert(0, _groups.ListGroup[2].ListIDNumber[_groups.ListGroup[2].ListIDNumber.Count - 1] + 1);
                        }
                    }
                }
                if (_groups.ListGroup.Count == 3)
                {
                    _groups.ListGroup.Add(new MyUGUIConfigGroup()
                    {
                        IsFoldOut = true,
                        Name = "EToastNotificationID",
                        NumDefault = 0,
                        ListID = new List<string>()
                    });
                }
            }
        }

        /// <summary>
        /// Correct values.
        /// </summary>
        private void _CorrectValues()
        {
            for (int i = 0, countI = _groups.ListGroup.Count; i < countI; ++i)
            {
                MyUGUIConfigGroup groupPopup = _groups.ListGroup[i];
                if (groupPopup.ListID != null && groupPopup.ListID.Count > 0)
                {
                    groupPopup.ListIDName = new List<string>();
                    groupPopup.ListIDNumber = new List<int>();
                    for (int j = 0, countJ = groupPopup.ListID.Count; j < countJ; ++j)
                    {
                        groupPopup.ListIDName.Add(groupPopup.ListID[j]);
                        groupPopup.ListIDNumber.Add(j);
                    }

                    groupPopup.ListID.Clear();
                }
            }
        }

        /// <summary>
        /// Generate the script.
        /// </summary>
        private void _GenerateScript()
        {
            if (_groups == null)
            {
                return;
            }

            if (_groups.ListGroup.Count == 4)
            {
                _groups.ListGroup.Insert(2, new MyUGUIConfigGroup()
                {
                    IsFoldOut = true,
                    Name = "ESubSceneID",
                    NumDefault = 0,
                    ListID = new List<string>()
                });
            }

            string scriptName = typeof(MyUGUIConfigIDEditorWindow).Name;
            string unityScenes = string.Empty;
            for (int i = 0, countI = _groups.ListGroup[0].ListIDName.Count; i < countI; i++)
            {
                _groups.ListGroup[0].ListIDNumber[i] = i;
                unityScenes += "\n\t\t" + _groups.ListGroup[0].ListIDName[i] + " = " + _groups.ListGroup[0].ListIDNumber[i] + ",";
            }
            string scenes = string.Empty;
            for (int i = 0, countI = _groups.ListGroup[1].ListIDName.Count; i < countI; i++)
            {
                if (_groups.ListGroup[1].ListIDNumber.Count <= i)
                {
                    _groups.ListGroup[1].ListIDNumber.Add(_groups.ListGroup[1].ListIDNumber.Count);
                }
                scenes += "\n\t\t" + _groups.ListGroup[1].ListIDName[i] + " = " + _groups.ListGroup[1].ListIDNumber[i] + ",";
            }
            string subScenes = string.Empty;
            for (int i = 0, countI = _groups.ListGroup[2].ListIDName.Count; i < countI; i++)
            {
                if (_groups.ListGroup[2].ListIDNumber.Count <= i)
                {
                    _groups.ListGroup[2].ListIDNumber.Add(_groups.ListGroup[2].ListIDNumber.Count);
                }
                subScenes += "\n\t\t" + _groups.ListGroup[2].ListIDName[i] + " = " + _groups.ListGroup[2].ListIDNumber[i] + ",";
            }
            string popups = string.Empty;
            for (int i = 0, countI = _groups.ListGroup[3].ListIDName.Count; i < countI; i++)
            {
                popups += "\n\t\t" + _groups.ListGroup[3].ListIDName[i] + " = " + _groups.ListGroup[3].ListIDNumber[i] + ",";
            }
            string toastNotifications = string.Empty;
            for (int i = 0, countI = _groups.ListGroup[4].ListIDName.Count; i < countI; i++)
            {
                toastNotifications += "\n\t\t" + _groups.ListGroup[4].ListIDName[i] + " = " + _groups.ListGroup[4].ListIDNumber[i] + ",";
            }
            string content = "/*\n * Copyright (c) 2016 Phạm Minh Hoàng\n * Email:\t\thoangpham61691@gmail.com\n * Framework:\tMyClasses\n * Description:\tThis script is generated by " + scriptName + "\n */\n\nnamespace MyClasses.UI\n{\n\tpublic enum EUnitySceneID\n\t{" + unityScenes + "\n\t}\n\n\tpublic enum ESceneID\n\t{" + scenes + "\n\t}\n\n\tpublic enum ESubSceneID\n\t{" + subScenes + "\n\t}\n\n\tpublic enum EPopupID\n\t{" + popups + "\n\t}\n\n\tpublic enum EToastNotificationID\n\t{" + toastNotifications + "\n\t}\n}";

            if (_groups.Directory.Length == 0)
            {
                _groups.Directory = "Assets/";
            }
            else if (_groups.Directory[_groups.Directory.Length - 1] != '/')
            {
                _groups.Directory += "/";
            }
            if (!Directory.Exists(_groups.Directory))
            {
                Directory.CreateDirectory(_groups.Directory);
            }
            if (Directory.Exists(_groups.Directory))
            {
                File.WriteAllText(_groups.Directory + "MyUGUIConfigID.cs", content);
                AssetDatabase.Refresh();
                Debug.Log("[MyClasses] MyUGUIConfigID was created.");
                return;
            }

            Debug.LogError("[MyClasses] Could not find MyClasses location.");
        }

        #endregion
    }
}