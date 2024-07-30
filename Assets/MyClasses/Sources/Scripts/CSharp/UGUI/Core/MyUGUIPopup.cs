/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIPopup (version 2.28)
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
        public MyUGUIPopup(MyUGUIConfigPopup config, bool isRepeatable = false)
            : base(config, isRepeatable)
        {
        }

        #endregion
    }
}