using UnityEngine;
using System.Collections.Generic;

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_UIManager : MonoBehaviour
    {
        #region ----- Internal Struct -----

        public struct Button
        {
            public string name;
            public GameObject gameObject;
        }

        #endregion

        #region ----- Variable -----

        [SerializeField] private GameObject _gameObjectAbstractFactory;
        [SerializeField] private GameObject _gameObjectAdapter;
        [SerializeField] private GameObject _gameObjectBridge;
        [SerializeField] private GameObject _gameObjectBuilder;
        [SerializeField] private GameObject _gameObjectChainOfResponsibility;
        [SerializeField] private GameObject _gameObjectCommand;
        [SerializeField] private GameObject _gameObjectComposite;
        [SerializeField] private GameObject _gameObjectDecorator;
        [SerializeField] private GameObject _gameObjectFacade;
        [SerializeField] private GameObject _gameObjectFactoryMethod;
        [SerializeField] private GameObject _gameObjectFlyweight;
        [SerializeField] private GameObject _gameObjectIterator;
        [SerializeField] private GameObject _gameObjectMediator;
        [SerializeField] private GameObject _gameObjectPrototype;
        [SerializeField] private GameObject _gameObjectProxy;
        [SerializeField] private GameObject _gameObjectSingleton;
        [SerializeField] private GameObject _gameObjectState;
        [SerializeField] private GameObject _gameObjectStrategy;
        [SerializeField] private GameObject _gameObjectTemplateMethod;
        [SerializeField] private GameObject _gameObjectVisitor;
        private TextMesh _textResult;

        private Rect[] _rects;
        private Button[] _buttons;
        private GUIStyle _styleLabel;
        private GUIStyle _styleButton;

        #endregion

        #region ----- MonoBehaviour Implementation -----

        void Start()
        {
            _textResult = MyUtilities.FindObjectFromRoot("Result").GetComponent<TextMesh>();
            _textResult.gameObject.SetActive(false);
            
            int numCol = 6;
            float marginX = 10;
            float marginY = 50;
            float width = (Screen.width - 70) / numCol;
            float height = 70;
            float paddingX = 10;
            float paddingY = 5;

            _rects = new Rect[20];
            for (int i = 0; i < _rects.Length; ++i)
            {
                int row = i / numCol;
                int col = i % numCol;
                _rects[i] = new Rect(marginX + (width + paddingX) * col, marginY + (paddingY + height) * row, width, height);
            }

            List<Button> buttons = new List<Button>();
            buttons.Add(new Button() { name = "Abstract\nFactory", gameObject = _gameObjectAbstractFactory });
            buttons.Add(new Button() { name = "Adapter", gameObject = _gameObjectAdapter });
            buttons.Add(new Button() { name = "Bridge", gameObject = _gameObjectBridge });
            buttons.Add(new Button() { name = "Builder", gameObject = _gameObjectBuilder });
            buttons.Add(new Button() { name = "Chain of\nResponsibility", gameObject = _gameObjectChainOfResponsibility });
            buttons.Add(new Button() { name = "Command", gameObject = _gameObjectCommand });
            buttons.Add(new Button() { name = "Composite", gameObject = _gameObjectComposite });
            buttons.Add(new Button() { name = "Decorator\n(Wrapper)", gameObject = _gameObjectDecorator });
            buttons.Add(new Button() { name = "Facade", gameObject = _gameObjectFacade });
            buttons.Add(new Button() { name = "Factory\nMethod", gameObject = _gameObjectFactoryMethod });
            buttons.Add(new Button() { name = "Flyweight", gameObject = _gameObjectFlyweight });
            buttons.Add(new Button() { name = "Iterator\n(Cursor)", gameObject = _gameObjectIterator });
            buttons.Add(new Button() { name = "Mediator", gameObject = _gameObjectMediator });
            buttons.Add(new Button() { name = "Prototype", gameObject = _gameObjectPrototype });
            buttons.Add(new Button() { name = "Proxy", gameObject = _gameObjectProxy });
            buttons.Add(new Button() { name = "Singleton", gameObject = _gameObjectSingleton });
            buttons.Add(new Button() { name = "State", gameObject = _gameObjectState });
            buttons.Add(new Button() { name = "Strategy", gameObject = _gameObjectStrategy });
            buttons.Add(new Button() { name = "Template\nMethod", gameObject = _gameObjectTemplateMethod });
            buttons.Add(new Button() { name = "Visitor\n(Double Dispatch)", gameObject = _gameObjectVisitor });
            _buttons = buttons.ToArray();

            _Active(null);
        }
        
        void OnGUI()
        {
            if (_styleButton == null)
            {
                _styleLabel = new GUIStyle(GUI.skin.label);
                _styleLabel.fontSize = 22;
                _styleLabel.alignment = TextAnchor.MiddleCenter;

                _styleButton = new GUIStyle(GUI.skin.button);
                _styleButton.fontSize = 22;
            }

            for (int i = 0; i < _buttons.Length; ++i)
            {
                if (GUI.Button(_rects[i], _buttons[i].name, _styleButton))
                {
                    _Active(_buttons[i].gameObject);
                }
            }

            GUI.Label(new Rect(20, 100, Screen.width - 40, 1500), _textResult.text, _styleLabel);
        }

        #endregion

        #region ----- Private Method -----

        private void _Active(GameObject gameObject)
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }
            gameObject?.SetActive(true);
        }

        #endregion
    }
}