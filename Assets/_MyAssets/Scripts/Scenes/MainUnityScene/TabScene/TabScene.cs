using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;

namespace MyApp.UI
{
    public class TabScene : MyUGUIScene
    {
        #region ----- Variable -----
        
        private MyUGUIButton _buttonMainMenuScene;

        #endregion

        #region ----- Constructor -----

        public TabScene(ESceneID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isInitWhenLoadUnityScene = false, bool isHideHUD = false)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isInitWhenLoadUnityScene, isHideHUD)
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
            this.LogInfo("OnUGUIEnter", "SceneId=" + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();
            
            _buttonMainMenuScene.OnEventPointerClick.AddListener(_OnClickMainMenuScene);
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