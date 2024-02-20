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
        private GameObject _gameObjectFooter;
        private MyUGUIButton _buttonTabAScene;
        private MyUGUIButton _buttonTabBScene;
        private MyUGUIButton _buttonTabCScene;

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
            _gameObjectFooter = MyUtilities.FindObject(GameObjectCanvas, "Footer");
            _buttonTabAScene = MyUtilities.FindObject(_gameObjectFooter, "ButtonTabAScene").GetComponent<MyUGUIButton>();
            _buttonTabBScene = MyUtilities.FindObject(_gameObjectFooter, "ButtonTabBScene").GetComponent<MyUGUIButton>();
            _buttonTabCScene = MyUtilities.FindObject(_gameObjectFooter, "ButtonTabCScene").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonTabAScene.OnEventPointerClick.AddListener(_OnClickTabAScene);
            _buttonTabBScene.OnEventPointerClick.AddListener(_OnClickTabBScene);
            _buttonTabCScene.OnEventPointerClick.AddListener(_OnClickTabCScene);
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);

            base.OnUGUIExit();

            _buttonTabAScene.OnEventPointerClick.RemoveAllListeners();
            _buttonTabBScene.OnEventPointerClick.RemoveAllListeners();
            _buttonTabCScene.OnEventPointerClick.RemoveAllListeners();
        }

        public override void OnUGUISceneSwitch(MyUGUIScene scene)
        {
            this.LogInfo("OnUGUISceneSwitch", "switch to " + scene.ID.ToString(), ELogColor.UI);

            switch (scene.ID)
            {
                case ESceneID.MainMenuScene:
                    {
                        _textSceneName.text = "HUD\nMAIN MENU SCENE";
                        _gameObjectFooter.SetActive(false);
                    }
                    break;

                case ESceneID.GameScene:
                    {
                        _textSceneName.text = "HUD\nGAME SCENE";
                    }
                    break;

                case ESceneID.TabAScene:
                    {
                        _textSceneName.text = "HUD\nTAB A SCENE";
                        _gameObjectFooter.SetActive(true);
                        _buttonTabAScene.SetDark(true);
                        _buttonTabAScene.SetEnable(false);
                        _buttonTabBScene.SetDark(false);
                        _buttonTabBScene.SetEnable(true);
                        _buttonTabCScene.SetDark(false);
                        _buttonTabCScene.SetEnable(true);
                    }
                    break;

                case ESceneID.TabBScene:
                    {
                        _textSceneName.text = "HUD\nTAB B SCENE";
                        _gameObjectFooter.SetActive(true);
                        _buttonTabAScene.SetDark(false);
                        _buttonTabAScene.SetEnable(true);
                        _buttonTabBScene.SetDark(true);
                        _buttonTabBScene.SetEnable(false);
                        _buttonTabCScene.SetDark(false);
                        _buttonTabCScene.SetEnable(true);
                    }
                    break;

                case ESceneID.TabCScene:
                    {
                        _textSceneName.text = "HUD\nTAB C SCENE";
                        _gameObjectFooter.SetActive(true);
                        _buttonTabAScene.SetDark(false);
                        _buttonTabAScene.SetEnable(true);
                        _buttonTabBScene.SetDark(false);
                        _buttonTabBScene.SetEnable(true);
                        _buttonTabCScene.SetDark(true);
                        _buttonTabCScene.SetEnable(false);
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

        private void _OnClickTabAScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTabAScene", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowScene(ESceneID.TabAScene, ESceneTransition.SlideInFromLeft);
        }

        private void _OnClickTabBScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTabBScene", null, ELogColor.UI);

            if (MyUGUIManager.Instance.CurrentScene.ID == ESceneID.TabAScene)
            {
                MyUGUIManager.Instance.ShowScene(ESceneID.TabBScene, ESceneTransition.SlideInFromRight);
            }
            else
            {
                MyUGUIManager.Instance.ShowScene(ESceneID.TabBScene, ESceneTransition.SlideInFromLeft);
            }
        }

        private void _OnClickTabCScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTabCScene", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowScene(ESceneID.TabCScene, ESceneTransition.SlideInFromRight);
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}