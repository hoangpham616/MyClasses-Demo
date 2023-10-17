using UnityEngine;
using UnityEngine.UI;
using MyClasses.UI;

namespace MyApp
{
    public class ReusableListItemView2 : MyUGUIReusableListItemView2
    {
        #region  ----- Variable -----

        [SerializeField]
        private Text _text;
        [SerializeField]
        private GameObject[] _gameObjects;

        #endregion

        #region  ----- MyUGUIReusableListItemView2 Implementation -----

        public override void OnReload()
        {
            ReusableListViewItemModel2 model = (ReusableListViewItemModel2)Model;
            _text.text = Index + ": " + (model != null ? model.Letter : string.Empty);
            for (int i = 0; i < _gameObjects.Length; ++i)
            {
                if (i == model.Size % _gameObjects.Length)
                {
                    _gameObjects[i].SetActive(true);
                    RectTransform rectTransform = _gameObjects[i].GetComponent<RectTransform>();
                    Vector2 sizeDelta = SizeDelta;
                    sizeDelta.y = rectTransform.sizeDelta.y;
                    SizeDelta = sizeDelta;
                }
                else
                {
                    _gameObjects[i].SetActive(false);
                }
            }
        }

        #endregion
    }

    public class ReusableListViewItemModel2
    {
        public string Letter;
        public int Size;
    }
}