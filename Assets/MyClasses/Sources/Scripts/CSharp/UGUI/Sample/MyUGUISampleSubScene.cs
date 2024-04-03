/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUISampleSubScene (version 2.2)
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
    public class MyUGUISampleSubScene : MyUGUISubScene
    {
        #region ----- Variable -----

        // private MyUGUIButton _buttonSample;

        #endregion

        #region ----- Constructor -----

        public MyUGUISampleSubScene(MyUGUIScene scene, int index, ESubSceneID id, GameObject gameObject, Vector3 parentTargetAnchoredPosition)
            : base (scene, index, id, gameObject, parentTargetAnchoredPosition)
        {
        }

        #endregion

        #region ----- MyUGUISubScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            // _buttonSample = MyUtilities.FindObject(GameObject, "Something/Something/ButtonSample").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUISceneEnter()
        {
            this.LogInfo("OnUGUISceneEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUISceneEnter();
        }

        public override void OnUGUIFirstEnter()
        {
            this.LogInfo("OnUGUIFirstEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIFirstEnter();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            // _buttonSample.OnEventPointerClick.AddListener(_OnClickSample);
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUISceneExit()
        {
            this.LogInfo("OnUGUISceneExit", null, ELogColor.DARK_UI);

            base.OnUGUISceneExit();

            // _buttonSample.OnEventPointerClick.RemoveAllListeners();
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