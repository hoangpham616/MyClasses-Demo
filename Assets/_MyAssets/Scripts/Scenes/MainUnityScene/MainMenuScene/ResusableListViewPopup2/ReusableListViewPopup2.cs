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
    public class ReusableListViewPopup2 : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonInsert;
        private MyUGUIButton _buttonNew;
        private MyUGUIButton _buttonAdd;
        private MyUGUIButton _buttonTop;
        private MyUGUIButton _buttonMid;
        private MyUGUIButton _buttonBot;
        private MyUGUIReusableListView2 _listView;
        private MyUGUIScrollView _scrollView;

        #endregion

        #region ----- Constructor -----

        public ReusableListViewPopup2(EPopupID id, string prefabName, bool isFloat = false, bool isRepeatable = false)
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
            _buttonInsert = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonInsert").GetComponent<MyUGUIButton>();
            _buttonNew = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonNew").GetComponent<MyUGUIButton>();
            _buttonAdd = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonAdd").GetComponent<MyUGUIButton>();
            _buttonTop = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonTop").GetComponent<MyUGUIButton>();
            _buttonMid = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonMid").GetComponent<MyUGUIButton>();
            _buttonBot = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonBot").GetComponent<MyUGUIButton>();
            _listView = MyUtilities.FindObject(GameObjectCanvas, "Container/ListView").GetComponent<MyUGUIReusableListView2>();
            _scrollView = _listView.GetComponent<MyUGUIScrollView>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", null, ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonInsert.OnEventPointerClick.AddListener(_OnClickInsert);
            _buttonNew.OnEventPointerClick.AddListener(_OnClickNew);
            _buttonAdd.OnEventPointerClick.AddListener(_OnClickAdd);
            _buttonTop.OnEventPointerClick.AddListener(_OnClickTop);
            _buttonMid.OnEventPointerClick.AddListener(_OnClickMid);
            _buttonBot.OnEventPointerClick.AddListener(_OnClickBot);

            int quantity = 1000;
            ReusableListViewItemModel2[] models = new ReusableListViewItemModel2[quantity];
            for (int i = 0; i < quantity; ++i)
            {
                ReusableListViewItemModel2 model = new ReusableListViewItemModel2()
                {
                    Letter = ((char)UnityEngine.Random.Range(65, 90)).ToString(),
                    Color = i % 4 == 3 ? Color.red : (i % 4 == 2 ? Color.magenta : (i % 4 == 1 ? Color.green : Color.yellow)),
                    Size = ((i % 5) + 1) * 100,
                };
                models[i] = model;
            }
            _listView.Initialize(MyUGUIReusableListView2.EStartPosition.Head);
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
            _buttonInsert.OnEventPointerClick.RemoveAllListeners();
            _buttonNew.OnEventPointerClick.RemoveAllListeners();
            _buttonAdd.OnEventPointerClick.RemoveAllListeners();
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


        private void _OnClickInsert(PointerEventData arg0)
        {
            this.LogInfo("_OnClickInsert", null, ELogColor.UI);

            _listView.InsertModel(new ReusableListViewItemModel2()
            {
                Letter = ((char)UnityEngine.Random.Range(65, 90)).ToString(),
                Color = _listView.Models.Count % 4 == 3 ? Color.red : (_listView.Models.Count % 4 == 2 ? Color.magenta : (_listView.Models.Count % 4 == 1 ? Color.green : Color.yellow)),
                Size = ((_listView.Models.Count % 5) + 1) * 100,
            });
            _listView.Reload();
            _listView.RefreshDisplayItems();
        }

        private void _OnClickNew(PointerEventData arg0)
        {
            this.LogInfo("_OnClickNew", null, ELogColor.UI);

            int quantity = UnityEngine.Random.Range(1, 10);
            ReusableListViewItemModel2[] models = new ReusableListViewItemModel2[quantity];
            for (int i = 0; i < quantity; ++i)
            {
                ReusableListViewItemModel2 model = new ReusableListViewItemModel2()
                {
                    Letter = ((char)UnityEngine.Random.Range(65, 90)).ToString(),
                    Color = i % 4 == 3 ? Color.red : (i % 4 == 2 ? Color.magenta : (i % 4 == 1 ? Color.green : Color.yellow)),
                    Size = ((i % 5) + 1) * 100,
                };
                models[i] = model;
            }
            _listView.Initialize(UnityEngine.Random.Range(100, 1000) % 2  == 0 ? MyUGUIReusableListView2.EStartPosition.Head : MyUGUIReusableListView2.EStartPosition.Tail);
            _listView.SetModels(models);
            _listView.Reload();
        }

        private void _OnClickAdd(PointerEventData arg0)
        {
            this.LogInfo("_OnClickAdd", null, ELogColor.UI);

            _listView.AddModel(new ReusableListViewItemModel2()
            {
                Letter = ((char)UnityEngine.Random.Range(65, 90)).ToString(),
                Color = _listView.Models.Count % 4 == 3 ? Color.red : (_listView.Models.Count % 4 == 2 ? Color.magenta : (_listView.Models.Count % 4 == 1 ? Color.green : Color.yellow)),
                Size = ((_listView.Models.Count % 5) + 1) * 100,
            });
            _listView.Reload();
        }

        private void _OnClickTop(PointerEventData arg0)
        {
            this.LogInfo("_OnClickTop", null, ELogColor.UI);

            float startTime = Time.time;
            float moveSpeed = _listView.CurrentDisplayPosition * 10;
            _listView.MoveToStart(moveSpeed, (itemView) =>
            {
                this.LogInfo("_OnClickTop", "list has move in " + (Time.time - startTime) + " seconds");
            });
        }

        private void _OnClickMid(PointerEventData arg0)
        {
            this.LogInfo("_OnClickMid", null, ELogColor.UI);

            float startTime = Time.time;
            int targetIndex = _listView.Quantity / 2;
            float moveSpeed = Math.Abs(_listView.CurrentDisplayPosition - targetIndex) * 10;
            _listView.MoveTo(targetIndex, moveSpeed, (itemView) =>
            {
                this.LogInfo("_OnClickMid", "list has move in " + (Time.time - startTime) + " seconds");
            });
        }

        private void _OnClickBot(PointerEventData arg0)
        {
            this.LogInfo("_OnClickBot", null, ELogColor.UI);

            float startTime = Time.time;
            float moveSpeed = (_listView.Quantity - _listView.CurrentDisplayPosition) * 10;
            _listView.MoveToEnd(moveSpeed, (itemView) =>
            {
                this.LogInfo("_OnClickBot", "list has move in " + (Time.time - startTime) + " seconds");
            });
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}