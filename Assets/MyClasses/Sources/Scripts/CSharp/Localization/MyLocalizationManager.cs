/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyLocalizationManager (version 3.10)
 */

#pragma warning disable 0108

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace MyClasses
{
    public class MyLocalizationManager : MyLocalizationManagerBase
    {
        #region ----- Define -----

#if MY_LOCALIZATION_ARABIC
        public readonly string[] ARABIC_SYMBOLS = new string[] { ".", "؟", "(" };
#endif

        #endregion

        #region ----- Singleton -----

        private static object _singletonLock = new object();
        private static MyLocalizationManager _instance;

        public static MyLocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
#if MY_LOCALIZATION_ARABIC
                    MyCompileFlag.MY_LOCALIZATION_ARABIC = true;
#else
                    MyCompileFlag.MY_LOCALIZATION_ARABIC = false;
#endif

#if MY_LOCALIZATION_KHMER
                    MyCompileFlag.MY_LOCALIZATION_KHMER = true;
#else
                    MyCompileFlag.MY_LOCALIZATION_KHMER = false;
#endif

                    lock (_singletonLock)
                    {
                        _instance = (MyLocalizationManager)FindObjectOfType(typeof(MyLocalizationManager));
                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(MyLocalizationManager).Name);
                            _instance = obj.AddComponent<MyLocalizationManager>();
                            if (Application.isPlaying)
                            {
                                DontDestroyOnLoad(obj);
                            }
                        }
                        else if (Application.isPlaying)
                        {
                            DontDestroyOnLoad(_instance);
                        }
                        _instance.LoadConfig();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region ----- Public Method -----

#if UNITY_EDITOR

        /// <summary>
        /// Create a template.
        /// </summary>
        public static void CreateTemplate()
        {
            GameObject obj = new GameObject(typeof(MyLocalizationManager).Name);
            MyLocalizationManager script = obj.AddComponent<MyLocalizationManager>();
            script.LoadConfig();

            EditorGUIUtility.PingObject(obj);
            Selection.activeGameObject = obj.gameObject;
        }

#endif

        #endregion

        #region ----- MyLocalizationManagerBase Implementation -----

        /// <summary>
        /// Load Arabic text by key.
        /// </summary>
        protected override string _LoadArabicKey(string value)
        {
#if MY_LOCALIZATION_ARABIC
            // please import "Arabic Support" package
            string arabic = ArabicSupport.ArabicFixer.Fix(value, false, false);
            for (int i = 0; i < 10; ++i)
            {
                string format = "{" + i + "}";
                if (value.Contains(format))
                {
                    arabic = arabic.Replace("}{" + i, format);
                    for (int j = 0; j < ARABIC_SYMBOLS.Length; ++j)
                    {
                        arabic = arabic.Replace(format + ARABIC_SYMBOLS[j], ARABIC_SYMBOLS[j] + format);
                    }
                }
                else
                {
                    break;
                }
            }
            return arabic;
#else
            return string.Empty;
#endif
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyLocalizationManager))]
    public class MyLocalizationManagerEditor : MyLocalizationManagerBaseEditor
    {
    }

#endif
}