/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIPieChart (version 2.9)
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
    public class MyUGUIPieChart : Graphic
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
        private float _fill = 100;
        [HideInInspector]
        [SerializeField]
        private int _density = 100;
        [HideInInspector]
        [SerializeField]
        private bool _isTransparency = false;
        [HideInInspector]
        [SerializeField]
        private List<Piece> _pieces = new List<Piece>() { new Piece(0.3f, Color.red), new Piece(0.4f, Color.green), new Piece(0.3f, Color.blue) };

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

        public float Fill
        {
            get { return _fill; }
            set
            {
                _fill = Mathf.Clamp(value, 0, 100);
                SetAllDirty();
            }
        }

        public int Density
        {
            get { return _density; }
            set { _density = Mathf.Clamp(value, 1, 200); }
        }

        public bool IsTransparency
        {
            get { return _isTransparency; }
            set { _isTransparency = value; }
        }

        public List<Piece> Pieces
        {
            get { return _pieces; }
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

            int countPiece = _pieces.Count;
            int curPieceIndex = 0;
            int segment = (int)(3.6f * _density) + 1;
            float outer = -_radius / 2;
            float lastPieceDegrees = 0;
            Vector2 prevPos = Vector2.zero;
            for (int i = 0; i <= segment; i++)
            {
                Piece curPiece = _pieces[curPieceIndex];
                float curPieceDegrees = lastPieceDegrees + (curPiece.Value * 360f);
                float maxDegrees = 3.6f * _fill;
                float degrees = Mathf.Clamp(i * 100f / _density, 0, maxDegrees);
                if (degrees <= curPieceDegrees || curPieceIndex == countPiece - 1)
                {
                    vh.AddUIVertexQuad(_GetVBOs(degrees, outer, curPiece.Color, ref prevPos));
                }
                else
                {
                    vh.AddUIVertexQuad(_GetVBOs(curPieceDegrees, outer, curPiece.Color, ref prevPos));
                    curPieceIndex++;
                    lastPieceDegrees = curPieceDegrees;
                    vh.AddUIVertexQuad(_GetVBOs(degrees, outer, _pieces[curPieceIndex].Color, ref prevPos));
                }
                if (degrees >= maxDegrees)
                {
                    break;
                }
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
            GameObject gameObject = new GameObject("PieChart");
            if (Selection.activeTransform != null)
            {
                gameObject.transform.parent = Selection.activeTransform;
            }

            gameObject.AddComponent<MyUGUIPieChart>();

            EditorGUIUtility.PingObject(gameObject);
            Selection.activeGameObject = gameObject;
        }

#endif

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Return vertex buffer objects by degrees.
        /// </summary>
        private UIVertex[] _GetVBOs(float degrees, float outer, Color color, ref Vector2 prevPos)
        {
            float rad = Mathf.Deg2Rad * (270 - degrees - _rotation);
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);

            Vector2 pos0 = prevPos;
            Vector2 pos1 = new Vector2(outer * cos, outer * sin);

            prevPos = pos1;

            UIVertex[] VBOs = new UIVertex[4];
            Vector2[] vertices = _isTransparency ? new Vector2[] { pos0, pos1 } : new Vector2[] { pos0, pos1, Vector2.zero };
            for (int i = 0; i < vertices.Length; i++)
            {
                UIVertex vert = UIVertex.simpleVert;
                vert.color = color;
                vert.position = vertices[i];
                VBOs[i] = vert;
            }
            return VBOs;
        }

        #endregion

        #region ----- Internal Struct -----

        [System.Serializable]
        public class Piece
        {
            public float Value;
            public Color Color;

            public Piece(float value, Color color)
            {
                Value = value;
                Color = color;
            }
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyUGUIPieChart))]
    public class MyUGUIPieChartEditor : Editor
    {
        private MyUGUIPieChart _script;
        private bool _isVisible = true;

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUIPieChart)target;
        }

        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(_script), typeof(MyUGUIPieChart), false);

            _script.raycastTarget = EditorGUILayout.Toggle("Raycast Target", _script.raycastTarget);

            SerializedProperty transparencyProperty = serializedObject.FindProperty("_isTransparency");
            transparencyProperty.boolValue = EditorGUILayout.Toggle("Is Transparency", transparencyProperty.boolValue);

            SerializedProperty radiusProperty = serializedObject.FindProperty("_radius");
            radiusProperty.floatValue = EditorGUILayout.FloatField("Radius", radiusProperty.floatValue);

            SerializedProperty rotationProperty = serializedObject.FindProperty("_rotation");
            rotationProperty.floatValue = EditorGUILayout.Slider("Rotation", rotationProperty.floatValue, 0, 360);

            SerializedProperty fillProperty = serializedObject.FindProperty("_fill");
            fillProperty.floatValue = EditorGUILayout.Slider("Fill", fillProperty.floatValue, 0, 100);

            SerializedProperty densityProperty = serializedObject.FindProperty("_density");
            float densityValue = (float)densityProperty.intValue;
            densityProperty.intValue = (int)EditorGUILayout.Slider("Density", densityValue, 1, 200);

            _isVisible = EditorGUILayout.Foldout(_isVisible, "Pieces", true);
            if (_isVisible)
            {
                SerializedProperty piecesProperty = serializedObject.FindProperty("_pieces");
                EditorGUI.indentLevel++;
                piecesProperty.arraySize = EditorGUILayout.IntField("Size", piecesProperty.arraySize);
                for (int i = 0; i < piecesProperty.arraySize; i++)
                {
                    EditorGUILayout.LabelField("Element " + i);
                    EditorGUI.indentLevel++;
                    SerializedProperty elementProperty = piecesProperty.GetArrayElementAtIndex(i);
                    SerializedProperty elementPropertyValue = elementProperty.FindPropertyRelative("Value");
                    SerializedProperty elementPropertyColor = elementProperty.FindPropertyRelative("Color");
                    elementPropertyValue.floatValue = EditorGUILayout.FloatField("Value", elementPropertyValue.floatValue);
                    elementPropertyColor.colorValue = EditorGUILayout.ColorField("Color", elementPropertyColor.colorValue);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
