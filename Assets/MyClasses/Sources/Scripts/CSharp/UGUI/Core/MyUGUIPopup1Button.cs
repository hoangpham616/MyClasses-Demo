﻿/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIPopup1Button (version 2.20)
 */

#pragma warning disable 0414
#pragma warning disable 0618
#pragma warning disable 0649

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace MyClasses.UI
{
    public class MyUGUIPopup1Button : MyUGUIPopup
    {
        #region ----- Variable -----

        private TextMeshProUGUI _textTitleTMPro;
        private Text _textTitle;
        private TextMeshProUGUI _textBodyTMPro;
        private Text _textBody;
        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonMain;
        private Action<object> _onClickCloseCallback;
        private Action<object> _onClickMainCallback;

        private bool _isAutoHideWhenClickButton;

        #endregion

        #region ----- Constructor -----

        /// <summary>
        /// Constructor.
        /// </summary>
        public MyUGUIPopup1Button(MyUGUIConfigPopup config, bool isRepeatable = false)
            : base(config, isRepeatable)
        {
#if UNITY_EDITOR
            if (!_CheckPrefab())
            {
                _CreatePrefab();
            }
#endif
        }

        #endregion

        #region ----- MyUGUIPopup Implementation -----

        /// <summary>
        /// OnUGUIInit.
        /// </summary>
        public override void OnUGUIInit()
        {
            base.OnUGUIInit();

            GameObject container = MyUtilities.FindObjectInAllLayers(GameObjectCanvas, "Container");
            _buttonMain = MyUtilities.FindObjectInAllLayers(container, "ButtonMain").GetComponent<MyUGUIButton>();

            GameObject title = MyUtilities.FindObject(container, "Title");
            if (title != null)
            {
                _textTitleTMPro = title.GetComponent<TextMeshProUGUI>();
                if (_textTitleTMPro == null)
                {
                    _textTitle = title.GetComponent<Text>();
                }
            }

            GameObject body = MyUtilities.FindObject(container, "Body");
            if (body != null)
            {
                _textBodyTMPro = body.GetComponent<TextMeshProUGUI>();
                if (_textBodyTMPro == null)
                {
                    _textBody = body.GetComponent<Text>();
                }
            }

            GameObject close = MyUtilities.FindObjectInAllLayers(container, "ButtonClose");
            if (close != null)
            {
                _buttonClose = close.GetComponent<MyUGUIButton>();
            }
        }

        /// <summary>
        /// OnEnter.
        /// </summary>
        public override void OnUGUIEnter()
        {
            base.OnUGUIEnter();

            if (_buttonClose != null)
            {
                _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            }
            _buttonMain.OnEventPointerClick.AddListener(_OnClickMain);
        }

        /// <summary>
        /// OnUGUIVisible.
        /// </summary>
        public override bool OnUGUIVisible()
        {
            return base.OnUGUIVisible();
        }

        /// <summary>
        /// OnUpdateUGUI.
        /// </summary>
        public override void OnUGUIUpdate(float deltaTime)
        {
            base.OnUGUIUpdate(deltaTime);
        }

        /// <summary>
        /// OnUGUIExit.
        /// </summary>
        public override void OnUGUIExit()
        {
            base.OnUGUIExit();

            if (_buttonClose != null)
            {
                _buttonClose.OnEventPointerClick.RemoveAllListeners();
            }
            _buttonMain.OnEventPointerClick.RemoveAllListeners();

            _onClickCloseCallback = null;
            _onClickMainCallback = null;
        }

        /// <summary>
        /// OnUGUIInvisible.
        /// </summary>
        public override bool OnUGUIInvisible()
        {
            return base.OnUGUIInvisible();
        }

        #endregion

        #region ----- Button Event -----

        /// <summary>
        /// Click on close button.
        /// </summary>
        private void _OnClickClose(PointerEventData arg0)
        {
            if (_onClickCloseCallback != null)
            {
                _onClickCloseCallback(AttachedData);
            }

            Hide();
        }

        /// <summary>
        /// Click on main button.
        /// </summary>
        private void _OnClickMain(PointerEventData arg0)
        {
            if (_onClickMainCallback != null)
            {
                _onClickMainCallback(AttachedData);
            }

            if (_isAutoHideWhenClickButton)
            {
                Hide();
            }
        }

        #endregion

        #region ----- Public Method -----

        /// <summary>
        /// Set data.
        /// </summary>
        public void SetData(string title, string content, string mainButton, Action<object> actionMain, Action<object> actionClose, bool isAutoHideWhenClickButton = true)
        {
            _SetData(title, content, mainButton, actionMain, true, actionClose, isAutoHideWhenClickButton);
        }

        /// <summary>
        /// Set data.
        /// </summary>
        public void SetData(string title, string content, string mainButton, Action<object> actionMain = null, bool isAutoHideWhenClickButton = true)
        {
            _SetData(title, content, mainButton, actionMain, false, null, isAutoHideWhenClickButton);
        }

        /// <summary>
        /// Set data.
        /// </summary>
        public void SetData(string content, string mainButton, Action<object> actionMain, Action<object> actionClose, bool isAutoHideWhenClickButton = true)
        {
            _SetData(string.Empty, content, mainButton, actionMain, true, actionClose, isAutoHideWhenClickButton);
        }

        /// <summary>
        /// Set data.
        /// </summary>
        public void SetData(string content, string buttonMain, Action<object> actionMain, bool isAutoHideWhenClickButton = true)
        {
            _SetData(string.Empty, content, buttonMain, actionMain, false, null, isAutoHideWhenClickButton);
        }

        /// <summary>
        /// Set data.
        /// </summary>
        public void SetData(string content, string buttonMain, bool isShowCloseButton = false, bool isAutoHideWhenClickButton = true)
        {
            _SetData(string.Empty, content, buttonMain, null, isShowCloseButton, null, isAutoHideWhenClickButton);
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Set data.
        /// </summary>
        private void _SetData(string title, string body, string buttonMain, Action<object> actionMain, bool isShowButtonClose, Action<object> actionClose, bool isAutoHideWhenClickButton)
        {
            if (_textTitleTMPro != null)
            {
                _textTitleTMPro.text = title;
            }
            else if (_textTitle != null)
            {
                _textTitle.text = title;
            }

            if (_textBodyTMPro != null)
            {
                _textBodyTMPro.text = body;
            }
            else if (_textBody != null)
            {
                _textBody.text = body;
            }

            if (_buttonClose != null)
            {
                _buttonClose.SetActive(isShowButtonClose);
            }
            _onClickCloseCallback = actionClose;

            _buttonMain.SetText(buttonMain);
            _onClickMainCallback = actionMain;

            _isAutoHideWhenClickButton = isAutoHideWhenClickButton;
        }

#if UNITY_EDITOR

        /// <summary>
        /// Check existence of prefab.
        /// </summary>
        private static bool _CheckPrefab(string subfixName = "")
        {
            string filePath = "Assets/Resources/" + MyUGUIManagerBase.POPUP_DIRECTORY + "Dialog1ButtonPopup" + subfixName + ".prefab";
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// Create template prefab.
        /// </summary>
        private static void _CreatePrefab(string subfixName = "")
        {
            string prefabName = "Dialog1ButtonPopup" + subfixName;

            GameObject prefab = new GameObject(prefabName);

            Animator root_animator = prefab.AddComponent<Animator>();
            string[] paths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < paths.Length; i++)
            {
                if (System.IO.File.Exists(paths[i] + "/Sources/Animations/MyAnimatorDialog.controller"))
                {
                    root_animator.runtimeAnimatorController = (RuntimeAnimatorController)UnityEditor.AssetDatabase.LoadAssetAtPath(paths[i] + "/Sources/Animations/MyAnimatorDialog.controller", typeof(RuntimeAnimatorController));
                    Debug.LogError("[" + typeof(MyUGUIPopup1Button).Name + "] CreateTemplate(): please setup \"MyAnimatorDialog\" controller.");
                    Debug.LogError("[" + typeof(MyUGUIPopup1Button).Name + "] CreateTemplate(): mapping \"MyAnimationDialogShow\" motion for \"Show\" state.");
                    Debug.LogError("[" + typeof(MyUGUIPopup1Button).Name + "] CreateTemplate(): mapping \"MyAnimationDialogHide\" motion for \"Hide\" state.");
                    break;
                }
            }
            root_animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            RectTransform root_rect = prefab.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref root_rect, MyUtilities.EAnchorPreset.DualStretch, MyUtilities.EAnchorPivot.MiddleCenter, Vector2.zero, Vector2.zero);

            GameObject container = new GameObject("Container");
            container.transform.SetParent(prefab.transform, false);

            RectTransform container_rect = container.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref container_rect, MyUtilities.EAnchorPreset.MiddleCenter, MyUtilities.EAnchorPivot.MiddleCenter, 1000, 600, 0, 0);

            Image container_image = container.AddComponent<Image>();
            container_image.color = new Color(0.9f, 0.9f, 0.9f);
            container_image.raycastTarget = true;

            GameObject title = new GameObject("Title");
            title.transform.SetParent(container.transform, false);

            RectTransform title_rect = title.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref title_rect, MyUtilities.EAnchorPreset.TopCenter, MyUtilities.EAnchorPivot.TopCenter, 800, 100, 0, 0);

            Text title_text = title.AddComponent<Text>();
            title_text.text = "TITLE";
            title_text.color = Color.black;
#if UNITY_2022_2_OR_NEWER
            title_text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
#else
            title_text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
#endif
            title_text.fontSize = 50;
            title_text.fontStyle = FontStyle.Bold;
            title_text.alignment = TextAnchor.MiddleCenter;
            title_text.horizontalOverflow = HorizontalWrapMode.Wrap;
            title_text.verticalOverflow = VerticalWrapMode.Truncate;
            title_text.raycastTarget = false;

            GameObject body = new GameObject("Body");
            body.transform.SetParent(container.transform, false);

            RectTransform body_rect = body.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref body_rect, MyUtilities.EAnchorPreset.DualStretch, MyUtilities.EAnchorPivot.MiddleCenter, new Vector2(100, 120), new Vector2(-100, -120));

            Text body_text = body.AddComponent<Text>();
            body_text.text = "Body";
            body_text.color = Color.black;
#if UNITY_2022_2_OR_NEWER
            body_text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
#else
            body_text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
#endif
            body_text.fontSize = 40;
            body_text.alignment = TextAnchor.MiddleCenter;
            body_text.horizontalOverflow = HorizontalWrapMode.Wrap;
            body_text.verticalOverflow = VerticalWrapMode.Truncate;
            body_text.raycastTarget = false;

            GameObject buttonClose = new GameObject("ButtonClose");
            buttonClose.transform.SetParent(container.transform, false);

            RectTransform buttonClose_rect = buttonClose.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref buttonClose_rect, MyUtilities.EAnchorPreset.TopRight, MyUtilities.EAnchorPivot.TopRight, 80, 80, -10, -10);

            Image buttonClose_image = buttonClose.AddComponent<Image>();
            buttonClose_image.color = Color.red;
            buttonClose_image.raycastTarget = true;

            Button buttonClose_button = buttonClose.AddComponent<Button>();
            buttonClose_button.transition = Selectable.Transition.None;

            buttonClose.AddComponent<MyUGUIButton>();

            GameObject buttonMain = new GameObject("ButtonMain");
            buttonMain.transform.SetParent(container.transform, false);

            RectTransform buttonMain_rect = buttonMain.AddComponent<RectTransform>();
            MyUtilities.Anchor(ref buttonMain_rect, MyUtilities.EAnchorPreset.BottomCenter, MyUtilities.EAnchorPivot.BottomCenter, 300, 100, 0, 20);

            Image buttonMain_image = buttonMain.AddComponent<Image>();
            buttonMain_image.color = Color.green;
            buttonMain_image.raycastTarget = true;

            Button buttonMain_button = buttonMain.AddComponent<Button>();
            buttonMain_button.transition = Selectable.Transition.None;

            buttonMain.AddComponent<MyUGUIButton>();

            GameObject buttonMainText = new GameObject("Text");
            buttonMainText.transform.SetParent(buttonMain.transform, false);

            Text buttonMainText_text = buttonMainText.AddComponent<Text>();
            buttonMainText_text.text = "Main Button";
            buttonMainText_text.color = Color.black;
#if UNITY_2022_2_OR_NEWER
            buttonMainText_text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
#else
            buttonMainText_text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
#endif
            buttonMainText_text.fontSize = 40;
            buttonMainText_text.alignment = TextAnchor.MiddleCenter;
            buttonMainText_text.horizontalOverflow = HorizontalWrapMode.Wrap;
            buttonMainText_text.verticalOverflow = VerticalWrapMode.Overflow;
            buttonMainText_text.raycastTarget = false;

            string folderPath = "Assets/Resources/" + MyUGUIManagerBase.POPUP_DIRECTORY;
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string filePath = folderPath + prefabName + ".prefab";
            UnityEditor.PrefabUtility.CreatePrefab(filePath, prefab, UnityEditor.ReplacePrefabOptions.ReplaceNameBased);

            GameObject.Destroy(prefab);
        }

#endif

        #endregion
    }
}