/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIScene (version 2.16)
 */

#pragma warning disable 0108

using UnityEngine;
using System;

namespace MyClasses.UI
{
    public class MyUGUIScene : MyUGUISceneBase
    {
        #region ----- Property -----

        public ESceneID ID
        {
            get { return (ESceneID)_config.ID; }
        }

        public EUnitySceneID UnitySceneID
        {
            get { return (EUnitySceneID)_unitySceneID; }
        }

        public MyUGUISubScene CurrentSubScene
        {
            get { return (MyUGUISubScene)_currentSubScene; }
        }

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIScene(MyUGUIConfigScene config, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback)
            : base(config, onSubSceneSwitchCallback)
        {
        }

        #endregion

        #region ----- Public Method -----

        /// <summary>
        /// Returns a sub scene.
        /// </summary>
        public MyUGUISubScene GetSubScene(ESubSceneID id)
        {
            return (MyUGUISubScene)GetSubScene((int)id);
        }

        /// <summary>
        /// Switches sub scene.
        /// </summary>
        public void SwitchSubScene(ESubSceneID id, Action<MyUGUISubSceneBase> onEnterCallback = null)
        {
            SwitchSubScene((int)id, onEnterCallback);
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