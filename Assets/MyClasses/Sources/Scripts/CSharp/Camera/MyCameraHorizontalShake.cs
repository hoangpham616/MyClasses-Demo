﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyCameraHorizontalShake (version 1.2)
 * Requirement: MyShaderImageHorizontalShake.shader
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
    public class MyCameraHorizontalShake : MonoBehaviour
    {
        #region ----- Define -----

        private readonly string _INTENSITY = "_Intensity";
        private readonly string _SHAKE_TIME = "_ShakeTime";

        #endregion

        #region ----- Variable -----

        [SerializeField, Range(0, 1)]
        private float _intensity = 0.1f;

        private Material _material;
        private Shader _shader;
        private float _time;

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

            _time += Time.deltaTime * _intensity * 11.3f;

            _material.SetFloat(_INTENSITY, _intensity);
            _material.SetFloat(_SHAKE_TIME, _time);
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
                    _shader = AssetDatabase.LoadAssetAtPath<Shader>(paths[i] + "/Sources/Shaders/ImageEffect/MyShaderImageHorizontalShake.shader");
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