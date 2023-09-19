/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyImageStyleManagerEditor (version 1.0)
 */

#if UNITY_EDITOR

using UnityEditor;

namespace MyClasses
{
    [CustomEditor(typeof(MyImageStyleManager))]
    public class MyImageStyleManagerEditor : MyImageStyleManagerEditorBase
    {
        #region ----- MyImageStyleManagerEditorBase Implementation -----

        /// <summary>
        /// Create label with foldout header style.
        /// </summary>
        protected override void _CreateFoldoutLabel(string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.foldoutHeader);
        }

        #endregion
    }
}

#endif