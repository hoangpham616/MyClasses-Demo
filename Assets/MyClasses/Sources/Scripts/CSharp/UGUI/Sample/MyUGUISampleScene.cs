/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUISampleScene (version 2.27)
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
    public class MyUGUISampleScene : MyUGUIScene
    {
        #region ----- Variable -----

        // private MyUGUIButton _buttonSample;

        #endregion

        #region ----- Constructor -----

        public MyUGUISampleScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene = false, bool isHideHUD = false, List<MyUGUIConfigSubScene> subSceneConfigs = null, float minWidthPercentToSwitchSubScene = 10, float switchSubSceneTime = 0.1f, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback = null)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD, subSceneConfigs, minWidthPercentToSwitchSubScene, switchSubSceneTime, onSubSceneSwitchCallback)
        {
        }

        public MyUGUISampleScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene = false, bool isHideHUD = false)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD)
        {
        }

        #endregion

        #region ----- MyUGUIScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            // _buttonSample = MyUtilities.FindObject(GameObject, "Something/Something/ButtonSample").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            // _buttonSample.OnEventPointerClick.AddListener(_OnClickSample);
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
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);

            base.OnUGUIExit();

            // _buttonSample.OnEventPointerClick.RemoveAllListeners();
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

        public override void OnUGUIBackKey()
        {
            this.LogInfo("OnUGUIBackKey", null, ELogColor.DARK_UI);

            MyUGUIManager.Instance.Back();
        }

        public override void OnUGUISubSceneSwitch(MyUGUISubSceneBase subScene)
        {
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickSample(PointerEventData arg0)
        {
            this.LogInfo("_OnClickSample", null, ELogColor.UI);
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}