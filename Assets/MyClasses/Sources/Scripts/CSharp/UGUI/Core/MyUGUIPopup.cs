﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIPopup (version 2.27)
 */

#pragma warning disable 0108

namespace MyClasses.UI
{
    public class MyUGUIPopup : MyUGUIPopupBase
    {
        #region ----- Property -----

        public EPopupID ID
        {
            get { return (EPopupID)_id; }
        }

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isRepeatable">show multiple popups at the same time</param>
        public MyUGUIPopup(EPopupID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isRepeatable = false)
            : base((int)id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isRepeatable)
        {
        }

        #endregion
    }
}