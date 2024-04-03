using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class MainHUD : MyUGUIHUD
    {
        #region ----- Variable -----

        private Text _textSceneName;

        #endregion

        #region ----- Constructor -----

        public MainHUD(string prefabNameCanvas, string prefabName3D)
            : base(prefabNameCanvas, prefabName3D)
        {
        }

        #endregion

        #region ----- MyUGUIHUD Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _textSceneName = MyUtilities.FindObject(GameObjectCanvas, "Header/SceneName").GetComponent<Text>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);

            base.OnUGUIExit();
        }

        public override void OnUGUISceneSwitch(MyUGUIScene scene)
        {
            this.LogInfo("OnUGUISceneSwitch", "switch to " + scene.ID.ToString(), ELogColor.UI);

            switch (scene.ID)
            {
                case ESceneID.MainScene:
                    {
                        _textSceneName.text = "HUD\nMAIN SCENE";
                    }
                    break;

                case ESceneID.GroupScene:
                    {
                        _textSceneName.text = "HUD\nGROUP SCENE";
                    }
                    break;

                case ESceneID.GameScene:
                    {
                        _textSceneName.text = "HUD\nGAME SCENE";
                    }
                    break;
                    
                default:
                    {
                        _textSceneName.text = "HUD\nnot implemented yet";
                    }
                    break;
            }
        }

        public override void OnUGUIPopupShow(MyUGUIPopup popup)
        {
            this.LogInfo("OnUGUIPopupShow", popup.ID.ToString() + " shows", ELogColor.UI);
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