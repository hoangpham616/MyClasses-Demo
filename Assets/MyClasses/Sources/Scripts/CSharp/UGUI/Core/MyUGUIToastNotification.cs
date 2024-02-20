/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIToastNotification (version 2.0)
 */

#pragma warning disable 0108

namespace MyClasses.UI
{
    public class MyUGUIToastNotification : MyUGUIToastNotificationBase
    {
        #region ----- Property -----

        public EToastNotificationID ID
        {
            get { return (EToastNotificationID)_id; }
        }

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIToastNotification(EToastNotificationID id, string prefabNameCanvas, string prefabName3D)
            : base((int)id, prefabNameCanvas, prefabName3D)
        {
        }

        #endregion
    }
}