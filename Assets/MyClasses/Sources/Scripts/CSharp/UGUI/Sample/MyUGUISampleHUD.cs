﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUISampleHUD (version 2.28)
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
    public class MyUGUISampleHUD : MyUGUIHUD
    {
        #region ----- Variable -----

        // private MyUGUIButton _buttonBack;

        #endregion

        #region ----- Constructor -----

        public MyUGUISampleHUD(string prefabNameCanvas, string prefabName3D)
            : base(prefabNameCanvas, prefabName3D)
        {
        }

        #endregion

        #region ----- MyUGUIHUD Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            // _buttonBack = MyUtilities.FindObject(GameObjectCanvas, "Something/Something/ButtonBack").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);
            
            base.OnUGUIEnter();

            // _buttonBack.OnEventPointerClick.AddListener(_OnClickBack);
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);
            
            base.OnUGUIExit();

            // _buttonBack.OnEventPointerClick.RemoveAllListeners();
        }

        public override void OnUGUISceneSwitch(MyUGUIScene scene)
        {
            this.LogInfo("OnUGUISceneSwitch", scene.ID.ToString(), ELogColor.DARK_UI);
            
            switch (scene.ID)
            {
                default:
                    {

                    }
                    break;
            }
        }

        public override void OnUGUISubSceneSwitch(MyUGUIScene scene, MyUGUISubScene subScene)
        {
            this.LogInfo("OnUGUISubSceneSwitch", scene.ID.ToString() + " | " + subScene.ID.ToString(), ELogColor.DARK_UI);
            
            switch (subScene.ID)
            {
                default:
                    {

                    }
                    break;
            }
        }

        public override void OnUGUIPopupShow(MyUGUIPopup popup)
        {
            this.LogInfo("OnUGUIPopupShow", popup.ID.ToString(), ELogColor.DARK_UI);
            
            switch (popup.ID)
            {
                default:
                    {

                    }
                    break;
            }
        }

        public override void OnUGUIPopupHide(MyUGUIPopup popup)
        {
            this.LogInfo("OnUGUIPopupHide", popup.ID.ToString(), ELogColor.DARK_UI);
            
            switch (popup.ID)
            {
                default:
                    {

                    }
                    break;
            }
        }

        public override void OnUGUITopLevelPopupChange(MyUGUIPopup popup)
        {
            this.LogInfo("OnUGUITopLevelPopupChange", popup.ID.ToString(), ELogColor.DARK_UI);
            
            switch (popup.ID)
            {
                default:
                    {

                    }
                    break;
            }
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickBack(PointerEventData arg0)
        {
            this.LogInfo("_OnClickBack", null, ELogColor.UI);

            MyUGUIManager.Instance.Back();
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}