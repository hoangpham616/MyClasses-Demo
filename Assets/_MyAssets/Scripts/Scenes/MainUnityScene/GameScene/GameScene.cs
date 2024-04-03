using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class GameScene : MyUGUIScene
    {
        #region ----- Variable -----
        
        private MyUGUIButton _buttonMainScene;

        #endregion

        #region ----- Constructor -----

        public GameScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene = false, bool isHideHUD = false)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD)
        {
        }

        #endregion

        #region ----- MyUGUIScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonMainScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonMainScene").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "SceneId=" + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();
            
            _buttonMainScene.OnEventPointerClick.AddListener(_OnClickMainMenuScene);

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

            _buttonMainScene.OnEventPointerClick.RemoveAllListeners();
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
            MyUGUIManager.Instance.ShowScene(ESceneID.MainScene, ESceneTransition.FadeIn, 0.4f);
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}