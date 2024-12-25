using System.Collections;
using UnityEngine;

namespace MyClasses.Sample.DesignPattern
{
    public abstract class MyClasses_DesignPatterns_Script_Base : MonoBehaviour
    {
        protected string _name;
        [SerializeField] protected string _complexity;
        [SerializeField] protected string _popularity;
        [SerializeField] protected string _defination;
        [SerializeField] protected string _structure;
        [SerializeField] protected string _advantages;
        [SerializeField] protected string _disadvantages;
        [SerializeField] protected string _note;
        protected TextMesh _textResult;
        protected GameObject[] _gameObjectImages;

        void Awake()
        {
            _textResult = MyUtilities.FindObjectFromRoot("Result").GetComponent<TextMesh>();
            _gameObjectImages = new GameObject[transform.childCount];
            for (int i = 0; i < _gameObjectImages.Length; ++i)
            {
                _gameObjectImages[i] = transform.GetChild(i).gameObject;
            }
            _Define();
        }

        void OnEnable()
        {
            _textResult.text = _name;
            _textResult.text += "\nComplexity: " + _complexity;
            _textResult.text += "\nPopularity: " + _popularity;
            _textResult.text += "\n\n[Defination]\n" + _defination;
            _textResult.text += "\n\n[Structure]\n" + _structure;
            _textResult.text += "\n\n[Advantages]\n" + _advantages;
            _textResult.text += "\n\n[Disadvantages]\n" + _disadvantages;
            if (_note != null && _note.Length > 0)
            {
                _textResult.text += "\n\n[Note]\n" + _note;
            }
            _Usage();
            if (_gameObjectImages.Length > 0)
            {
                StartCoroutine(_AutoSwitchImage());
            }
        }

        protected abstract void _Define();

        protected abstract void _Usage();

        protected IEnumerator _AutoSwitchImage()
        {
            int index = 0;

            while (true)
            {
                for (int i = 0; i < _gameObjectImages.Length; ++i)
                {
                    _gameObjectImages[i].SetActive(i == index);
                }

                yield return new WaitForSeconds(3);

                index += 1;
                index %= _gameObjectImages.Length;
            }
        }
    }
}