/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIConfigToastNotificationEditorWindow (version 2.0)
 */

using System;

namespace MyClasses.UI.Tool
{
    public class MyUGUIConfigToastNotificationEditorWindow : MyUGUIConfigToastNotificationEditorWindowBase
    {
        #region ----- MyUGUIConfigToastNotificationEditorWindowBase Implementation -----

        /// <summary>
        /// Return the number of toast notification.
        /// </summary>
        protected override int _GetToastNotificationQuantity()
        {
            return Enum.GetValues(typeof(EToastNotificationID)).Length;
        }

        /// <summary>
        /// Return the name of popup.
        /// </summary>
        protected override string _GetToastNotificationName(int id)
        {
            return ((EToastNotificationID)id).ToString();
        }

        #endregion
    }
}
