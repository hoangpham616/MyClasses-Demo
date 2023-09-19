using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;

namespace MyApp.UI
{
    public class MainMenuScene : MyUGUIScene
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonButton;
        private MyUGUIToggleButton _toggleToggle;
        private MyUGUIButton _buttonRunningMessage;
        private MyUGUIButton _buttonFlyingMessage;
        private MyUGUIButton _buttonToastMessage;
        private MyUGUIButton _buttonLoadingIndicator;
        private MyUGUIButton _buttonDialog2Buttons;
        private MyUGUIButton _buttonReuasbleListView;
        private MyUGUIButton _buttonReuasbleListView2;
        private MyUGUIButton _buttonExtension;
        private MyUGUIButton _buttonGameScene;
        private MyUGUIButton _buttonLogger;
        private MyUGUIButton _buttonCoroutine;
        private MyUGUIButton _buttonPool;
        private MyUGUIButton _buttonLocalization;
        private MyUGUIButton _buttonAdMob;

        #endregion

        #region ----- Constructor -----

        public MainMenuScene(ESceneID id, string prefabName, bool isInitWhenLoadScene, bool isHideHUD = false, float fadeInDuration = 0.2f, float fadeOutDuration = 0.2f)
        : base(id, prefabName, isInitWhenLoadScene, isHideHUD, fadeInDuration, fadeOutDuration)
        {
        }

        #endregion

        #region ----- MyUGUIScene Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonButton = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonButton").GetComponent<MyUGUIButton>();
            _toggleToggle = MyUtilities.FindObject(GameObjectCanvas, "Buttons/Toggle/ToggleButton").GetComponent<MyUGUIToggleButton>();
            _buttonRunningMessage = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonRunningMessage").GetComponent<MyUGUIButton>();
            _buttonFlyingMessage = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonFlyingMessage").GetComponent<MyUGUIButton>();
            _buttonToastMessage = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonToastMessage").GetComponent<MyUGUIButton>();
            _buttonLoadingIndicator = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonLoadingIndicator").GetComponent<MyUGUIButton>();
            _buttonDialog2Buttons = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonDialog2Buttons").GetComponent<MyUGUIButton>();
            _buttonReuasbleListView = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonReusableListView").GetComponent<MyUGUIButton>();
            _buttonReuasbleListView2 = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonReusableListView2").GetComponent<MyUGUIButton>();
            _buttonExtension = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonExtension").GetComponent<MyUGUIButton>();
            _buttonGameScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonGameScene").GetComponent<MyUGUIButton>();
            _buttonLogger = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonLogger").GetComponent<MyUGUIButton>();
            _buttonCoroutine = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonCoroutine").GetComponent<MyUGUIButton>();
            _buttonPool = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonPool").GetComponent<MyUGUIButton>();
            _buttonLocalization = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonLocalization").GetComponent<MyUGUIButton>();
            _buttonAdMob = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonAdMob").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "scene id = " + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonButton.OnEventPointerClick.AddListener(_OnClickButton);
            _toggleToggle.OnValueChange.AddListener(_OnToggleValueChange);
            _buttonRunningMessage.OnEventPointerDoubleClick.AddListener(_OnClickRunningMessage);
            _buttonFlyingMessage.OnEventPointerClick.AddListener(_OnClickFlyingMessage);
            _buttonToastMessage.OnEventPointerClick.AddListener(_OnClickToastMessage);
            _buttonLoadingIndicator.OnEventPointerClick.AddListener(_OnClickLoadingIndicator);
            _buttonDialog2Buttons.OnEventPointerClick.AddListener(_OnClickDialog2Buttons);
            _buttonReuasbleListView.OnEventPointerClick.AddListener(_OnClickReuasbleListView);
            _buttonReuasbleListView2.OnEventPointerClick.AddListener(_OnClickReuasbleListView2);
            _buttonExtension.OnEventPointerClick.AddListener(_OnClickExtension);
            _buttonGameScene.OnEventPointerClick.AddListener(_OnClickGameScene);
            _buttonLogger.OnEventPointerClick.AddListener(_OnClickLogger);
            _buttonCoroutine.OnEventPointerClick.AddListener(_OnClickCoroutine);
            _buttonPool.OnEventPointerClick.AddListener(_OnClickPool);
            _buttonLocalization.OnEventPointerClick.AddListener(_OnClickLocalization);
            _buttonAdMob.OnEventPointerClick.AddListener(_OnClickAdMob);

            _toggleToggle.SetToggle(false, false);
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

            _buttonButton.OnEventPointerClick.RemoveAllListeners();
            _toggleToggle.OnValueChange.RemoveAllListeners();
            _buttonRunningMessage.OnEventPointerDoubleClick.RemoveAllListeners();
            _buttonFlyingMessage.OnEventPointerClick.RemoveAllListeners();
            _buttonToastMessage.OnEventPointerClick.RemoveAllListeners();
            _buttonLoadingIndicator.OnEventPointerClick.RemoveAllListeners();
            _buttonDialog2Buttons.OnEventPointerClick.RemoveAllListeners();
            _buttonReuasbleListView.OnEventPointerClick.RemoveAllListeners();
            _buttonReuasbleListView2.OnEventPointerClick.RemoveAllListeners();
            _buttonExtension.OnEventPointerClick.RemoveAllListeners();
            _buttonGameScene.OnEventPointerClick.RemoveAllListeners();
            _buttonLogger.OnEventPointerClick.RemoveAllListeners();
            _buttonCoroutine.OnEventPointerClick.RemoveAllListeners();
            _buttonPool.OnEventPointerClick.RemoveAllListeners();
            _buttonLocalization.OnEventPointerClick.RemoveAllListeners();
            _buttonAdMob.OnEventPointerClick.RemoveAllListeners();
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

        private void _OnClickButton(PointerEventData arg0)
        {
            this.LogInfo("_OnClickButton", null, ELogColor.UI);

            if (_buttonButton.IsNormal)
            {
                _buttonButton.SetGrayMode(true, true);
                _buttonButton.SetGray(true);
                _buttonButton.SetText("Button\n(Grayscale)");
            }
            else if (_buttonButton.IsGray)
            {
                _buttonButton.SetDarkMode(true, true);
                _buttonButton.SetDark(true);
                _buttonButton.SetText("Button\n(Dark)");
            }
            else
            {
                _buttonButton.Normalize();
                _buttonButton.SetText("Button\n(Normal)");
            }
        }

        private void _OnToggleValueChange(bool isTurnOn)
        {
            this.LogInfo("_OnToggleValueChange", isTurnOn.ToString(), ELogColor.UI);
        }

        private void _OnClickRunningMessage(PointerEventData arg0)
        {
            this.LogInfo("_OnClickRunningMessage", null, ELogColor.UI);

            MyUGUIManager.Instance.SetRunningMessageMaxQueue(MyUGUIRunningMessage.EType.Default, 3);
            MyUGUIManager.Instance.ShowRunningMessage("This is Running Message 1 (will not show because out of queue)", ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType.Default);
            MyUGUIManager.Instance.ShowRunningMessage("This is Running Message 2 (will not show because out of queue)", ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType.Default);
            MyUGUIManager.Instance.ShowRunningMessage("This is Running Message 3", ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType.Default);
            MyUGUIManager.Instance.ShowRunningMessage("This is Running Message 4", ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType.Default);
            MyUGUIManager.Instance.ShowRunningMessage("This is Running Message 5", ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType.Default);
        }

        private void _OnClickFlyingMessage(PointerEventData arg0)
        {
            this.LogInfo("_OnClickFlyingMessage", null, ELogColor.UI);

            if (UnityEngine.Random.Range(0, 100) % 2 == 0)
            {
                MyUGUIManager.Instance.ShowFlyingMessage("This is Flying Message\nEType = ShortFlyFromBot", MyUGUIFlyingMessage.EType.ShortFlyFromBot);
            }
            else
            {
                MyUGUIManager.Instance.ShowFlyingMessage("This is Flying Message\nEType = ShortFlyFromMid", MyUGUIFlyingMessage.EType.ShortFlyFromMid);
            }
        }

        private void _OnClickToastMessage(PointerEventData arg0)
        {
            this.LogInfo("_OnClickToastMessage", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowToastMessage("This is Toast Message", EToastMessageDuration.Medium);
        }

        private void _OnClickLoadingIndicator(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLoadingIndicator", null, ELogColor.UI);

            int id1 = MyUGUIManager.Instance.ShowLoadingIndicator(1, () =>
            {
                this.LogInfo("_OnClickLoadingIndicator", "Loading Indicator 1 timeout", ELogColor.UI);
            });
            int id2 = MyUGUIManager.Instance.ShowLoadingIndicator(2, () =>
            {
                this.LogInfo("_OnClickLoadingIndicator", "Loading Indicator 2 timeout", ELogColor.UI);
            });
            int id3 = MyUGUIManager.Instance.ShowLoadingIndicator(3, () =>
            {
                this.LogInfo("_OnClickLoadingIndicator", "Loading Indicator 3 timeout", ELogColor.UI);
            });
        }

        private void _OnClickDialog2Buttons(PointerEventData arg0)
        {
            this.LogInfo("_OnClickDialog2Buttons", null, ELogColor.UI);

            MyUGUIPopup2Buttons popup = (MyUGUIPopup2Buttons)MyUGUIManager.Instance.ShowPopup(EPopupID.Dialog2ButtonsPopup);
            popup.SetData("TITLE", "Body", "Left", (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Left Button", ELogColor.UI);
            }, "Right", (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Right Button", ELogColor.UI);
            }, (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Close Button", ELogColor.UI);
            }, false);
        }

        private void _OnClickReuasbleListView(PointerEventData arg0)
        {
            this.LogInfo("_OnClickReuasbleListView", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.ReusableListViewPopup);
        }

        private void _OnClickReuasbleListView2(PointerEventData arg0)
        {
            this.LogInfo("_OnClickReuasbleListView2", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.ReusableListViewPopup2);
        }

        private void _OnClickExtension(PointerEventData arg0)
        {
            this.LogInfo("_OnClickExtension", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.ExtensionPopup);
        }

        private void _OnClickGameScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickGameScene", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowScene(ESceneID.GameScene);
        }

        private void _OnClickLogger(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLogger", null, ELogColor.UI);

            this.LogInfo("_OnClickLogger", "log color DEFAULT");
            this.LogError("_OnClickLogger", "log color DARK_CORE", ELogColor.DARK_SDK);
            this.LogWarning("_OnClickLogger", "log color CORE", ELogColor.SDK);
            this.LogError("_OnClickLogger", "log color DARK_NETWORK", ELogColor.DARK_NETWORK);
            this.LogWarning("_OnClickLogger", "log color NETWORK", ELogColor.NETWORK);
            MyLogger.Error(typeof(MainMenuScene).Name, "_OnClickLogger", "log color DARK_UI", ELogColor.DARK_UI);
            MyLogger.Warning(typeof(MainMenuScene).Name, "_OnClickLogger", "log color UI", ELogColor.UI);
            MyLogger.Error(typeof(MainMenuScene).Name, "_OnClickLogger", "log color DARK_GAMEPLAY", ELogColor.DARK_GAMEPLAY);
            MyLogger.Warning(typeof(MainMenuScene).Name, "_OnClickLogger", "log color GAMEPLAY", ELogColor.GAMEPLAY);
        }

        private void _OnClickCoroutine(PointerEventData arg0)
        {
            this.LogInfo("_OnClickCoroutine", null, ELogColor.UI);

            MyCoroutiner.ExecuteAfterDelayFrame("DelayFrame", 2000, () =>
            {
                this.LogInfo("_OnClickCoroutine", "callback after 2000 frames delay");
            });
            MyCoroutiner.ExecuteAfterDelayTime("DelaySecond", 1.5f, () =>
            {
                this.LogInfo("_OnClickCoroutine", "callback after 1.5 second delay");
            });
            MyCoroutiner.ExecuteAfterEndOfFrame(() =>
            {
                this.LogInfo("_OnClickCoroutine", "callback after frame ends");
            });
            MyCoroutiner.ExecuteFrameByFrame("FrameByFrame", 5, (frame) =>
            {
                this.LogInfo("_OnClickCoroutine", "callback frame by frame (" + frame + ")");
            });
        }

        private void _OnClickPool(PointerEventData arg0)
        {
            this.LogInfo("_OnClickPool", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.PoolPopup);
        }

        private void _OnClickLocalization(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLocalization", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.LocalizationPopup);
        }

        private void _OnClickAdMob(PointerEventData arg0)
        {
            this.LogInfo("_OnClickAdMob", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.AdMobPopup);
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}