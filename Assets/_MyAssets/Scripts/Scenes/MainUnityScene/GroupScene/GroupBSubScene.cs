﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;

namespace MyApp.UI
{
    public class GroupBSubScene : MyUGUISubScene
    {
        #region ----- Variable -----
        


        #endregion

        #region ----- Constructor -----

        public GroupBSubScene(MyUGUIScene scene, int index, ESubSceneID id, GameObject gameObject, Vector3 parentTargetAnchoredPosition)
            : base (scene, index, id, gameObject, parentTargetAnchoredPosition)
        {
        }

        #endregion

        #region ----- MyUGUISubScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            ScrollRect scrollRect = MyUtilities.FindObject(GameObjectCanvas, "ScrollView").GetComponent<ScrollRect>();

            RegisterScrollRects(new List<ScrollRect>() { scrollRect });
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
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUISceneExit()
        {
            this.LogInfo("OnUGUISceneExit", null, ELogColor.DARK_UI);

            base.OnUGUISceneExit();
        }

        #endregion

        #region ----- Button Event -----



        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}