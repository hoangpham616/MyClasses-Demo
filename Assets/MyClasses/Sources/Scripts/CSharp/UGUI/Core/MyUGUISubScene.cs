/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUISubScene (version 2.1)
 */

using UnityEngine;

namespace MyClasses.UI
{
    public class MyUGUISubScene : MyUGUISubSceneBase
    {
        #region ----- Property -----

        public new MyUGUIScene Scene
        {
            get { return (MyUGUIScene)_scene; }
        }

        public new ESubSceneID ID
        {
            get { return (ESubSceneID)_id; }
        }

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUISubScene(MyUGUIScene scene, int index, ESubSceneID id, GameObject gameObjectCanvas, Vector3 parentTargetAnchoredPosition)
            : base(scene, index, (int)id, gameObjectCanvas, parentTargetAnchoredPosition)
        {
        }

        #endregion
    }
}