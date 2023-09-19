/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUILine (version 2.1)
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
    public class MyUGUILine : Graphic
    {
        #region ----- Variable -----

        [SerializeField]
        private Gradient _color = null;
        [SerializeField]
        private EColorType _colorType = EColorType.Horizontal;
        [SerializeField]
        private EJoinType _joinType = EJoinType.Bevel;
        [SerializeField]
        private float _thickness = 25;
        [SerializeField]
        private int _numExtraPointForSegment = 0;
        [SerializeField]
        private List<Vector2> _listPoint = new List<Vector2>() { new Vector2(-100, -100), new Vector2(-100, 100), new Vector2(0, 0), new Vector2(100, 100), new Vector2(100, -100) };

        [SerializeField]
        private List<Vector2> _listFullPoint = new List<Vector2>() { new Vector2(-100, -100), new Vector2(-100, 100), new Vector2(0, 0), new Vector2(100, 100), new Vector2(100, -100) };
        [SerializeField]
        private List<float> _listFullLength = new List<float>();
        [SerializeField]
        private float _totalLength = 0;

        #endregion

        #region ----- Property -----

        public List<Vector2> Points
        {
            get { return _listPoint; }
            set
            {
                _listPoint = value;
                Refresh();
            }
        }

        public float Thickness
        {
            get { return _thickness; }
            set { _thickness = Mathf.Max(value, 0.1f); }
        }

        public int Density
        {
            get { return _numExtraPointForSegment; }
            set
            {
                _numExtraPointForSegment = Mathf.Clamp(value, 0, 5);
                Refresh();
            }
        }

        #endregion

        #region ----- MonoBehaviour Implementation -----

        /// <summary>
        /// Awake.
        /// </summary>
        private void Awake()
        {
            if (_color == null)
            {
                GradientColorKey[] colorKeys = new GradientColorKey[6];
                colorKeys[0] = new GradientColorKey(new Color(0, 0.3f, 1f), 0);
                colorKeys[1] = new GradientColorKey(new Color(0, 1f, 1f), 0.2f);
                colorKeys[2] = new GradientColorKey(new Color(0, 1f, 0.2f), 0.4f);
                colorKeys[3] = new GradientColorKey(new Color(1f, 1f, 0), 0.6f);
                colorKeys[4] = new GradientColorKey(new Color(1f, 0, 0), 0.8f);
                colorKeys[5] = new GradientColorKey(new Color(1f, 0.2f, 0.6f), 1f);

                GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
                alphaKeys[0] = new GradientAlphaKey(1f, 1f);
                alphaKeys[1] = new GradientAlphaKey(0, 1f);

                _color = new Gradient();
                _color.SetKeys(colorKeys, alphaKeys);
            }
        }

        /// <summary>
        /// Start.
        /// </summary>
        private void Start()
        {
            Refresh();
        }

        #endregion

        #region ----- Graphic Implementation -----

        /// <summary>
        /// OnPopulateMesh.
        /// </summary>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Vector2 prevBot = Vector2.zero;
            Vector2 prevTop = Vector2.zero;
            for (int i = 1, length = _listFullPoint.Count - 1; i <= length; ++i)
            {
                Vector2 prevPoint = _listFullPoint[i - 1] * transform.localScale;
                Vector2 curPoint = _listFullPoint[i] * transform.localScale;
                Vector2 offset = new Vector2((curPoint.y - prevPoint.y), prevPoint.x - curPoint.x).normalized * _thickness / 2;

                if (i == 1)
                {
                    prevTop = prevPoint - offset;
                    prevBot = prevPoint + offset;

                    vh.AddVert(prevBot, _GetColor(0, true), Vector2.zero);
                    vh.AddVert(prevTop, _GetColor(0, false), Vector2.zero);
                }
                Vector2 curTop = curPoint - offset;
                Vector2 curBot = curPoint + offset;

                switch (_joinType)
                {
                    case EJoinType.None:
                        {
                            // None
                            // [i = 1]       [i = 2]
                            // 1 x x 3       1 x x 3
                            // x x x x       x x 4 x 5
                            // 0 x x 2       0 x x 2 x
                            //                   x x x
                            //                   6 x 7

                            if (2 <= i)
                            {
                                prevTop = prevPoint - offset;
                                prevBot = prevPoint + offset;

                                vh.AddVert(prevBot, _GetColor(i - 1, true), Vector2.zero);
                                vh.AddVert(prevTop, _GetColor(i - 1, false), Vector2.zero);
                            }

                            vh.AddVert(curBot, _GetColor(i, true), Vector2.zero);
                            vh.AddVert(curTop, _GetColor(i, false), Vector2.zero);

                            int lastBotIndex = vh.currentVertCount - 4;
                            int lastTopIndex = lastBotIndex + 1;
                            int curBotIndex = lastBotIndex + 2;
                            int curTopIndex = lastBotIndex + 3;

                            vh.AddTriangle(lastBotIndex, lastTopIndex, curTopIndex);
                            vh.AddTriangle(curTopIndex, curBotIndex, lastBotIndex);
                        }
                        break;

                    case EJoinType.Miter:
                        {
                            // Miter
                            // [i = 1]       [i = 2]
                            // 1 x x x 3     1 x x x 3
                            // x x x x       x x x x x
                            // 0 x 2         0 x 2 x x
                            //                   x x x
                            //                   4 x 5

                            if (i < length)
                            {
                                Vector2 nextPoint = _listFullPoint[i + 1] * transform.localScale;
                                Vector2 vectorCurPrev = prevPoint - curPoint;
                                Vector2 vectorCurNext = nextPoint - curPoint;
                                float angle = Vector2.Angle(vectorCurPrev, vectorCurNext) * Mathf.Deg2Rad;
                                float sign = Mathf.Sign(Vector3.Cross(vectorCurPrev.normalized, vectorCurNext.normalized).z);
                                float miterDistance = _thickness / (2 * Mathf.Tan(angle / 2));
                                Vector2 miterVector = vectorCurPrev.normalized * miterDistance * sign;

                                curTop -= miterVector;
                                curBot += miterVector;
                            }

                            vh.AddVert(curBot, _GetColor(i, true), Vector2.zero);
                            vh.AddVert(curTop, _GetColor(i, false), Vector2.zero);

                            int lastBotIndex = vh.currentVertCount - 4;
                            int lastTopIndex = lastBotIndex + 1;
                            int curBotIndex = lastBotIndex + 2;
                            int curTopIndex = lastBotIndex + 3;

                            vh.AddTriangle(lastBotIndex, lastTopIndex, curTopIndex);
                            vh.AddTriangle(curTopIndex, curBotIndex, lastBotIndex);
                        }
                        break;

                    case EJoinType.Bevel:
                        {
                            // Bevel
                            // [i = 1]     [i = 2]
                            // 1 x x 2     1 x x 2
                            // x x x x     x x x x 4
                            // 0 x 3 x     0 x 3 x x
                            //                 x x x
                            //                 6 x 5

                            if (2 <= i)
                            {
                                prevTop = prevPoint - offset;

                                vh.AddVert(prevTop, _GetColor(i - 1, false), Vector2.zero);

                                int newestIndex = vh.currentVertCount - 1;

                                vh.AddTriangle(newestIndex, newestIndex - 1, newestIndex - 2);
                            }

                            if (i < length)
                            {
                                Vector2 nextPoint = _listFullPoint[i + 1] * transform.localScale;
                                Vector2 vectorCurPrev = prevPoint - curPoint;
                                Vector2 vectorCurNext = nextPoint - curPoint;
                                float angle = Vector2.Angle(vectorCurPrev, vectorCurNext) * Mathf.Deg2Rad;
                                float sign = Mathf.Sign(Vector3.Cross(vectorCurPrev.normalized, vectorCurNext.normalized).z);
                                float miterDistance = _thickness / (2 * Mathf.Tan(angle / 2));
                                Vector2 miterVector = vectorCurPrev.normalized * miterDistance * sign;

                                curBot += miterVector;
                            }

                            vh.AddVert(curTop, _GetColor(i, true), Vector2.zero);
                            vh.AddVert(curBot, _GetColor(i, false), Vector2.zero);

                            int lastBotIndex = vh.currentVertCount - 4;
                            int lastTopIndex = lastBotIndex + 1;
                            int curTopIndex = lastBotIndex + 2;
                            int curBotIndex = lastBotIndex + 3;

                            vh.AddTriangle(lastBotIndex, lastTopIndex, curTopIndex);
                            vh.AddTriangle(curTopIndex, curBotIndex, lastBotIndex);
                        }
                        break;
                }

                prevTop = curTop;
                prevBot = curBot;
            }
        }

        #endregion

        #region ----- Public Method -----

        /// <summary>
        /// Refresh.
        /// </summary>
        public void Refresh()
        {
            _FindAllPoints();
            _CalculateLength();
            SetAllDirty();
        }

#if UNITY_EDITOR

        /// <summary>
        /// Create a template.
        /// </summary>
        public static void CreateTemplate()
        {
            GameObject gameObject = new GameObject("Line");
            if (Selection.activeTransform != null)
            {
                gameObject.transform.parent = Selection.activeTransform;
            }

            gameObject.AddComponent<MyUGUILine>();

            EditorGUIUtility.PingObject(gameObject);
            Selection.activeGameObject = gameObject.gameObject;
        }

#endif

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Find all points based on density.
        /// </summary>
        private void _FindAllPoints()
        {
            _listFullPoint.Clear();
            if (_numExtraPointForSegment > 0)
            {
                for (int i = 0, count = _listPoint.Count; i < count; ++i)
                {
                    Vector2 curPoint = _listPoint[i];
                    if (1 < i)
                    {
                        Vector2 prevPoint = _listPoint[i - 1];
                        Vector2 offset = (curPoint - prevPoint) / (_numExtraPointForSegment + 1);
                        for (int j = 1; j < _numExtraPointForSegment; ++j)
                        {
                            _listFullPoint.Add(prevPoint + (offset * j));
                        }
                    }
                    _listFullPoint.Add(_listPoint[i]);
                }
            }
            else
            {
                _listFullPoint.AddRange(_listPoint);
            }
        }

        /// <summary>
        /// Calculate length.
        /// </summary>
        private void _CalculateLength()
        {
            _totalLength = 0;
            _listFullLength.Clear();
            _listFullLength.Add(0);
            for (int i = 1, length = _listFullPoint.Count - 1; i <= length; ++i)
            {
                _totalLength += Vector3.Distance(_listFullPoint[i], _listFullPoint[i - 1]);
                _listFullLength.Add(_totalLength);
            }
        }

        /// <summary>
        /// Return a color by a point.
        /// </summary>
        private Color _GetColor(int pointIndex, bool isBot = true)
        {
            switch (_colorType)
            {
                case EColorType.Horizontal:
                    {
                        return _color.Evaluate(_listFullLength[pointIndex] / _totalLength);
                    }

                case EColorType.ReverseHorizontal:
                    {
                        return _color.Evaluate(1 - (_listFullLength[pointIndex] / _totalLength));
                    }

                case EColorType.Vertical:
                    {
                        return _color.Evaluate(isBot ? 0 : 1);
                    }

                case EColorType.ReverseVertical:
                    {
                        return _color.Evaluate(isBot ? 1 : 0);
                    }
            }

            return Color.white;
        }

        #endregion

        #region ----- Enumeration -----

        public enum EColorType
        {
            Horizontal = 0,
            Vertical = 1,
            ReverseHorizontal = 2,
            ReverseVertical = 3,
        }

        public enum EJoinType
        {
            None = 0,
            Bevel = 1,
            Miter = 2,
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyUGUILine))]
    public class MyUGUILineEditor : Editor
    {
        private MyUGUILine _script;

        private SerializedProperty _color;
        private SerializedProperty _colorType;
        private SerializedProperty _joinType;
        private SerializedProperty _thickness;
        private SerializedProperty _numExtraPointForSegment;
        private SerializedProperty _listPoint;

        private bool _isListPointVisible = true;

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUILine)target;

            _color = serializedObject.FindProperty("_color");
            _colorType = serializedObject.FindProperty("_colorType");
            _joinType = serializedObject.FindProperty("_joinType");
            _thickness = serializedObject.FindProperty("_thickness");
            _numExtraPointForSegment = serializedObject.FindProperty("_numExtraPointForSegment");
            _listPoint = serializedObject.FindProperty("_listPoint");
        }

        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(_script), typeof(MyUGUILine), false);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_color, new GUIContent("Color Over Length"), true);
            _colorType.enumValueIndex = (int)(MyUGUILine.EColorType)EditorGUILayout.EnumPopup("Direction", (MyUGUILine.EColorType)_colorType.enumValueIndex);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Line", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            _joinType.enumValueIndex = (int)(MyUGUILine.EJoinType)EditorGUILayout.EnumPopup("Join", (MyUGUILine.EJoinType)_joinType.enumValueIndex);
            _thickness.floatValue = EditorGUILayout.Slider("Thickness", _thickness.floatValue, 0.1f, 512);
            _numExtraPointForSegment.intValue = (int)EditorGUILayout.Slider("Extra Points For Each Segment", _numExtraPointForSegment.intValue, 0, 5);

            _isListPointVisible = EditorGUILayout.Foldout(_isListPointVisible, "Points", true);
            if (_isListPointVisible)
            {
                EditorGUI.indentLevel++;
                _listPoint.arraySize = EditorGUILayout.IntField("Size", _listPoint.arraySize);
                for (int i = 0; i < _listPoint.arraySize; i++)
                {
                    Rect elementPosition = GUILayoutUtility.GetRect(0f, 16f);
                    SerializedProperty elementProperty = _listPoint.GetArrayElementAtIndex(i);
                    EditorGUI.PropertyField(elementPosition, elementProperty);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                _script.Refresh();
            }
        }
    }

#endif
}
