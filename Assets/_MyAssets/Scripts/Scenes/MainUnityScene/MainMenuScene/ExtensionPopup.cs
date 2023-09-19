using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class ExtensionPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonTop;
        private MyUGUIButton _buttonMid;
        private MyUGUIButton _buttonBot;

        #endregion

        #region ----- Constructor -----

        public ExtensionPopup(EPopupID id, string prefabName, bool isFloat = false, bool isRepeatable = false)
            : base(id, prefabName, isFloat, isRepeatable)
        {
        }

        #endregion

        #region ----- MyUGUIPopup Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonClose = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonClose").GetComponent<MyUGUIButton>();
            _buttonTop = MyUtilities.FindObject(GameObjectCanvas, "Container/Left/ButtonTop").GetComponent<MyUGUIButton>();
            _buttonMid = MyUtilities.FindObject(GameObjectCanvas, "Container/Left/ButtonMid").GetComponent<MyUGUIButton>();
            _buttonBot = MyUtilities.FindObject(GameObjectCanvas, "Container/Left/ButtonBot").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonTop.OnEventPointerClick.AddListener(_OnClickTop);
            _buttonMid.OnEventPointerClick.AddListener(_OnClickMid);
            _buttonBot.OnEventPointerClick.AddListener(_OnClickBot);

            _SelectButton(_buttonTop, _buttonMid, _buttonBot, 0);

            MyUtilities.TweenNumber(10, 1000000, 100, 0.1f, (float number) =>
            {
                _buttonTop.Text.text = "TOP_" + number;
            }, null);
            MyUtilities.TweenNumber(200, 2000000, 100, 0.1f, (float number) =>
            {
                _buttonMid.Text.text = "MID_" + number;
            }, null);
            MyUtilities.TweenNumber(3000, 3000000, 100, 0.1f, (float number) =>
            {
                _buttonBot.Text.text = "BOT_" + number;
            }, null);
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

            _buttonClose.OnEventPointerClick.RemoveAllListeners();
            _buttonTop.OnEventPointerClick.RemoveAllListeners();
            _buttonMid.OnEventPointerClick.RemoveAllListeners();
            _buttonBot.OnEventPointerClick.RemoveAllListeners();
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

        private void _OnClickClose(PointerEventData arg0)
        {
            this.LogInfo("_OnClickClose", null, ELogColor.UI);

            Hide();
        }

        private void _OnClickTop(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTop", null, ELogColor.UI);

            if (_buttonTop.transform.localScale.x < 1)
            {
                _SelectButton(_buttonTop, _buttonMid, _buttonBot, 0.15f);
            }
        }

        private void _OnClickMid(PointerEventData arg0)
        {
            this.LogInfo("_OnClickMid", null, ELogColor.UI);

            if (_buttonMid.transform.localScale.x < 1)
            {
                _SelectButton(_buttonMid, _buttonTop, _buttonBot, 0.15f);
            }
        }

        private void _OnClickBot(PointerEventData arg0)
        {
            this.LogInfo("_OnClickBot", null, ELogColor.UI);

            if (_buttonBot.transform.localScale.x < 1)
            {
                _SelectButton(_buttonBot, _buttonTop, _buttonMid, 0.15f);
            }
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----

        private void _SelectButton(MyUGUIButton buttonActive, MyUGUIButton buttonDeactive, MyUGUIButton buttonDeactive2, float duration)
        {
            MyUGUISizeFitter sizeFitterActive = buttonActive.GetComponent<MyUGUISizeFitter>();
            MyUGUISizeFitter sizeFitterDeactive = buttonDeactive.GetComponent<MyUGUISizeFitter>();
            MyUGUISizeFitter sizeFitterDeactive2 = buttonDeactive2.GetComponent<MyUGUISizeFitter>();

            float activeAlpha = 0.9f;
            float activeScale = 1;
            float activeWidth = 120;
            float activeRightWidth = 20;

            float deactiveAlpha = 0.6f;
            float deactiveScale = 0.75f;
            float deactiveLosingWidth = (activeWidth - activeRightWidth) * (activeScale - deactiveScale);
            float deactiveAddingWidth = deactiveLosingWidth * (activeScale / deactiveScale);
            float deactiveWidth = activeWidth + deactiveAddingWidth;

            MyUtilities.TweenNumber(deactiveAlpha, activeAlpha, duration, duration / 10, (float alpha) =>
            {
                Color color = buttonActive.Button.image.color;
                color.a = alpha;
                buttonActive.Button.image.color = color;
            }, () =>
            {
                Color color = buttonActive.Button.image.color;
                color.a = activeAlpha;
                buttonActive.Button.image.color = color;
            });
            MyUtilities.TweenNumber(activeAlpha, deactiveAlpha, duration, duration / 10, (float alpha) =>
            {
                Color color = buttonDeactive.Button.image.color;
                color.a = alpha;
                buttonDeactive.Button.image.color = color;
                buttonDeactive2.Button.image.color = color;
            }, () =>
            {
                Color color = buttonDeactive.Button.image.color;
                color.a = deactiveAlpha;
                buttonDeactive.Button.image.color = color;
                buttonDeactive2.Button.image.color = color;
            });
            MyUtilities.TweenNumber(deactiveScale, activeScale, duration, duration / 10, (float scale) =>
            {
                sizeFitterActive.transform.localScale = Vector3.one * scale;
            }, () =>
            {
                sizeFitterActive.transform.localScale = Vector3.one * activeScale;
            });
            MyUtilities.TweenNumber(activeScale, deactiveScale, duration, duration / 10, (float scale) =>
            {
                sizeFitterDeactive.transform.localScale = Vector3.one * scale;
                sizeFitterDeactive2.transform.localScale = Vector3.one * scale;
            }, () =>
            {
                sizeFitterDeactive.transform.localScale = Vector3.one * deactiveScale;
                sizeFitterDeactive2.transform.localScale = Vector3.one * deactiveScale;
            });
            MyUtilities.TweenNumber(deactiveWidth, activeWidth, duration, duration / 10, (float width) =>
            {
                sizeFitterActive.ExtraWidth = width;
                sizeFitterActive.Resize();
            }, () =>
            {
                sizeFitterActive.ExtraWidth = activeWidth;
                sizeFitterActive.Resize();
            });
            MyUtilities.TweenNumber(activeWidth, deactiveWidth, duration, duration / 10, (float width) =>
            {
                sizeFitterDeactive.ExtraWidth = width;
                sizeFitterDeactive.Resize();
                sizeFitterDeactive2.ExtraWidth = width;
                sizeFitterDeactive2.Resize();
            }, () =>
            {
                sizeFitterDeactive.ExtraWidth = deactiveWidth;
                sizeFitterDeactive.Resize();
                sizeFitterDeactive2.ExtraWidth = deactiveWidth;
                sizeFitterDeactive2.Resize();
            });
        }

        #endregion
    }
}