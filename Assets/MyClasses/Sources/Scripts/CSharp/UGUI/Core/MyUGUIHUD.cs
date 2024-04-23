/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIHUD (version 2.8)
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
        /// OnUGUISubSceneSwitch.
        /// </summary>
        public override void OnUGUISubSceneSwitch(MyUGUISceneBase scene, MyUGUISubSceneBase subScene)
        {
            OnUGUISubSceneSwitch((MyUGUIScene)scene, (MyUGUISubScene)subScene);
        }

        /// <summary>
        /// OnUGUIPopupShow.
        /// </summary>
        public override void OnUGUIPopupShow(MyUGUIPopupBase popup)
        {
            OnUGUIPopupShow((MyUGUIPopup)popup);
        }

        /// <summary>
        /// OnUGUIPopupHide.
        /// </summary>
        public override void OnUGUIPopupHide(MyUGUIPopupBase popup)
        {
            OnUGUIPopupHide((MyUGUIPopup)popup);
        }

        /// <summary>
        /// OnUGUITopLevelPopupChange.
        /// </summary>
        public override void OnUGUITopLevelPopupChange(MyUGUIPopupBase popup)
        {
            OnUGUITopLevelPopupChange((MyUGUIPopup)popup);
        }

        /// <summary>
        /// OnUGUISceneSwitch.
        /// </summary>
        public virtual void OnUGUISceneSwitch(MyUGUIScene scene)
        {
        }

        /// <summary>
        /// OnUGUISubSceneSwitch.
        /// </summary>
        public virtual void OnUGUISubSceneSwitch(MyUGUIScene scene, MyUGUISubScene subScene)
        {
        }

        /// <summary>
        /// OnUGUIPopupShow.
        /// </summary>
        public virtual void OnUGUIPopupShow(MyUGUIPopup popup)
        {
        }

        /// <summary>
        /// OnUGUIPopupHide.
        /// </summary>
        public virtual void OnUGUIPopupHide(MyUGUIPopup popup)
        {
        }

        /// <summary>
        /// OnUGUITopLevelPopupChange.
        /// </summary>
        public virtual void OnUGUITopLevelPopupChange(MyUGUIPopup popup)
        {
        }

        #endregion
    }
}