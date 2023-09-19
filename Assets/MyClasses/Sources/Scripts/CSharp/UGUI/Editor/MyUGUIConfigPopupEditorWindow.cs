/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIConfigPopupEditorWindow (version 2.11)
 */

using System;

namespace MyClasses.UI.Tool
{
    public class MyUGUIConfigPopupEditorWindow : MyUGUIConfigPopupEditorWindowBase
    {
        #region ----- MyUGUIConfigPopupEditorWindowBase Implementation -----

        /// <summary>
        /// Return the number of popup.
        /// </summary>
        protected override int _GetPopupQuantity()
        {
            return Enum.GetValues(typeof(EPopupID)).Length;
        }

        /// <summary>
        /// Return the name of popup.
        /// </summary>
        protected override string _GetPopupName(int id)
        {
            return ((EPopupID)id).ToString();
        }

        #endregion
    }
}
