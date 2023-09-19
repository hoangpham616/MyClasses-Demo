/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUICircle (version 2.2)
 */

#pragma warning disable 0114
#pragma warning disable 0414
#pragma warning disable 0649

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UI;

namespace MyClasses.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CanvasRenderer))]
    public class MyUGUICircle : Graphic
    {
        #region ----- Variable -----

        [HideInInspector]
        [SerializeField]
        private bool _isFill = true;
        [HideInInspector]
        [SerializeField]
        private float _radius = 100;
        [HideInInspector]
        [SerializeField]
        private float _thickness = 3;
        [HideInInspector]
        [SerializeField]
        private float _rotation = 0;
        [HideInInspector]
        [SerializeField]
        private float _degrees = 360;
        [HideInInspector]
        [SerializeField]
        private int _density = 100;

        #endregion

        #region ----- Property -----

        public bool IsFill
        {
            get { return _isFill; }
            set { _isFill = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set
            { 
                _radius = value;
                SetAllDirty();
            }
        }

        public float Thickness
        {
            get { return _thickness; }
            set
            { 
                _thickness = value;
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

        public float Degrees
        {
            get { return _degrees; }
            set
            { 
                _degrees = Mathf.Clamp(value, 0, 360);
                SetAllDirty();
            }
        }

        public int Density
        {
            get { return _density; }
            set { _density = Mathf.Clamp(value, 1, 200); }
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

            float outer = -_radius;
            float inner = outer + _thickness;

            Vector2 prevPos1 = Vector2.zero;
            Vector2 prevPos2 = Vector2.zero;

            int segment = (int)(3.6f * _density) + 1;
            for (int i = 0; i <= segment; i++)
            {
                float degrees = Mathf.Clamp(i * 100f / _density, 0, _degrees);

                float rad = Mathf.Deg2Rad * (270 - degrees - _rotation);
                float cos = Mathf.Cos(rad);
                float sin = Mathf.Sin(rad);

                Vector2 pos0 = prevPos1;
                Vector2 pos1 = new Vector2(outer * cos, outer * sin);
                Vector2 pos2 = _isFill ? Vector2.zero : new Vector2(inner * cos, inner * sin);
                Vector2 pos3 = _isFill ? Vector2.zero : prevPos2;

                prevPos1 = pos1;
                prevPos2 = pos2;

                Vector2 uv0 = new Vector2(0, 1);
                Vector2 uv1 = new Vector2(1, 1);
                Vector2 uv2 = new Vector2(1, 0);
                Vector2 uv3 = new Vector2(0, 0);

                vh.AddUIVertexQuad(_GetVBOs(new Vector2[]{ pos0, pos1, pos2, pos3 }, new Vector2[] { uv0, uv1, uv2, uv3 }));

                if (degrees >= _degrees)
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
            GameObject gameObject = new GameObject("Circle");
            if (Selection.activeTransform != null)
            {
                gameObject.transform.parent = Selection.activeTransform;
            }

            gameObject.AddComponent<MyUGUICircle>();

            EditorGUIUtility.PingObject(gameObject);
            Selection.activeGameObject = gameObject;
        }

#endif

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Return vertex buffer objects by degrees.
        /// </summary>
        private UIVertex[] _GetVBOs(Vector2[] vertices, Vector2[] uvs)
        {
            UIVertex[] vbo = new UIVertex[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                var vert = UIVertex.simpleVert;
                vert.color = color;
                vert.position = vertices[i];
                vert.uv0 = uvs[i];
                vbo[i] = vert;
            }
            return vbo;
        }

        #endregion
    }

#if UNITY_EDITOR
    
    [CustomEditor(typeof(MyUGUICircle))]
    public class MyUGUICircleEditor : Editor
    {
        private MyUGUICircle _script;
    
        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUICircle)target;
        }
    
        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(_script), typeof(MyUGUICircle), false);

            _script.raycastTarget = EditorGUILayout.Toggle("Raycast Target", _script.raycastTarget);
        
            SerializedProperty fillProperty = serializedObject.FindProperty("_isFill");
            fillProperty.boolValue = EditorGUILayout.Toggle("Is Fill", fillProperty.boolValue);
        
            SerializedProperty radiusProperty = serializedObject.FindProperty("_radius");
            radiusProperty.floatValue = EditorGUILayout.FloatField("Radius", radiusProperty.floatValue);

            SerializedProperty thicknessProperty = serializedObject.FindProperty("_thickness");
            thicknessProperty.floatValue = EditorGUILayout.FloatField("Thickness", thicknessProperty.floatValue);
        
            SerializedProperty rotationProperty = serializedObject.FindProperty("_rotation");
            rotationProperty.floatValue = EditorGUILayout.Slider("Rotation", rotationProperty.floatValue, 0, 360);

            SerializedProperty degreesProperty = serializedObject.FindProperty("_degrees");
            degreesProperty.floatValue = EditorGUILayout.Slider("Degrees", degreesProperty.floatValue, 0, 360);
        
            SerializedProperty densityProperty = serializedObject.FindProperty("_density");
            float densityValue = (float)densityProperty.intValue;
            densityProperty.intValue = (int)EditorGUILayout.Slider("Density", densityValue, 1, 200);
        
            serializedObject.ApplyModifiedProperties();
        }
    }
    
#endif
}
