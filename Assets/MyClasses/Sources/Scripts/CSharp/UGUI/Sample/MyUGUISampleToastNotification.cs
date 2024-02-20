﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUISampleToastNotification (version 2.0)
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class MyUGUISampleToastNotification : MyUGUIToastNotification
    {
        #region ----- Variable -----

        // private MyUGUIButton _buttonClose;

        #endregion

        #region ----- Constructor -----

        public MyUGUISampleToastNotification(EToastNotificationID id, string prefabNameCanvas, string prefabName3D)
            : base(id, prefabNameCanvas, prefabName3D)
        {
        }


        #endregion

        #region ----- MyUGUIToastNotification Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            // _buttonClose = MyUtilities.FindObject(GameObject, "Container/ButtonClose").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            // _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
        }

        public override float OnUGUIAnimationShow()
        {
            this.LogInfo("OnUGUIAnimationShow", null, ELogColor.DARK_UI);

            return 0;
        }

        public override bool OnUGUIVisible()
        {
            if (base.OnUGUIVisible())
            {
                this.LogInfo("OnUGUIVisible", null, ELogColor.DARK_UI);

                return true;
            }
            return false;
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
            base.OnUGUIUpdate(deltaTime);
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);

            base.OnUGUIExit();

            // _buttonClose.OnEventPointerClick.RemoveAllListeners();
        }

        public override float OnUGUIAnimationHide()
        {
            this.LogInfo("OnUGUIAnimationHide", null, ELogColor.DARK_UI);

            return 0;
        }

        public override float OnUGUIAnimationQuickHide()
        {
            this.LogInfo("OnUGUIAnimationQuickHide", null, ELogColor.DARK_UI);

            return 0;
        }

        public override bool OnUGUIInvisible()
        {
            if (base.OnUGUIInvisible())
            {
                this.LogInfo("OnUGUIInvisible", null, ELogColor.DARK_UI);

                return true;
            }
            return false;
        }

        public override float GetDisplayTime()
        {
            return 2;
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            this.LogInfo("_OnClickClose", null, ELogColor.UI);

            Hide();
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}