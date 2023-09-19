/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIConfigSceneEditorWindow (version 2.11)
 */

using UnityEditor;
using System;

namespace MyClasses.UI.Tool
{
    public class MyUGUIConfigSceneEditorWindow : MyUGUIConfigSceneEditorWindowBase
    {
        #region ----- MyUGUIConfigSceneEditorWindowBase Implementation -----

        /// <summary>
        /// Return the number of unity scene.
        /// </summary>
        protected override int _GetUnitySceneQuantity()
        {
            return Enum.GetValues(typeof(EUnitySceneID)).Length;
        }

        /// <summary>
        /// Return the name of unity scene.
        /// </summary>
        protected override string _GetUnitySceneName(int id)
        {
            return ((EUnitySceneID)id).ToString();
        }

        /// <summary>
        /// Return the name of scene.
        /// </summary>
        protected override string _GetSceneName(int id)
        {
            return ((ESceneID)id).ToString();
        }

        /// <summary>
        /// Create scene enum popup.
        /// </summary>
        protected override int _CreateSceneEnumPopup(int sceneId)
        {
            return (int)(ESceneID)EditorGUILayout.EnumPopup("ID", (ESceneID)sceneId);
        }


        #endregion
    }
}