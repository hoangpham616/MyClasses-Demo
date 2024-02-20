using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using MyClasses;
using MyClasses.UI;
using System.Linq;

namespace MyApp
{
    public class ReusableListViewPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonTop;
        private MyUGUIButton _buttonMid;
        private MyUGUIButton _buttonBot;
        private MyUGUIReusableListView _listView;
        private MyUGUIScrollView _scrollView;

        #endregion

        #region ----- Constructor -----

        public ReusableListViewPopup(EPopupID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isRepeatable = false)
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
            _buttonTop = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonTop").GetComponent<MyUGUIButton>();
            _buttonMid = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonMid").GetComponent<MyUGUIButton>();
            _buttonBot = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonBot").GetComponent<MyUGUIButton>();
            _listView = MyUtilities.FindObject(GameObjectCanvas, "Container/ListView").GetComponent<MyUGUIReusableListView>();
            _scrollView = _listView.GetComponent<MyUGUIScrollView>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonTop.OnEventPointerClick.AddListener(_OnClickTop);
            _buttonMid.OnEventPointerClick.AddListener(_OnClickMid);
            _buttonBot.OnEventPointerClick.AddListener(_OnClickBot);
            _listView.OnPullAtTheStart += _OnPullAtTheStart;
            _listView.OnPullAtTheEnd += _OnPullAtTheEnd;

            _listView.Initialize();
            int quantity = UnityEngine.Random.Range(15, 150);
            ReusableListItemViewModel[] models = new ReusableListItemViewModel[quantity];
            for (int i = 0; i < quantity; ++i)
            {
                models[i] = new ReusableListItemViewModel()
                {
                    Letter = ((char)UnityEngine.Random.Range(65, 90)).ToString()
                };
            }
            for (int i = 0, count = _listView.ItemViews.Length; i < count; ++i)
            {
                ReusableListItemView itemView = (ReusableListItemView)_listView.ItemViews[i];
                itemView.Button.OnEventPointerClick.AddListener(_OnClickItem);
            }
            _listView.SetModels(models);
            _listView.Reload();
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
            _listView.OnPullAtTheStart -= _OnPullAtTheStart;
            _listView.OnPullAtTheEnd -= _OnPullAtTheEnd;
            for (int i = 0, count = _listView.ItemViews.Length; i < count; ++i)
            {
                ReusableListItemView itemView = (ReusableListItemView)_listView.ItemViews[i];
                itemView.Button.OnEventPointerClick.RemoveAllListeners();
            }

            _listView.Reload(0);
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

        #region ----- List View Event -----

        private void _OnPullAtTheStart()
        {
            this.LogInfo("_OnPullAtTheStart", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowLoadingIndicator(2);
        }

        private void _OnPullAtTheEnd()
        {
            this.LogInfo("_OnPullAtTheEnd", null, ELogColor.UI);

            MyUGUIManager.Instance.ShowLoadingIndicator(2);
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            this.LogInfo("_OnClickClose", null, ELogColor.UI);

            Hide();
        }

        private void _OnClickItem(PointerEventData arg0)
        {
            this.LogInfo("_OnClickItem", null, ELogColor.UI);

            ReusableListItemView itemView = arg0.pointerClick.transform.parent.GetComponent<ReusableListItemView>();
            if (itemView != null)
            {
                MyUGUIManager.Instance.ShowToastMessage("Click on item " + itemView.Index);
            }
        }

        private void _OnClickTop(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTop", null, ELogColor.UI);

            float moveTime = 1f - _scrollView.VerticalNormalizedPosition;
            moveTime = Mathf.Max(moveTime, 0.2f);
            _scrollView.MoveToStart(1f - _scrollView.VerticalNormalizedPosition, () =>
            {
                this.LogInfo("_OnClickTop", "list has move in " + moveTime + " seconds");
            });
        }

        private void _OnClickMid(PointerEventData arg0)
        {
            this.LogInfo("_OnClickMid", null, ELogColor.UI);

            float moveTime = Mathf.Abs(0.5f - _scrollView.VerticalNormalizedPosition);
            moveTime = Mathf.Max(moveTime, 0.2f);
            _scrollView.MoveToMiddle(moveTime, () =>
            {
                this.LogInfo("_OnClickMid", "list has move in " + moveTime + " seconds");
            });
        }

        private void _OnClickBot(PointerEventData arg0)
        {
            this.LogInfo("_OnClickBot", null, ELogColor.UI);

            float moveTime = _scrollView.VerticalNormalizedPosition;
            moveTime = Mathf.Max(moveTime, 0.2f);
            _scrollView.MoveToEnd(moveTime, () =>
            {
                this.LogInfo("_OnClickBot", "list has move in " + moveTime + " seconds");
            });
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}