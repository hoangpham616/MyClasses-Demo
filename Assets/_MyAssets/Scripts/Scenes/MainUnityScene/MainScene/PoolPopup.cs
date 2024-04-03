using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class PoolPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonUseString;
        private MyUGUIButton _buttonUsePrefab;
        private MyUGUIButton _buttonReturn;
        private Transform _transformItemParent;

        private List<Text> _textItems = new List<Text>();

        #endregion

        #region ----- Constructor -----

        public PoolPopup(EPopupID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isRepeatable = false)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isRepeatable)
        {
        }

        #endregion

        #region ----- MyUGUIPopup Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonClose = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonClose").GetComponent<MyUGUIButton>();
            _buttonUseString = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonUseString").GetComponent<MyUGUIButton>();
            _buttonUsePrefab = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonUsePrefab").GetComponent<MyUGUIButton>();
            _buttonReturn = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonReturn").GetComponent<MyUGUIButton>();
            _transformItemParent = MyUtilities.FindObject(GameObjectCanvas, "Container/Items").transform;
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "popup id = " + MyUGUIManager.Instance.CurrentPopup.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonUseString.OnEventPointerClick.AddListener(_OnClickUseString);
            _buttonUsePrefab.OnEventPointerClick.AddListener(_OnClickUsePrefab);
            _buttonReturn.OnEventPointerClick.AddListener(_OnClickReturn);
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
            _buttonUseString.OnEventPointerClick.RemoveAllListeners();
            _buttonUsePrefab.OnEventPointerClick.RemoveAllListeners();
            _buttonReturn.OnEventPointerClick.RemoveAllListeners();

            for (int i = _textItems.Count - 1; i >= 0; --i)
            {
                MyPoolManager.Instance.Return(_textItems[i].gameObject);
            }
            _textItems.Clear();
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

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            Hide();
        }

        private void _OnClickUseString(PointerEventData arg0)
        {
            this.LogInfo("_OnClickUseString", null, ELogColor.UI);

            Text text = MyPoolManager.Instance.Use("Prefabs/TextPoolObject").GetComponent<Text>();
            text.transform.SetParent(_transformItemParent, false);
            text.transform.localPosition = new Vector3(UnityEngine.Random.Range(-300, 300), UnityEngine.Random.Range(-300, 300), 0);
            _textItems.Add(text);
        }

        private void _OnClickUsePrefab(PointerEventData arg0)
        {
            this.LogInfo("_OnClickUsePrefab", null, ELogColor.UI);

            GameObject prefab = MyResourceManager.LoadPrefab("Prefabs/TextPoolObject");

            Text text = MyPoolManager.Instance.Use(prefab).GetComponent<Text>();
            text.transform.SetParent(_transformItemParent, false);
            text.transform.localPosition = new Vector3(UnityEngine.Random.Range(-300, 300), UnityEngine.Random.Range(-300, 300), 0);
            _textItems.Add(text);
        }

        private void _OnClickReturn(PointerEventData arg0)
        {
            this.LogInfo("_OnClickReturn", null, ELogColor.UI);

            if (_textItems.Count > 0)
            {
                MyPoolManager.Instance.Return(_textItems[0].gameObject);
                _textItems.RemoveAt(0);
            }
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}