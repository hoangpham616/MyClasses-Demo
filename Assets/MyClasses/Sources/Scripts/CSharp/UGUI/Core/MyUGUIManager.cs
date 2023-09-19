/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIManager (version 2.43)
 */

#pragma warning disable 0108
#pragma warning disable 0429

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_URP || USE_MY_URP
using UnityEngine.Rendering.Universal;
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

        public GameObject CanvasOnTopFloatPopup
        {
            get { return _canvasOnTopFloatPopup; }
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

        public MyUGUIPopup CurrentFloatPopup
        {
            get { return (MyUGUIPopup)_currentFloatPopup; }
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

#if UNITY_URP || USE_MY_URP
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
        /// Set asset bundle for core UI (scene fading, popup overlay, toast, running text, loading indicator).
        /// </summary>
        public void SetAssetBundleForCore(string url, int version)
        {
            _SetAssetBundleForCore(url, version);
        }

        /// <summary>
        /// Set asset bundle for HUDs.
        /// </summary>
        public void SetAssetBundleForHUDs(string url, int version)
        {
            _SetAssetBundleForHUDs(url, version);
        }

        /// <summary>
        /// Set asset bundle for scene.
        /// </summary>
        public void SetAssetBundleForScene(ESceneID sceneID, string url, int version)
        {
            _SetAssetBundleForScene((int)sceneID, url, version);
        }

        /// <summary>
        /// Set asset bundle for popup.
        /// </summary>
        public void SetAssetBundleForPopup(EPopupID popupID, string url, int version)
        {
            _SetAssetBundleForPopup((int)popupID, url, version);
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
            _ShowUnityScene((int)unitySceneID, (int)sceneID);
        }

        /// <summary>
        /// Return a scene which showed before in current Unity scene.
        /// </summary>
        public MyUGUIScene GetScene(ESceneID sceneID)
        {
            return (MyUGUIScene)_GetScene((int)sceneID);
        }

        /// <summary>
        /// Show a scene.
        /// </summary>
        public void ShowScene(ESceneID sceneID, bool isHideRunningMessageWhenSwitchingScene = false, bool isHideToastWhenSwitchingScene = true, Action onPreEnterCallback = null, Action onPostEnterCallback = null, Action onPostVisibleCallback = null)
        {
            _ShowScene((int)sceneID, isHideRunningMessageWhenSwitchingScene, isHideToastWhenSwitchingScene, onPreEnterCallback, onPostEnterCallback, onPostVisibleCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, onCloseCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Show a popup.
        /// </summary>
        public MyUGUIPopup ShowPopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowPopup((int)popupID, attachedData, onCloseCallback);
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
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData, onCloseCallback);
        }

        /// <summary>
        /// Show a repeatable popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatablePopup(EPopupID popupID, object attachedData, Action<MyUGUIPopupBase> onEnterCallback, Action<MyUGUIPopupBase> onCloseCallback)
        {
            return (MyUGUIPopup)_ShowRepeatablePopup((int)popupID, attachedData, onEnterCallback, onCloseCallback);
        }

        /// <summary>
        /// Show a float popup.
        /// </summary>
        public MyUGUIPopup ShowFloatPopup(EPopupID popupID, object attachedData = null)
        {
            return (MyUGUIPopup)_ShowFloatPopup((int)popupID, attachedData);
        }

        /// <summary>
        /// Show a repeatable float popup.
        /// </summary>
        public MyUGUIPopup ShowRepeatableFloatPopup(EPopupID popupID, object attachedData = null)
        {
            return (MyUGUIPopup)_ShowRepeatableFloatPopup((int)popupID, attachedData);
        }

        /// <summary>
        /// Hide current popup.
        /// </summary>
        public void HideCurrentPopup()
        {
            _HideCurrentPopup();
        }

        /// <summary>
        /// Hide current float popup.
        /// </summary>
        public void HideCurrentFloatPopup()
        {
            _HideCurrentFloatPopup();
        }

        /// <summary>
        /// Hide all popups.
        /// </summary>
        public void HideAllPopups(bool isHidePopup = true, bool isHideFloatPopup = true)
        {
            _HideAllPopups(isHidePopup, isHideFloatPopup);
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

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Create HUD according to config.
        /// </summary>
        protected override void _CreateHUD(ref MyUGUIUnitySceneBase unitySceneBase, MyUGUIConfigUnityScene unitySceneConfig)
        {
            MyUGUIHUDBase hud = (MyUGUIHUDBase)Activator.CreateInstance(MyUtilities.FindTypesByName(unitySceneConfig.HUDScriptName)[0], unitySceneConfig.HUDPrefabNameCanvas);
            hud.SetPrefabName3D(unitySceneConfig.HUDPrefabName3D);
            unitySceneBase.SetHUD((MyUGUIHUD)hud);
        }

        /// <summary>
        /// Create scene according to config.
        /// </summary>
        protected override void _CreateScene(ref MyUGUIUnitySceneBase unitySceneBase, MyUGUIConfigScene sceneConfig)
        {
            MyUGUIScene scene = (MyUGUIScene)Activator.CreateInstance(MyUtilities.FindTypesByName(sceneConfig.ScriptName)[0], (ESceneID)sceneConfig.ID, sceneConfig.PrefabNameCanvas, sceneConfig.IsInitWhenLoadUnityScene, sceneConfig.IsHideHUD, sceneConfig.FadeInDuration, sceneConfig.FadeOutDuration);
            scene.SetPrefabName3D(sceneConfig.PrefabName3D);
            unitySceneBase.AddScene((MyUGUISceneBase)scene);
        }

        /// <summary>
        /// Create popup according to config.
        /// </summary>
        protected override void _CreatePopup(ref MyUGUIPopupBase popupBase, MyUGUIConfigPopup popupConfig, bool isRepeatable)
        {
            popupBase = (MyUGUIPopupBase)Activator.CreateInstance(MyUtilities.FindTypesByName(popupConfig.ScriptName)[0], (EPopupID)popupConfig.ID, popupConfig.PrefabNameCanvas, false, isRepeatable);
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
        /// Return the name of popup.
        /// </summary>
        protected override string _GetPopupName(int id)
        {
            return ((EPopupID)id).ToString();
        }

        #endregion
    }
}