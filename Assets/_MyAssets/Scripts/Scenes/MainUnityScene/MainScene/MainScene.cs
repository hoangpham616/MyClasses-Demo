using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using MyClasses;
using MyClasses.UI;

namespace MyApp.UI
{
    public class MainScene : MyUGUIScene
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonButton;
        private MyUGUIToggleButton _toggleToggle;
        private MyUGUIButton _buttonRunningMessage;
        private MyUGUIButton _buttonFlyingMessage;
        private MyUGUIButton _buttonToastMessage;
        private MyUGUIButton _buttonLoadingIndicator;
        private MyUGUIButton _buttonToastNotification;
        private MyUGUIButton _buttonDialog2Buttons;
        private MyUGUIButton _buttonScrollSnap;
        private MyUGUIButton _buttonReuasbleListView;
        private MyUGUIButton _buttonReuasbleListView2;
        private MyUGUIButton _buttonExtension;
        private MyUGUIButton _buttonGroupScene;
        private MyUGUIButton _buttonGameScene;
        private MyUGUIButton _buttonLogger;
        private MyUGUIButton _buttonCoroutine;
        private MyUGUIButton _buttonPool;
        private MyUGUIButton _buttonLocalization;
        private MyUGUIButton _buttonForbiddenText;
        private MyUGUIButton _buttonAdMob;

        #endregion

        #region ----- Constructor -----

        public MainScene(MyUGUIConfigScene config, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback)
            : base(config, onSubSceneSwitchCallback)
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
            _buttonToastNotification = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonToastNotification").GetComponent<MyUGUIButton>();
            _buttonDialog2Buttons = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonDialog2Buttons").GetComponent<MyUGUIButton>();
            _buttonScrollSnap = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonScrollSnap").GetComponent<MyUGUIButton>();
            _buttonReuasbleListView = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonReusableListView").GetComponent<MyUGUIButton>();
            _buttonReuasbleListView2 = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonReusableListView2").GetComponent<MyUGUIButton>();
            _buttonExtension = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonExtension").GetComponent<MyUGUIButton>();
            _buttonGroupScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonGroupScene").GetComponent<MyUGUIButton>();
            _buttonGameScene = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonGameScene").GetComponent<MyUGUIButton>();
            _buttonLogger = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonLogger").GetComponent<MyUGUIButton>();
            _buttonCoroutine = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonCoroutine").GetComponent<MyUGUIButton>();
            _buttonPool = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonPool").GetComponent<MyUGUIButton>();
            _buttonLocalization = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonLocalization").GetComponent<MyUGUIButton>();
            _buttonForbiddenText = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonForbiddenText").GetComponent<MyUGUIButton>();
            _buttonAdMob = MyUtilities.FindObject(GameObjectCanvas, "Buttons/ButtonAdMob").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "SceneId=" + MyUGUIManager.Instance.CurrentScene.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonButton.OnEventPointerClick.AddListener(_OnClickButton);
            _toggleToggle.OnValueChange.AddListener(_OnToggleValueChange);
            _buttonRunningMessage.OnEventPointerDoubleClick.AddListener(_OnClickRunningMessage);
            _buttonFlyingMessage.OnEventPointerClick.AddListener(_OnClickFlyingMessage);
            _buttonToastMessage.OnEventPointerClick.AddListener(_OnClickToastMessage);
            _buttonLoadingIndicator.OnEventPointerClick.AddListener(_OnClickLoadingIndicator);
            _buttonToastNotification.OnEventPointerClick.AddListener(_OnClickToastNotification);
            _buttonDialog2Buttons.OnEventPointerClick.AddListener(_OnClickDialog2Buttons);
            _buttonScrollSnap.OnEventPointerClick.AddListener(_OnClickScrollSnap);
            _buttonReuasbleListView.OnEventPointerClick.AddListener(_OnClickReuasbleListView);
            _buttonReuasbleListView2.OnEventPointerClick.AddListener(_OnClickReuasbleListView2);
            _buttonExtension.OnEventPointerClick.AddListener(_OnClickExtension);
            _buttonGroupScene.OnEventPointerClick.AddListener(_OnClickGroupScene);
            _buttonGameScene.OnEventPointerClick.AddListener(_OnClickGameScene);
            _buttonLogger.OnEventPointerClick.AddListener(_OnClickLogger);
            _buttonCoroutine.OnEventPointerClick.AddListener(_OnClickCoroutine);
            _buttonPool.OnEventPointerClick.AddListener(_OnClickPool);
            _buttonLocalization.OnEventPointerClick.AddListener(_OnClickLocalization);
            _buttonForbiddenText.OnEventPointerClick.AddListener(_OnClickForbiddenText);
            _buttonAdMob.OnEventPointerClick.AddListener(_OnClickAdMob);
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
            _buttonToastNotification.OnEventPointerClick.RemoveAllListeners();
            _buttonDialog2Buttons.OnEventPointerClick.RemoveAllListeners();
            _buttonScrollSnap.OnEventPointerClick.RemoveAllListeners();
            _buttonReuasbleListView.OnEventPointerClick.RemoveAllListeners();
            _buttonReuasbleListView2.OnEventPointerClick.RemoveAllListeners();
            _buttonExtension.OnEventPointerClick.RemoveAllListeners();
            _buttonGroupScene.OnEventPointerClick.RemoveAllListeners();
            _buttonGameScene.OnEventPointerClick.RemoveAllListeners();
            _buttonLogger.OnEventPointerClick.RemoveAllListeners();
            _buttonCoroutine.OnEventPointerClick.RemoveAllListeners();
            _buttonPool.OnEventPointerClick.RemoveAllListeners();
            _buttonLocalization.OnEventPointerClick.RemoveAllListeners();
            _buttonForbiddenText.OnEventPointerClick.RemoveAllListeners();
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

        private void _OnClickToastNotification(PointerEventData arg0)
        {
            this.LogInfo("_OnClickToastNotification", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowToastNotification(EToastNotificationID.TopCenterToastNotification, UnityEngine.Random.Range(1, 10000));
        }

        private void _OnClickDialog2Buttons(PointerEventData arg0)
        {
            MyUGUIManager.Instance.IsClosePopupByClickingOutside = true;
            this.LogInfo("_OnClickDialog2Buttons", null, ELogColor.UI);

            MyUGUIPopup2Buttons popup2 = (MyUGUIPopup2Buttons)MyUGUIManager.Instance.ShowPopup(EPopupID.Dialog2ButtonsPopup);
            popup2.SetData("2 Buttons Popup\n(TITLE)", "Do you want to open another popup?\n(BODY)", "No\n(LEFT)", (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Left Button", ELogColor.UI);
            }, "Yes\n(RIGHT)", (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Right Button", ELogColor.UI);

                MyUGUIPopup1Button popup1 = (MyUGUIPopup1Button)MyUGUIManager.Instance.ShowPopup(EPopupID.Dialog1ButtonPopup);
                popup1.SetData("1 Button Popup\n(TITLE)", "Click to close\n(BODY)", "Close\n(MAIN)", (data) =>
                {
                    this.LogInfo("_OnClickDialog1Button", "Click Main Button", ELogColor.UI);
                });
            }, (data) =>
            {
                this.LogInfo("_OnClickDialog2Buttons", "Click Close Button", ELogColor.UI);
            }, false);
        }

        private void _OnClickScrollSnap(PointerEventData arg0)
        {
            this.LogInfo("_OnClickScrollSnap", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.ScrollSnapPopup);
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

        private void _OnClickGroupScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickGroupScene", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowScene(ESceneID.GroupScene, ESubSceneID.GroupCSubScene);
        }

        private void _OnClickGameScene(PointerEventData arg0)
        {
            this.LogInfo("_OnClickGameScene", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowScene(ESceneID.GameScene, ESceneTransition.FadeIn, 0.4f);
        }

        private void _OnClickLogger(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLogger", null, ELogColor.UI);

            this.LogInfo("_OnClickLogger", "log color DEFAULT");
            this.LogError("_OnClickLogger", "log color DARK_CORE", ELogColor.DARK_SDK);
            this.LogWarning("_OnClickLogger", "log color CORE", ELogColor.SDK);
            this.LogError("_OnClickLogger", "log color DARK_NETWORK", ELogColor.DARK_NETWORK);
            this.LogWarning("_OnClickLogger", "log color NETWORK", ELogColor.NETWORK);
            MyLogger.Error(typeof(MainScene).Name, "_OnClickLogger", "log color DARK_UI", ELogColor.DARK_UI);
            MyLogger.Warning(typeof(MainScene).Name, "_OnClickLogger", "log color UI", ELogColor.UI);
            MyLogger.Error(typeof(MainScene).Name, "_OnClickLogger", "log color DARK_GAMEPLAY", ELogColor.DARK_GAMEPLAY);
            MyLogger.Warning(typeof(MainScene).Name, "_OnClickLogger", "log color GAMEPLAY", ELogColor.GAMEPLAY);
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
                this.LogInfo("_OnClickCoroutine", "stop all coroutines starting with 'ListCoroutine_'");

                MyCoroutiner.StopPrefix("ListCoroutine_");
            });
            MyCoroutiner.ExecuteAfterEndOfFrame(() =>
            {
                this.LogInfo("_OnClickCoroutine", "callback after frame ends");
            });
            MyCoroutiner.ExecuteFrameByFrame("FrameByFrame", 5, (frame) =>
            {
                this.LogInfo("_OnClickCoroutine", "callback frame by frame (" + frame + ")");
            });
            MyCoroutiner.StartWithoutReplace("ListCoroutine_1", _TestCoroutine());
            MyCoroutiner.StartWithoutReplace("ListCoroutine_1", _TestCoroutine());
            MyCoroutiner.Start("ListCoroutine_2", _TestCoroutine());
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

        private void _OnClickForbiddenText(PointerEventData arg0)
        {
            this.LogInfo("_OnClickForbiddenText", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowPopup(EPopupID.ForbiddenTextPopup);
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

        private IEnumerator _TestCoroutine()
        {
            for (int i = 0; i < 100; ++i)
            {
                yield return new WaitForSeconds(0.3f);

                MyLogger.Info(typeof(MainScene).Name, "_TestCoroutine", (i * 0.3f).ToString());
            }
        }

        #endregion
    }
}