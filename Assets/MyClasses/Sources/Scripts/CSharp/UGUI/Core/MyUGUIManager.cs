/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIManager (version 2.50)
 */

#pragma warning disable 0108
#pragma warning disable 0429

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_URP || MY_UI_URP
using UnityEngine.Rendering.Universal;
#endif
#if MY_UI_ADDRESSABLE
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

namespace MyClasses.UI
{
    public class MyUGUIManager : MyUGUIManagerBase
    {
        #region ----- Property -----

        public Camera Camera
        {
            get { return _cameraUI; }
        }

        public Canvas Canvas
        {
            get { return _canvas; }
        }

        public Canvas CanvasOnTop
        {
            get { return _canvasOnTop; }
        }

        public Vector2 DesignResolution
        {
            get
            {
                if (_canvasScaler == null)
                {
                    _canvasScaler = _canvas.GetComponent<CanvasScaler>();
                }
                return _canvasScaler.referenceResolution;
            }
        }

        public GameObject CanvasOnTopHUD
        {
            get { return _canvasOnTopHUD; }
        }

        public GameObject CanvasOnTopPopup
        {
            get { return _canvasOnTopPopup; }
        }

        public MyUGUIUnityScene CurrentUnityScene
        {
            get { return (MyUGUIUnityScene)_currentUnityScene; }
        }

        public MyUGUIHUD CurrentHUD
        {
            get { return (MyUGUIHUD)_currentUnityScene.HUD; }
        }

        public MyUGUIScene CurrentScene
        {
            get { return (MyUGUIScene)_currentScene; }
        }

        public MyUGUIScene PreviousScene
        {
            get { return (MyUGUIScene)_previousScene; }
        }

        public MyUGUISceneFading CurrentSceneFading
        {
            get { return _currentSceneFading; }
        }

        public MyUGUIPopup CurrentPopup
        {
            get { return (MyUGUIPopup)_currentPopup; }
        }

        public MyUGUILoadingIndicator CurrentLoadingIndicator
        {
            get { return _currentLoadingIndicator; }
        }

        public bool IsShowingLoadingIndicator
        {
            get { return _currentLoadingIndicator != null && _currentLoadingIndicator.IsActive; }
        }

        public bool IsClosePopupByClickingOutside
        {
            get { return _isClosePopupByClickingOutside; }
            set { _isClosePopupByClickingOutside = value; }
        }

        #endregion

        #region ----- Singleton -----

        private static object _singletonLock = new object();
        private static MyUGUIManager _instance;

        public static MyUGUIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_singletonLock)
                    {
                        _instance = (MyUGUIManager)FindObjectOfType(typeof(MyUGUIManager));
                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(MyUGUIManager).Name);
                            _instance = obj.AddComponent<MyUGUIManager>();
                            DontDestroyOnLoad(obj);
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region ----- Public Method -----

#if UNITY_URP || MY_UI_URP
        /// <summary>
        /// Add camera UI to the stack of 3D camera.
        /// </summary>
        public void AddCamera(Camera camera3D)
        {
            var camaraData = camera3D.GetUniversalAdditionalCameraData();
            bool isAdded = false;
            for (int i = 0, count = camaraData.cameraStack.Count; i < count; i++)
            {
                if (camaraData.cameraStack[i] == Camera)
                {
                    isAdded = true;
                    break;
                }
            }
            if (isAdded == false)
            {
                camaraData.cameraStack.Add(Camera);
            }
        }
#endif

        /// <summary>
        /// Check if UI is touched.
        /// </summary>
        public bool IsPointerOverUIGameObject()
        {
            return _IsPointerOverUIGameObject();
        }

        /// <summary>
        /// Back to previous scene.
        /// </summary>
        public bool Back()
        {
            return base.Back();
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        /// <param name="unitySceneID">Empty: without scene</param>
        public void ShowUnityScene(EUnitySceneID unitySceneID, ESceneID sceneID)
        {
            _ShowUnityScene((int)unitySceneID, (int)sceneID, -1);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        /// <param name="unitySceneID">Empty: without scene</param>
        public void ShowUnityScene(EUnitySceneID unitySceneID, ESceneID sceneID, ESubSceneID subSceneID)
        {
            _ShowUnityScene((int)unitySceneID, (int)sceneID, (int)subSceneID);
        }

        /// <summary>
        /// Return a scene which showed before in current Unity scene.
        /// </summary>
        public MyUGUIScene GetScene(ESceneID sceneID)
        {
            return (MyUGUIScene)_GetScene((int)sceneID);
        }

        /// <summary>
        /// Preload a scene.
        /// </summary>
        public void PreloadScene(ESceneID sceneID, Action<bool> onCompleteCallback = null)
        {
            _PreloadScene((int)sceneID, onCompleteCallback);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, Action<MyUGUISceneBase> onPreEnterCallback = null, Action<MyUGUISceneBase> onPostEnterCallback = null, Action<MyUGUISceneBase> onPostVisibleCallback = null)
        {
            _ShowScene((int)sceneID, -1, ESceneTransition.None, 0, false, false, false, onPreEnterCallback, onPostEnterCallback, onPostVisibleCallback, null);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, ESceneTransition transition, float transitionDuration = 0.2f, bool isHideRunningMessageWhenSwitchingScene = false, bool isHideToastMessageWhenSwitchingScene = false, bool isHideToastNotificationWhenSwitchingScene = false, Action<MyUGUISceneBase> onPreEnterCallback = null, Action<MyUGUISceneBase> onPostEnterCallback = null, Action<MyUGUISceneBase> onPostVisibleCallback = null)
        {
            _ShowScene((int)sceneID, -1, transition, transitionDuration, isHideRunningMessageWhenSwitchingScene, isHideToastMessageWhenSwitchingScene, isHideToastNotificationWhenSwitchingScene, onPreEnterCallback, onPostEnterCallback, onPostVisibleCallback, null);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, ESubSceneID subSceneID, Action<MyUGUISceneBase> onPreEnterCallback = null, Action<MyUGUISceneBase> onPostEnterCallback = null, Action<MyUGUISceneBase> onPostVisibleCallback = null)
        {
            _ShowScene((int)sceneID, (int)subSceneID, ESceneTransition.None, 0, false, false, false, onPreEnterCallback, onPostEnterCallback, onPostVisibleCallback, null);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, ESubSceneID subSceneID, Action<MyUGUISubSceneBase> onSubSceneEnterCallback)
        {
            _ShowScene((int)sceneID, (int)subSceneID, ESceneTransition.None, 0, false, false, false, null, null, null, onSubSceneEnterCallback);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, ESubSceneID subSceneID, ESceneTransition transition, float transitionDuration = 0.2f, bool isHideRunningMessageWhenSwitchingScene = false, bool isHideToastMessageWhenSwitchingScene = false, bool isHideToastNotificationWhenSwitchingScene = false, Action<MyUGUISceneBase> onPreEnterCallback = null, Action<MyUGUISceneBase> onPostEnterCallback = null, Action<MyUGUISceneBase> onPostVisibleCallback = null)
        {
            _ShowScene((int)sceneID, (int)subSceneID, transition, transitionDuration, isHideRunningMessageWhenSwitchingScene, isHideToastMessageWhenSwitchingScene, isHideToastNotificationWhenSwitchingScene, onPreEnterCallback, onPostEnterCallback, onPostVisibleCallback, null);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, ESubSceneID subSceneID, ESceneTransition transition, float transitionDuration, bool isHideRunningMessageWhenSwitchingScene, bool isHideToastMessageWhenSwitchingScene, bool isHideToastNotificationWhenSwitchingScene, Action<MyUGUISubSceneBase> onSubSceneEnterCallback)
        {
            _ShowScene((int)sceneID, (int)subSceneID, transition, transitionDuration, isHideRunningMessageWhenSwitchingScene, isHideToastMessageWhenSwitchingScene, isHideToastNotificationWhenSwitchingScene, null, null, null, onSubSceneEnterCallback);
        }

        /// <summary>
        /// Return an active popup.
        /// </summary>
        public MyUGUIPopup GetActivePopup(EPopupID popupID)
        {
            return (MyUGUIPopup)_GetActivePopup((int)popupID);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, null, null, null);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, null, null, onCloseCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, null, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, attachedData, null, onCloseCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, attachedData, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, null, null, null);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, null, null, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, null, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData, null, null);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData, null, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Hide current popup.
        /// </summary>
        public void HideCurrentPopup()
        {
            _HideCurrentPopup();
        }

        /// <summary>
        /// Hide all popups.
        /// </summary>
        public void HideAllPopups(bool isHide = true)
        {
            _HideAllPopups(isHide);
        }

        /// <summary>
        /// Show a toast notification.
        /// </summary>
        public void ShowToastNotification(EToastNotificationID toastNotificationID, object attachedData, Action<MyUGUIToastNotificationBase> onEnterCallback = null, Action<MyUGUIToastNotificationBase> onCloseCallback = null)
        {
            _QueueOrShowToastNotification((int)toastNotificationID, attachedData, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Hide toast notifications by id.
        /// </summary>
        public void HideToastNotifications(EToastNotificationID toastNotificationID)
        {
            _HideToastNotifications((int)toastNotificationID);
        }

        /// <summary>
        /// Hide all toast notifications.
        /// </summary>
        public void HideAllToastNotifications()
        {
            _HideAllToastNotifications();
        }

        /// <summary>
        /// Hide current tips loading indicator, show simple loading indicator and return loading id.
        /// </summary>
        public int ShowLoadingIndicator()
        {
            return _ShowLoadingIndicator();
        }

        /// <summary>
        /// Hide current tips loading indicator, show simple loading indicator and return loading id.
        /// </summary>
        public int ShowLoadingIndicator(float timeout, Action timeoutCallback = null)
        {
            return _ShowLoadingIndicator(timeout, timeoutCallback);
        }

        /// <summary>
        /// Hide current simple loading indicator and show tips loading indicator.
        /// </summary>
        public void ShowLoadingIndicatorWithTips(string tips, string description, bool isThreeDots, Action cancelCallback = null)
        {
            _ShowLoadingIndicatorWithTips(tips, description, isThreeDots, cancelCallback);
        }

        /// <summary>
        /// Hide current simple loading indicator and show tips loading indicator.
        /// </summary>
        public void ShowLoadingIndicatorWithTips(string tips, string description, bool isThreeDots, float timeout, Action timeoutCallback = null, Action cancelCallback = null)
        {
            _ShowLoadingIndicatorWithTips(tips, description, isThreeDots, timeout, timeoutCallback, cancelCallback);
        }

        /// <summary>
        /// Hide loading indicator.
        /// </summary>
        /// <param name="minLiveTime">minimum seconds have to show before hiding</param>
        public void HideLoadingIndicator(float minLiveTime = 0.15f)
        {
            _HideLoadingIndicator(minLiveTime);
        }

        /// <summary>
        /// Hide loading indicator by loading id.
        /// </summary>
        /// <param name="minLiveTime">minimum seconds have to show before hiding</param>
        public void HideLoadingIndicator(int loadingID, float minLiveTime = 0.15f)
        {
            _HideLoadingIndicator(loadingID, minLiveTime);
        }

        /// <summary>
        /// Show flying message.
        /// </summary>
        public void ShowFlyingMessage(string content, MyUGUIFlyingMessage.EType type = MyUGUIFlyingMessage.EType.ShortFlyFromMid)
        {
            _ShowFlyingMessage(content, type);
        }

        /// <summary>
        /// Set queue limit for running message.
        /// </summary>
        public void SetRunningMessageMaxQueue(MyUGUIRunningMessage.EType type = MyUGUIRunningMessage.EType.Default, int maxQueue = -1)
        {
            _SetRunningMessageMaxQueue(type, maxQueue);
        }

        /// <summary>
        /// Show running message.
        /// </summary>
        public void ShowRunningMessage(string content, ERunningMessageSpeed speed = ERunningMessageSpeed.Normal, MyUGUIRunningMessage.EType type = MyUGUIRunningMessage.EType.Default)
        {
            _ShowRunningMessage(content, speed, type);
        }

        /// <summary>
        /// Hide running message.
        /// </summary>
        public void HideRunningMessage()
        {
            _HideRunningMessage();
        }

        /// <summary>
        /// Show toast message.
        /// </summary>
        public void ShowToastMessage(string content, EToastMessageDuration duration = EToastMessageDuration.Medium)
        {
            _ShowToastMessage(content, duration);
        }

        /// <summary>
        /// Show toast message.
        /// </summary>
        public void ShowToastMessage(string content, float duration)
        {
            _ShowToastMessage(content, duration);
        }

        /// <summary>
        /// Hide toast.
        /// </summary>
        public void HideToastMessage()
        {
            _HideToastMessage();
        }

        /// <summary>
        /// Convert screen point to world point.
        /// </summary>
        public Vector3 ScreenToWorldPoint(Vector3 screenPoint)
        {
            return _ScreenToWorldPoint(screenPoint);
        }

        /// <summary>
        /// Set alpha for a TextMeshProUGUI.
        /// </summary>
        public void SetAlpha(ref TextMeshProUGUI textMeshPro, float alpha)
        {
            Color color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }

        /// <summary>
        /// Set color for a TextMeshProUGUI.
        /// </summary>
        public void SetColor(ref TextMeshProUGUI textMeshPro, Color color)
        {
            textMeshPro.color = color;
        }

        /// <summary>
        /// Load a prefab from addressable.
        /// </summary>
        public override GameObject LoadAddressable(string addressable)
        {
#if MY_UI_ADDRESSABLE
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressable);
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
#else
            Debug.LogWarning("[" + typeof(MyUGUIManager).Name + "] LoadAddressable(): You need to add compile flag 'MY_UI_ADDRESSABLE' to use Addressable for MyUGUI.");
            Debug.LogError("[" + typeof(MyUGUIManager).Name + "] LoadAddressable(): You need to add compile flag 'MY_UI_ADDRESSABLE' to use Addressable for MyUGUI.");
            Debug.Break();
#endif
            return null;
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Create HUD according to config.
        /// </summary>
        protected override void _CreateHUD(ref MyUGUIUnitySceneBase unitySceneBase, MyUGUIConfigUnityScene unitySceneConfig)
        {
            MyUGUIHUDBase hud = (MyUGUIHUDBase)Activator.CreateInstance(MyUtilities.FindTypesByName(unitySceneConfig.HUDScriptName)[0], unitySceneConfig.HUDPrefabNameCanvas, unitySceneConfig.HUDPrefabName3D);
            unitySceneBase.SetHUD(hud);
        }

        /// <summary>
        /// Create scene according to config.
        /// </summary>
        protected override void _CreateScene(ref MyUGUIUnitySceneBase unitySceneBase, MyUGUIConfigScene sceneConfig, Action<MyUGUISceneBase, MyUGUISubSceneBase> onSubSceneSwitchCallback)
        {
            MyUGUIScene scene = (MyUGUIScene)Activator.CreateInstance(MyUtilities.FindTypesByName(sceneConfig.ScriptName)[0], sceneConfig, onSubSceneSwitchCallback);
            unitySceneBase.AddScene(scene);
        }

        /// <summary>
        /// Create popup according to config.
        /// </summary>
        protected override void _CreatePopup(ref MyUGUIPopupBase popupBase, MyUGUIConfigPopup popupConfig, bool isRepeatable)
        {
            popupBase = (MyUGUIPopup)Activator.CreateInstance(MyUtilities.FindTypesByName(popupConfig.ScriptName)[0], (EPopupID)popupConfig.ID, popupConfig.PrefabNameCanvas, popupConfig.PrefabName3D, popupConfig.AddressableCanvas, popupConfig.Addressable3D, isRepeatable);
        }

        /// <summary>
        /// Create toast notification according to config.
        /// </summary>
        protected override void _CreateToastNotification(ref MyUGUIToastNotificationBase toastNotificationBase, MyUGUIConfigToastNotification toastNotificationConfig)
        {
            toastNotificationBase = (MyUGUIToastNotificationBase)Activator.CreateInstance(MyUtilities.FindTypesByName(toastNotificationConfig.ScriptName)[0], (EToastNotificationID)toastNotificationConfig.ID, toastNotificationConfig.PrefabNameCanvas, toastNotificationConfig.PrefabName3D);
        }

        /// <summary>
        /// Return the name of unity scene.
        /// </summary>
        protected override string _GetUnitySceneName(int id)
        {
            return ((EUnitySceneID)id).ToString();
        }

        /// <summary>
        /// Return the name of scene.
        /// </summary>
        protected override string _GetSceneName(int id)
        {
            return ((ESceneID)id).ToString();
        }

        /// <summary>
        /// Return the name of sub scene.
        /// </summary>
        protected override string _GetSubSceneName(int id)
        {
            string name = ((ESubSceneID)id).ToString();
            return name.Equals("-1") ? "None" : name;
        }

        /// <summary>
        /// Return the name of popup.
        /// </summary>
        protected override string _GetPopupName(int id)
        {
            return ((EPopupID)id).ToString();
        }

        /// <summary>
        /// Return the name of toast notification.
        /// </summary>
        protected override string _GetToastNotificationName(int id)
        {
            return ((EToastNotificationID)id).ToString();
        }

        #endregion
    }
}