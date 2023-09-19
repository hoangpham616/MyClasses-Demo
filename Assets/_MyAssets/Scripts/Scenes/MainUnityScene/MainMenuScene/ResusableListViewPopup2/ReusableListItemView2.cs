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
        private Image _image;

        #endregion

        #region  ----- MyUGUIReusableListItemView2 Implementation -----

        public override void OnReload()
        {
            ReusableListViewItemModel2 model = (ReusableListViewItemModel2)Model;
            _text.text = Index + ": " + (model != null ? model.Letter : string.Empty);
            _image.color = model.Color;
            var sizeDelta = SizeDelta;
            sizeDelta.x = model.Size;
            sizeDelta.y = model.Size;
            SizeDelta = sizeDelta;
        }

        #endregion
    }

    public class ReusableListViewItemModel2
    {
        public string Letter;
        public Color Color;
        public int Size;
    }
}