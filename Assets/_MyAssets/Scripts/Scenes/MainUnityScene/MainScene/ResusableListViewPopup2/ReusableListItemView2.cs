using UnityEngine;
using MyClasses.UI;
using TMPro;
using UnityEngine.UI;

namespace MyApp
{
    public class ReusableListItemView2 : MyUGUIReusableListItemView2
    {
        #region  ----- Variable -----

        [SerializeField]
        private RectTransform[] _rectTransformTypes;
        [SerializeField]
        private TextMeshProUGUI[] _textMessages;

        #endregion

        #region  ----- MyUGUIReusableListItemView2 Implementation -----

        public override void OnReload()
        {
            ReusableListViewItemModel2 model = (ReusableListViewItemModel2)Model;
            for (int i = 0; i < _rectTransformTypes.Length; ++i)
            {
                RectTransform rectTransformType = _rectTransformTypes[i];
                int typeIndex = model.Type % _rectTransformTypes.Length;
                if (i == typeIndex)
                {
                    rectTransformType.gameObject.SetActive(true);
                    
                    Vector2 sizeDelta = SizeDelta;
                    TextMeshProUGUI textMessage = _textMessages[i];
                    if (textMessage != null)
                    {
                        textMessage.text = Index + ": " + (model != null ? model.Message : string.Empty);
                        LayoutRebuilder.ForceRebuildLayoutImmediate(textMessage.rectTransform);

                        Vector2 sizeDeltaType = rectTransformType.sizeDelta;
                        sizeDeltaType.y = textMessage.GetPreferredValues().y;
                        rectTransformType.sizeDelta = sizeDeltaType;

                        sizeDelta.y = textMessage.GetPreferredValues().y + 10;
                    }
                    else
                    {
                        sizeDelta.y = rectTransformType.sizeDelta.y + 10;
                    }
                    SizeDelta = sizeDelta;
                }
                else
                {
                    rectTransformType.gameObject.SetActive(false);
                }
            }
        }

        #endregion
    }

    public class ReusableListViewItemModel2
    {
        public int Type;
        public string Message;
    }
}