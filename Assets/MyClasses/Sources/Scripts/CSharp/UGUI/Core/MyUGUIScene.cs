/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIScene (version 2.14)
 */

#pragma warning disable 0108

using UnityEngine;
using System;
using System.Collections.Generic;

namespace MyClasses.UI
{
    public class MyUGUIScene : MyUGUISceneBase
    {
        #region ----- Property -----

        public ESceneID ID
        {
            get { return (ESceneID)_id; }
        }

        public EUnitySceneID UnitySceneID
        {
            get { return (EUnitySceneID)_unitySceneID; }
        }

        public new MyUGUISubScene CurrentSubScene
        {
            get { return (MyUGUISubScene)_currentSubScene; }
        }

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene, bool isHideHUD, List<MyUGUIConfigSubScene> subSceneConfigs = null, float minWidthPercentToSwitchSubScene = 10, float switchSubSceneTime = 0.1f, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback = null)
            : base((int)id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD, subSceneConfigs, minWidthPercentToSwitchSubScene, switchSubSceneTime, onSubSceneSwitchCallback)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene, bool isHideHUD)
            : base((int)id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD)
        {
        }

        #endregion

        #region ----- Public Method -----

        /// <summary>
        /// Returns a sub scene.
        /// </summary>
        public MyUGUISubSceneBase GetSubScene(ESubSceneID id)
        {
            return _GetSubScene((int)id);
        }

        /// <summary>
        /// Switches sub scene.
        /// </summary>
        public void SwitchSubScene(ESubSceneID id)
        {
            _SwitchSubScene((int)id);
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Create sub scene according to config.
        /// </summary>
        protected override void _CreateSubScene(ref MyUGUISubSceneBase subScene, MyUGUIConfigSubScene subSceneConfig, MyUGUISceneBase scene, int index, GameObject gameObjectSubScene, Vector3 parentTargetAnchoredPosition)
        {
            subScene = (MyUGUISubScene)Activator.CreateInstance(MyUtilities.FindTypesByName(subSceneConfig.ScriptName)[0], scene, index, (ESubSceneID)subSceneConfig.ID, gameObjectSubScene, parentTargetAnchoredPosition);
        }

        #endregion
    }
}