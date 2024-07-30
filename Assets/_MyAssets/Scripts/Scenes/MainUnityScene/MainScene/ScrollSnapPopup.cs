using UnityEngine;
using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;
using UnityEngine.UI;
using System.Linq;

namespace MyApp
{
    public class ScrollSnapPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIScrollSnap _scrollSnapVertical;
        private MyUGUIButton _buttonVerticalPrevious;
        private MyUGUIButton _buttonVerticalNext;
        private MyUGUIScrollSnap _scrollSnapHorizontal;
        private MyUGUIButton _buttonHorizontalPrevious;
        private MyUGUIButton _buttonHorizontalNext;
        private Text _textHorizontalPagination;
        private GameObject _gameObjectPrefab1;
        private GameObject _gameObjectPrefab2;

        #endregion

        #region ----- Constructor -----

        public ScrollSnapPopup(MyUGUIConfigPopup config, bool isRepeatable = false)
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

            _scrollSnapVertical = MyUtilities.FindObject(GameObjectCanvas, "Container/VerticalScrollSnap").GetComponent<MyUGUIScrollSnap>();
            _buttonVerticalPrevious = MyUtilities.FindObject(_scrollSnapVertical.gameObject, "ButtonPrevious").GetComponent<MyUGUIButton>();
            _buttonVerticalNext = MyUtilities.FindObject(_scrollSnapVertical.gameObject, "ButtonNext").GetComponent<MyUGUIButton>();

            _scrollSnapHorizontal = MyUtilities.FindObject(GameObjectCanvas, "Container/HorizontalScrollSnap").GetComponent<MyUGUIScrollSnap>();
            _buttonHorizontalPrevious = MyUtilities.FindObject(_scrollSnapHorizontal.gameObject, "ButtonPrevious").GetComponent<MyUGUIButton>();
            _buttonHorizontalNext = MyUtilities.FindObject(_scrollSnapHorizontal.gameObject, "ButtonNext").GetComponent<MyUGUIButton>();
            _textHorizontalPagination = MyUtilities.FindObject(_scrollSnapHorizontal.gameObject, "Pagination").GetComponent<Text>();
            _gameObjectPrefab1 = MyUtilities.FindObject(_scrollSnapHorizontal.gameObject, "Prefab1");
            _gameObjectPrefab2 = MyUtilities.FindObject(_scrollSnapHorizontal.gameObject, "Prefab2");
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonVerticalPrevious.OnEventPointerClick.AddListener(_OnClickVerticalPrevious);
            _buttonVerticalNext.OnEventPointerClick.AddListener(_OnClickVerticalNext);
            _buttonHorizontalPrevious.OnEventPointerClick.AddListener(_OnClickHorizontalPrevious);
            _buttonHorizontalNext.OnEventPointerClick.AddListener(_OnClickHorizontalNext);
            _scrollSnapHorizontal.OnPageChanged += _OnPageChanged;

            _scrollSnapVertical.AddPage(10);
            for (int i = 0, count = _scrollSnapVertical.Pages.Count(); i < count; ++i)
            {
                GameObject page = _scrollSnapVertical.Pages[i];
                page.GetComponent<Image>().color = i % 2 == 0 ? Color.cyan : Color.magenta;
                page.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            }

            _scrollSnapHorizontal.AddPage(12);
            for (int i = 0, count = _scrollSnapHorizontal.Pages.Count(); i < count; ++i)
            {
                GameObject page = _scrollSnapHorizontal.Pages[i];
                page.GetComponent<Image>().color = i % 3 == 0 ? Color.red : (i % 3 == 1 ? Color.green : Color.blue);
            }
            _UpdatePagination();
        }

        public override bool OnUGUIVisible()
        {
            _scrollSnapVertical.UpdateLayout();
            _scrollSnapVertical.GoToPage(4, true);
            _scrollSnapHorizontal.UpdateLayout();

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
            _buttonHorizontalPrevious.OnEventPointerClick.RemoveAllListeners();
            _buttonHorizontalNext.OnEventPointerClick.RemoveAllListeners();
            _scrollSnapHorizontal.OnPageChanged -= _OnPageChanged;
        }

        public override bool OnUGUIInvisible()
        {
            if (base.OnUGUIInvisible())
            {
                this.LogInfo("OnUGUIInvisible", null, ELogColor.DARK_UI);

                _scrollSnapVertical.RemoveAllPages();
                _scrollSnapHorizontal.RemoveAllPages();

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

        #region ----- Scroll Snap Event -----

        private void _OnPageChanged(int pageIndex)
        {
            this.LogInfo("_OnPageChanged", "pageIndex=" + pageIndex, ELogColor.UI);

            _UpdatePagination();
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            this.LogInfo("_OnClickClose", null, ELogColor.UI);

            Hide();
        }

        private void _OnClickVerticalPrevious(PointerEventData arg0)
        {
            this.LogInfo("_OnClickVerticalPrevious", null, ELogColor.UI);

            _scrollSnapVertical.PreviousPage();
        }

        private void _OnClickVerticalNext(PointerEventData arg0)
        {
            this.LogInfo("_OnClickVerticalNext", null, ELogColor.UI);

            _scrollSnapVertical.NextPage();
        }

        private void _OnClickHorizontalPrevious(PointerEventData arg0)
        {
            this.LogInfo("_OnClickHorizontalPrevious", null, ELogColor.UI);

            _scrollSnapHorizontal.PreviousPage();
        }

        private void _OnClickHorizontalNext(PointerEventData arg0)
        {
            this.LogInfo("_OnClickHorizontalNext", null, ELogColor.UI);

            _scrollSnapHorizontal.NextPage();
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----

        private void _UpdatePagination()
        {
            _textHorizontalPagination.text = (_scrollSnapHorizontal.CurrentPage + 1) + "/" + _scrollSnapHorizontal.PageQuantity;
        }

        #endregion
    }
}