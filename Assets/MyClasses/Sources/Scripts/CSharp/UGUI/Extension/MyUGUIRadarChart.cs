/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIRadarChart (version 2.9)
 */

#pragma warning disable 0114
#pragma warning disable 0414
#pragma warning disable 0649

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MyClasses.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CanvasRenderer))]
    public class MyUGUIRadarChart : Graphic
    {
        #region ----- Variable -----

        [HideInInspector]
        [SerializeField]
        private float _radius = 100;
        [HideInInspector]
        [SerializeField]
        private float _rotation = 0;
        [HideInInspector]
        [SerializeField]
        private List<float> _verticles = new List<float>() { 1, 1, 1, 1, 1, 1 };

        #endregion

        #region ----- Property -----

        public float Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                SetAllDirty();
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = Mathf.Clamp(value, 0, 360);
                SetAllDirty();
            }
        }

        public List<float> Verticles
        {
            get { return _verticles; }
        }

        #endregion

        #region ----- MonoBehaviour Implementation -----

        /// <summary>
        /// Start.
        /// </summary>
        void Start()
        {
            _radius = rectTransform.rect.width < rectTransform.rect.height ? rectTransform.rect.width : rectTransform.rect.height;
        }

        #endregion

        #region ----- Graphic Implementation -----

        /// <summary>
        /// OnPopulateMesh.
        /// </summary>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            int countVerticle = _verticles.Count;
            float degrees = 360 / countVerticle;
            float outer = -_radius / 2;
            for (int i = 0; i < countVerticle; i++)
            {
                float rad = Mathf.Deg2Rad * (270 - (i * degrees) - _rotation);
                float cos = Mathf.Cos(rad);
                float sin = Mathf.Sin(rad);
                Vector3 pos = new Vector2(outer * cos, outer * sin) * _verticles[i];
                vh.AddVert(pos, color, Vector2.zero);
            }
            vh.AddVert(Vector3.zero, color, Vector2.zero);

            for (int i = 0; i < countVerticle; i++)
            {
                int prevVert = (i + countVerticle - 1) % countVerticle;
                int curVert = i;
                int nextVert = (i + 1) % countVerticle;
                vh.AddTriangle(countVerticle, curVert % countVerticle, prevVert % countVerticle);
                vh.AddTriangle(countVerticle, curVert % countVerticle, nextVert);
            }
        }

        #endregion

        #region ----- Public Method -----

        /// <summary>
        /// Refresh.
        /// </summary>
        public void Refresh()
        {
            SetAllDirty();
        }

#if UNITY_EDITOR

        /// <summary>
        /// Create a template.
        /// </summary>
        public static void CreateTemplate()
        {
            GameObject gameObject = new GameObject("RadarChart");
            if (Selection.activeTransform != null)
            {
                gameObject.transform.parent = Selection.activeTransform;
            }

            gameObject.AddComponent<MyUGUIRadarChart>();

            EditorGUIUtility.PingObject(gameObject);
            Selection.activeGameObject = gameObject;
        }

#endif

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyUGUIRadarChart))]
    public class MyUGUIRadarChartEditor : Editor
    {
        private MyUGUIRadarChart _script;
        private bool _isVisible = true;

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUIRadarChart)target;
        }

        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SerializedProperty radiusProperty = serializedObject.FindProperty("_radius");
            radiusProperty.floatValue = EditorGUILayout.FloatField("Radius", radiusProperty.floatValue);

            SerializedProperty rotationProperty = serializedObject.FindProperty("_rotation");
            rotationProperty.floatValue = EditorGUILayout.Slider("Rotation", rotationProperty.floatValue, 0, 360);

            _isVisible = EditorGUILayout.Foldout(_isVisible, "Verticles", true);
            if (_isVisible)
            {
                SerializedProperty verticlesProperty = serializedObject.FindProperty("_verticles");
                EditorGUI.indentLevel++;
                verticlesProperty.arraySize = EditorGUILayout.IntField("Size", verticlesProperty.arraySize);
                for (int i = 0; i < verticlesProperty.arraySize; i++)
                {
                    Rect elementPosition = GUILayoutUtility.GetRect(0f, 16f);
                    SerializedProperty elementProperty = verticlesProperty.GetArrayElementAtIndex(i);
                    EditorGUI.PropertyField(elementPosition, elementProperty);
                }
                EditorGUI.indentLevel--;
                for (int i = _script.Verticles.Count - 1; i >= 0; i--)
                {
                    if (_script.Verticles[i] < 0)
                    {
                        _script.Verticles[i] = 0;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
