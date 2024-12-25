using UnityEngine;
using System.Collections.Generic;

public class MyClasses_Benchmark_Script_UIManager : MonoBehaviour
{
    #region ----- Internal Struct -----

    public struct Button
    {
        public string name;
        public GameObject gameObject;
    }

    #endregion

    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private GameObject _gameObjectArithmeticOperators;
    [SerializeField] private GameObject _gameObjectComparisonOperators;
    [SerializeField] private GameObject _gameObjectDictionaryHashtable;
    [SerializeField] private GameObject _gameObjectDistance;
    [SerializeField] private GameObject _gameObjectExternCall;
    [SerializeField] private GameObject _gameObjectFindObject;
    [SerializeField] private GameObject _gameObjectIf;
    [SerializeField] private GameObject _gameObjectIncrementOperator;
    [SerializeField] private GameObject _gameObjectIteration;
    [SerializeField] private GameObject _gameObjectMyLogger;
    [SerializeField] private GameObject _gameObjectOrderOfOperation;
    [SerializeField] private GameObject _gameObjectString;
    [SerializeField] private GameObject _gameObjectStringBuilder;

    private Rect[] _rects;
    private Button[] _buttons;
    private GUIStyle _styleLabel;
    private GUIStyle _styleButton;

    #endregion

    #region ----- MonoBehaviour Implementation -----

    void Start()
    {
        _textResult.gameObject.SetActive(false);
        
        int numCol = 6;
        float marginX = 10;
        float marginY = 50;
        float width = (Screen.width - 70) / numCol;
        float height = 100;
        float paddingX = 10;
        float paddingY = 5;

        List<Button> buttons = new List<Button>();
        buttons.Add(new Button() { name = "Arithmetic\nOperators", gameObject = _gameObjectArithmeticOperators });
        buttons.Add(new Button() { name = "Comparison\nOperators", gameObject = _gameObjectComparisonOperators });
        buttons.Add(new Button() { name = "Dictionary\nHashtable", gameObject = _gameObjectDictionaryHashtable });
        buttons.Add(new Button() { name = "Distance", gameObject = _gameObjectDistance });
        buttons.Add(new Button() { name = "Extern\nCall", gameObject = _gameObjectExternCall });
        buttons.Add(new Button() { name = "Find\nObject", gameObject = _gameObjectFindObject });
        buttons.Add(new Button() { name = "If", gameObject = _gameObjectIf });
        buttons.Add(new Button() { name = "Increment\nOperator", gameObject = _gameObjectIncrementOperator });
        buttons.Add(new Button() { name = "Iteration", gameObject = _gameObjectIteration });
        buttons.Add(new Button() { name = "MyLogger", gameObject = _gameObjectMyLogger });
        buttons.Add(new Button() { name = "Order of\nOperation", gameObject = _gameObjectOrderOfOperation });
        buttons.Add(new Button() { name = "String", gameObject = _gameObjectString });
        buttons.Add(new Button() { name = "String\nBuilder", gameObject = _gameObjectStringBuilder });
        _buttons = buttons.ToArray();

        _rects = new Rect[_buttons.Length];
        for (int i = 0; i < _rects.Length; ++i)
        {
            int row = i / numCol;
            int col = i % numCol;
            _rects[i] = new Rect(marginX + (width + paddingX) * col, marginY + (paddingY + height) * row, width, height);
        }

        _Active(null);
    }
    
    void OnGUI()
    {
        if (_styleLabel == null)
        {
            _styleLabel = new GUIStyle(GUI.skin.label);
            _styleLabel.fontSize = 24;
            _styleLabel.alignment = TextAnchor.MiddleCenter;
                
            _styleButton = new GUIStyle(GUI.skin.button);
            _styleButton.fontSize = 24;
        }

        for (int i = 0; i < _buttons.Length; ++i)
        {
            if (GUI.Button(_rects[i], _buttons[i].name, _styleButton))
            {
                _Active(_buttons[i].gameObject);
            }
        }

#if UNITY_EDITOR
        GUI.Label(new Rect(0, 300, Screen.width, 200), "The memory usage will sometimes be incorrect\nbecause of Garbage Collection process in the editor", _styleLabel);
#endif

        GUI.Label(new Rect(20, 550, Screen.width - 40, 500), _textResult.text, _styleLabel);
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