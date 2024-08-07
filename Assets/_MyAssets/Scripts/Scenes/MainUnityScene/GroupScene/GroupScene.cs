﻿using UnityEngine.EventSystems;
using System;
using MyClasses;
using MyClasses.UI;

namespace MyApp.UI
{
    public class GroupScene : MyUGUIScene
    {
        #region ----- Variable -----
        
        private MyUGUIButton _buttonMainScene;
        private MyUGUIButton _buttonA;
        private MyUGUIButton _buttonB;
        private MyUGUIButton _buttonC;
        private MyUGUIButton _buttonD;
        private MyUGUIButton _buttonE;

        #endregion

        #region ----- Constructor -----

        public GroupScene(MyUGUIConfigScene config, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback)
            : base(config, onSubSceneSwitchCallback)
        {
        }

        #endregion

        #region ----- MyUGUIScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonMainScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonMainScene").GetComponent<MyUGUIButton>();
            _buttonA = MyUtilities.FindObject(GameObjectCanvas, "Footer/ButtonA").GetComponent<MyUGUIButton>();
            _buttonB = MyUtilities.FindObject(GameObjectCanvas, "Footer/ButtonB").GetComponent<MyUGUIButton>();
            _buttonC = MyUtilities.FindObject(GameObjectCanvas, "Footer/ButtonC").GetComponent<MyUGUIButton>();
            _buttonD = MyUtilities.FindObject(GameObjectCanvas, "Footer/ButtonD").GetComponent<MyUGUIButton>();
            _buttonE = MyUtilities.FindObject(GameObjectCanvas, "Footer/ButtonE").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "SceneId=" + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();
            
            _buttonMainScene.OnEventPointerClick.AddListener(_OnClickMainScene);
            _buttonA.OnEventPointerClick.AddListener(_OnClickA);
            _buttonB.OnEventPointerClick.AddListener(_OnClickB);
            _buttonC.OnEventPointerClick.AddListener(_OnClickC);
            _buttonD.OnEventPointerClick.AddListener(_OnClickD);
            _buttonE.OnEventPointerClick.AddListener(_OnClickE);
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

            _buttonMainScene.OnEventPointerClick.RemoveAllListeners();
            _buttonA.OnEventPointerClick.RemoveAllListeners();
            _buttonB.OnEventPointerClick.RemoveAllListeners();
            _buttonC.OnEventPointerClick.RemoveAllListeners();
            _buttonD.OnEventPointerClick.RemoveAllListeners();
            _buttonE.OnEventPointerClick.RemoveAllListeners();
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
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickMainScene(PointerEventData arg0)
        {
            MyUGUIManager.Instance.ShowScene(ESceneID.MainScene, ESceneTransition.FadeIn, 0.4f);
        }

        private void _OnClickA(PointerEventData arg0)
        {
            SwitchSubScene(ESubSceneID.GroupASubScene, (subScene) =>
            {
                MyUGUIManager.Instance.ShowToastMessage("switched to A Sub Scene");
            });
        }

        private void _OnClickB(PointerEventData arg0)
        {
            MyUGUIManager.Instance.ShowScene(ESceneID.GroupScene, ESubSceneID.GroupBSubScene, (subScene) =>
            {
                MyUGUIManager.Instance.ShowToastMessage("switched to B Sub Scene");
            });
        }

        private void _OnClickC(PointerEventData arg0)
        {
            SwitchSubScene(ESubSceneID.GroupCSubScene, (subScene) =>
            {
                MyUGUIManager.Instance.ShowToastMessage("switched to C Sub Scene");
            });
        }

        private void _OnClickD(PointerEventData arg0)
        {
            SwitchSubScene(ESubSceneID.GroupDSubScene, (subScene) =>
            {
                MyUGUIManager.Instance.ShowToastMessage("switched to D Sub Scene");
            });
        }

        private void _OnClickE(PointerEventData arg0)
        {
            SwitchSubScene(ESubSceneID.GroupESubScene, (subScene) =>
            {
                MyUGUIManager.Instance.ShowToastMessage("switched to E Sub Scene");
            });
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}