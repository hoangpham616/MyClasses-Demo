/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIScene (version 2.10)
 */

#pragma warning disable 0108

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

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIScene(ESceneID id, string prefabNameCanvas, bool isInitWhenLoadUnityScene, bool isHideHUD = false, float fadeInDuration = 0.2f, float fadeOutDuration = 0.2f)
            : base((int)id, prefabNameCanvas, isInitWhenLoadUnityScene, isHideHUD, fadeInDuration, fadeOutDuration)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIScene(ESceneID id, string prefabNameCanvas, string prefabName3D, bool isInitWhenLoadUnityScene, bool isHideHUD = false, float fadeInDuration = 0.2f, float fadeOutDuration = 0.2f)
            : base((int)id, prefabNameCanvas, prefabName3D, isInitWhenLoadUnityScene, isHideHUD, fadeInDuration, fadeOutDuration)
        {
        }

        #endregion
    }
}