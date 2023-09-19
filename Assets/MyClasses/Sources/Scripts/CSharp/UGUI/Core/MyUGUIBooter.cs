/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIBooter (version 2.12)
 */

#pragma warning disable 0414
#pragma warning disable 0649

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace MyClasses.UI
{
    public class MyUGUIBooter : MyUGUIBooterBase
    {
        #region ----- Variable -----

        [SerializeField]
        private EUnitySceneID _defaultUnitySceneID;
        [SerializeField]
        private ESceneID _defaultSceneID;

        #endregion

        #region ----- MonoBehaviour Implementation -----

        /// <summary>
        /// Awake.
        /// </summary>
        void Awake()
        {
#if UNITY_2022_2_OR_NEWER
            MyCompileFlag.UNITY_2022_2_OR_NEWER = true;
#else
            MyCompileFlag.UNITY_2022_2_OR_NEWER = false;
#endif

#if UNITY_ANDROID
            MyCompileFlag.UNITY_ANDROID = true;
#else
            MyCompileFlag.UNITY_ANDROID = false;
#endif

#if UNITY_IOS
            MyCompileFlag.UNITY_IOS = true;
#else
            MyCompileFlag.UNITY_IOS = false;
#endif

#if UNITY_WEBGL
            MyCompileFlag.UNITY_WEBGL = true;
#else
            MyCompileFlag.UNITY_WEBGL = false;
#endif

#if DISABLE_MY_LOGGER_ALL
            MyCompileFlag.DISABLE_MY_LOGGER_ALL = true;
#else
            MyCompileFlag.DISABLE_MY_LOGGER_ALL = false;
#endif

#if DISABLE_MY_LOGGER_INFO
            MyCompileFlag.DISABLE_MY_LOGGER_INFO = true;
#else
            MyCompileFlag.DISABLE_MY_LOGGER_INFO = false;
#endif

#if DISABLE_MY_LOGGER_WARNING
            MyCompileFlag.DISABLE_MY_LOGGER_WARNING = true;
#else
            MyCompileFlag.DISABLE_MY_LOGGER_WARNING = false;
#endif

#if DISABLE_MY_LOGGER_ERROR
            MyCompileFlag.DISABLE_MY_LOGGER_ERROR = true;
#else
            MyCompileFlag.DISABLE_MY_LOGGER_ERROR = false;
#endif

#if DEBUG_MY_SOUND
            MyCompileFlag.DEBUG_MY_SOUND = true;
#else
            MyCompileFlag.DEBUG_MY_SOUND = false;
#endif

#if DEBUG_MY_UI
            MyCompileFlag.DEBUG_MY_UI = true;
#else
            MyCompileFlag.DEBUG_MY_UI = false;
#endif
        }

        #endregion

        #region ----- MyUGUIBooterBase Implementation -----

        // <summary>
        // Show scene.
        // </summary>
        protected override void _ShowScene()
        {
            MyUGUIManager.Instance.ShowUnityScene(_defaultUnitySceneID, _defaultSceneID);
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyUGUIBooter))]
    public class MyUGUIConfigEditor : Editor
    {
        private MyUGUIBooter _script;
        private SerializedProperty _showMode;
        private SerializedProperty _bootMode;
        private SerializedProperty _defaultUnitySceneID;
        private SerializedProperty _defaultSceneID;
        private SerializedProperty _delayTimeOnEditor;
        private SerializedProperty _delayTimeOnDevice;
        private SerializedProperty _onPreShowSync;
        private SerializedProperty _onPreShow;
        private SerializedProperty _onCustomShow;
        private SerializedProperty _onPostShow;

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUIBooter)target;
            _showMode = serializedObject.FindProperty("_showMode");
            _bootMode = serializedObject.FindProperty("_bootMode");
            _defaultUnitySceneID = serializedObject.FindProperty("_defaultUnitySceneID");
            _defaultSceneID = serializedObject.FindProperty("_defaultSceneID");
            _delayTimeOnEditor = serializedObject.FindProperty("_delayTimeOnEditor");
            _delayTimeOnDevice = serializedObject.FindProperty("_delayTimeOnDevice");
            _onPreShowSync = serializedObject.FindProperty("_onPreShowSync");
            _onPreShow = serializedObject.FindProperty("_onPreShow");
            _onCustomShow = serializedObject.FindProperty("_onCustomShow");
            _onPostShow = serializedObject.FindProperty("_onPostShow");
        }

        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(_script), typeof(MyUGUIBooter), false);

            serializedObject.Update();

            _bootMode.enumValueIndex = (int)(MyUGUIBooter.EBootMode)EditorGUILayout.EnumPopup("Boot Mode", (MyUGUIBooter.EBootMode)_bootMode.enumValueIndex);
            _showMode.enumValueIndex = (int)(MyUGUIBooter.EShowMode)EditorGUILayout.EnumPopup("Show Mode", (MyUGUIBooter.EShowMode)_showMode.enumValueIndex);
            if ((MyUGUIBooter.EShowMode)_showMode.enumValueIndex == MyUGUIBooter.EShowMode.Default)
            {
                _defaultUnitySceneID.enumValueIndex = (int)(EUnitySceneID)EditorGUILayout.EnumPopup("Default Unity Scene", (EUnitySceneID)_defaultUnitySceneID.enumValueIndex);
                _defaultSceneID.enumValueIndex = (int)(ESceneID)EditorGUILayout.EnumPopup("Default Scene", (ESceneID)_defaultSceneID.enumValueIndex);
            }
            switch ((MyUGUIBooter.EBootMode)_bootMode.enumValueIndex)
            {
                case MyUGUIBooter.EBootMode.Instant:
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_onPreShow, new GUIContent("On Pre Show"));
                        if (EditorGUI.EndChangeCheck())
                        {
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                    break;

                case MyUGUIBooter.EBootMode.FixedTimeDelay:
                    {
                        _delayTimeOnEditor.floatValue = EditorGUILayout.FloatField("Delay Second (On Editor)", _delayTimeOnEditor.floatValue);
                        _delayTimeOnDevice.floatValue = EditorGUILayout.FloatField("Delay Second (On Device)", _delayTimeOnDevice.floatValue);
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_onPreShow, new GUIContent("On Pre Show"));
                        if (EditorGUI.EndChangeCheck())
                        {
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                    break;

                case MyUGUIBooter.EBootMode.WaitForInitializing:
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_onPreShowSync, new GUIContent("On Pre Show"));
                        if (EditorGUI.EndChangeCheck())
                        {
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                    break;
            }

            EditorGUI.BeginChangeCheck();
            if ((MyUGUIBooter.EShowMode)_showMode.enumValueIndex == MyUGUIBooter.EShowMode.Custom)
            {
                EditorGUILayout.PropertyField(_onCustomShow, new GUIContent("On Custom Show", "You should call MyUGUIManager.Instance.ShowUnityScene() here"));
            }
            EditorGUILayout.PropertyField(_onPostShow, new GUIContent("On Post Show"));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}