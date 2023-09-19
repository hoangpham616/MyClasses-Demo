/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIPopup (version 2.26)
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
        public MyUGUIPopup(EPopupID id, string prefabNameCanvas, bool isFloat = false, bool isRepeatable = false)
            : base((int)id, prefabNameCanvas, isFloat, isRepeatable)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIPopup(EPopupID id, string prefabNameCanvas, string prefabName3D, bool isFloat = false, bool isRepeatable = false)
            : base((int)id, prefabNameCanvas, prefabName3D, isFloat, isRepeatable)
        {
        }

        #endregion
    }
}