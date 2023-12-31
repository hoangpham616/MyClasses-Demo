﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyCameraScanLineGlitch (version 1.2)
 * Requirement: MyShaderImageScanLineGlitch.shader
 */

#pragma warning disable 0649

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace MyClasses
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class MyCameraScanLineGlitch : MonoBehaviour
    {
        #region ----- Define -----

        private readonly string _INTENSITY = "_Intensity";

        #endregion

        #region ----- Variable -----

        [SerializeField, Range(0, 1)]
        private float _intensity = 0.3f;

        private Material _material;
        private Shader _shader;

        #endregion

        #region ----- MonoBehaviour Implementation -----

        /// <summary>
        /// Start.
        /// </summary>
        private void Start()
        {
            _Initialize();
        }

        /// <summary>
        /// OnRenderImage.
        /// </summary>
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
#if UNITY_EDITOR
            if (_material == null)
            {
                _Initialize();
            }
#endif

            _material.SetFloat(_INTENSITY, _intensity);
            Graphics.Blit(source, destination, _material);
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Initialize.
        /// </summary>
        private void _Initialize()
        {
#if UNITY_EDITOR
            if (_shader == null)
            {
                string[] paths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
                for (int i = 0; i < paths.Length; i++)
                {
                    _shader = AssetDatabase.LoadAssetAtPath<Shader>(paths[i] + "/Sources/Shaders/ImageEffect/MyShaderImageScanLineGlitch.shader");
                    if (_shader != null)
                    {
                        break;
                    }
                }
            }
#endif

            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        #endregion
    }
}