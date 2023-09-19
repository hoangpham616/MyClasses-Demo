/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIUnityScene (version 2.2)
 */

#pragma warning disable 0108

namespace MyClasses.UI
{
    public class MyUGUIUnityScene : MyUGUIUnitySceneBase
    {
        #region ----- Variable -----

        private EUnitySceneID _id;
        
        #endregion

        #region ----- Property -----

        public EUnitySceneID ID
        {
            get { return _id; }
        }
        
        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIUnityScene(EUnitySceneID sceneID, string unitySceneName) : base ((int)sceneID, unitySceneName)
        {
            _id = sceneID;
        }

        #endregion
    }
}