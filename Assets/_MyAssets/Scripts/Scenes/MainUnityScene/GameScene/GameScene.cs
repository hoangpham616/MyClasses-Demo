﻿using UnityEngine;
using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class GameScene : MyUGUIScene
    {
        #region ----- Variable -----
        
        private MyUGUIButton _buttonMainMenuScene;

        #endregion

        #region ----- Constructor -----

        public GameScene(ESceneID id, string prefabName, bool isInitWhenLoadScene, bool isHideHUD = false, float fadeInDuration = 0.5f, float fadeOutDuration = 0.5f)
        : base(id, prefabName, isInitWhenLoadScene, isHideHUD, fadeInDuration, fadeOutDuration)
        {
        }

        #endregion

        #region ----- MyUGUIScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonMainMenuScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonMainMenuScene").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "scene id = " + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();
            
            _buttonMainMenuScene.OnEventPointerClick.AddListener(_OnClickMainMenuScene);

            GameObject3D.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 1.5f);

            
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

            _buttonMainMenuScene.OnEventPointerClick.RemoveAllListeners();
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

        #endregion

        #region ----- Button Event -----

        private void _OnClickMainMenuScene(PointerEventData arg0)
        {
            MyUGUIManager.Instance.ShowScene(ESceneID.MainMenuScene);
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}