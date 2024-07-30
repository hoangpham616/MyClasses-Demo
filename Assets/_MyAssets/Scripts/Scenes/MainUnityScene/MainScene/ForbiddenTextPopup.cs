using UnityEngine.EventSystems;
using UnityEngine.UI;
using MyClasses;
using MyClasses.UI;
using TMPro;

namespace MyApp
{
    public class ForbiddenTextPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private TMP_InputField _inputFieldText;
        private Text _textResult1;
        private Text _textResult2;
        private Text _textResult3;

        #endregion

        #region ----- Constructor -----

        public ForbiddenTextPopup(MyUGUIConfigPopup config, bool isRepeatable = false)
            : base(config, isRepeatable)
        {
        }

        #endregion

        #region ----- MyUGUIPopup Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonClose = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonClose").GetComponent<MyUGUIButton>();
            _inputFieldText = MyUtilities.FindObject(GameObjectCanvas, "Container/Text/InputFieldText").GetComponent<TMP_InputField>();
            _textResult1 = MyUtilities.FindObject(GameObjectCanvas, "Container/Result_1/Text").GetComponent<Text>();
            _textResult2 = MyUtilities.FindObject(GameObjectCanvas, "Container/Result_2/Text").GetComponent<Text>();
            _textResult3 = MyUtilities.FindObject(GameObjectCanvas, "Container/Result_3/Text").GetComponent<Text>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "popup id = " + MyUGUIManager.Instance.CurrentPopup.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _inputFieldText.onValueChanged.AddListener(_OnChangeText);

            _UpdateResult(_inputFieldText.text);
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
            _inputFieldText.onValueChanged.RemoveAllListeners();
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
            MyUGUIManager.Instance.Back();
        }

        #endregion

        #region ----- Input Field Event -----

        private void _OnChangeText(string text)
        {
            _UpdateResult(text);
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            Hide();
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----

        private void _UpdateResult(string text)
        {
            _textResult1.text = MyForbiddenTextManager.Instance.ReplaceForbiddenWords(text);
            _textResult2.text = MyForbiddenTextManager.Instance.ReplaceForbiddenWords(text, true, true);
            _textResult3.text = MyForbiddenTextManager.Instance.ReplaceForbiddenWords(text, true, true, true);
        }

        #endregion
    }
}