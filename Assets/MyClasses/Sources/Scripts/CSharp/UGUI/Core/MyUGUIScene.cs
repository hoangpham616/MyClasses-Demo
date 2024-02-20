/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIScene (version 2.12)
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
        public MyUGUIScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene = false, bool isHideHUD = false)
            : base((int)id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD)
        {
        }

        #endregion
    }
}