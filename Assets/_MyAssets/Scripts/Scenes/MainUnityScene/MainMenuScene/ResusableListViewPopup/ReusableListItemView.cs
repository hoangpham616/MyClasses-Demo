using UnityEngine;
using UnityEngine.UI;
using MyClasses.UI;

namespace MyApp
{
    public class ReusableListItemView : MyUGUIReusableListItemView
    {
        #region  ----- Variable -----

        [SerializeField]
        private Text _textIndex;

        #endregion

        #region  ----- MyUGUIReusableListItemView2 Implementation -----

        public override void OnReload()
        {
            ReusableListItemViewModel model = (ReusableListItemViewModel)Model;
            _textIndex.text = Index + ": " + (model != null ? model.Letter : string.Empty);
        }

        #endregion
    }

    public class ReusableListItemViewModel
    {
        public string Letter;
    }
}