/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIHUD (version 2.7)
 */

namespace MyClasses.UI
{
    public abstract class MyUGUIHUD : MyUGUIHUDBase
    {
        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIHUD(string prefabName)
            : base(prefabName, null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIHUD(string prefabNameCanvas, string prefabName3D)
            : base(prefabNameCanvas, prefabName3D)
        {
        }

        #endregion

        #region ----- MyUGUIHUDBase Implementation -----

        /// <summary>
        /// OnUGUISceneSwitch.
        /// </summary>
        public override void OnUGUISceneSwitch(MyUGUISceneBase scene)
        {
            OnUGUISceneSwitch((MyUGUIScene)scene);
        }

        /// <summary>
        /// OnUGUIPopupShow.
        /// </summary>
        public override void OnUGUIPopupShow(MyUGUIPopupBase popup)
        {
            OnUGUIPopupShow((MyUGUIPopup)popup);
        }

        /// <summary>
        /// OnUGUISceneSwitch.
        /// </summary>
        public virtual void OnUGUISceneSwitch(MyUGUIScene scene)
        {
        }

        /// <summary>
        /// OnUGUIPopupShow.
        /// </summary>
        public virtual void OnUGUIPopupShow(MyUGUIPopup popup)
        {
        }

        #endregion
    }
}