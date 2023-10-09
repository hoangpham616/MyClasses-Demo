/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUITextMeshEffect (version 1.1)
 */

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

namespace MyClasses.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MyUGUITextMeshEffect : MonoBehaviour
    {
        #region ----- Internal Class -----

        private class MyUGUITextMeshEffectTagModel
        {
            public MyUGUIConfigTextMeshEffectTag Info;
            public int TagLength;
            public int StartIndex;
            public int EndIndex;
            public float TotalTime;
            public float Speed;
            public bool IsOverrideSpeed;
            public ETilt Tilt;
            public Color32 LastColor;
        }

        #endregion

        #region ----- Define -----

        private readonly string PATTERN_C_S_T = @"<c=([\w\d]+),s=([\d.-]+),t=(\w+)>";
        private readonly string PATTERN_C_T_S = @"<c=([\w\d]+),t=(\w+),s=([\d.-]+)>";
        private readonly string PATTERN_C_S = @"<c=([\w\d]+),s=([\d.-]+)>";
        private readonly string PATTERN_C_T = @"<c=([\w\d]+),t=(\w+)>";

        #endregion

        #region ----- Variable -----

        [SerializeField]
        private MyUGUIConfigTextMeshEffect _config;
        [SerializeField]
        private ETilt _tilt = ETilt.None;
        [SerializeField]
        private float _speed = 0.5f;
        [SerializeField]
        private EGradientLength _gradientLengthType = EGradientLength.TextLengthPlus;
        [SerializeField]
        private int _gradientLengthPlus = 6;
        [SerializeField]
        private int _gradientLength = 8;
        [SerializeField]
        private float _gradientLengthPercent = 100;

        private List<MyUGUITextMeshEffectTagModel> _tagModels = new List<MyUGUITextMeshEffectTagModel>();
        private TextMeshProUGUI _textMesh;
        private string _originText = string.Empty;
        private string _removedTagText = string.Empty;
        private Coroutine _coroutine;

        #endregion

        #region ----- MonoBehaviour Implementation -----

        /// <summary>
        /// Awake.
        /// </summary>
        void Awake()
        {
            _config = Resources.Load<MyUGUIConfigTextMeshEffect>(MyUGUIManagerBase.CONFIG_DIRECTORY + typeof(MyUGUIConfigTextMeshEffect).Name);
        }

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            if (_textMesh == null)
            {
                _textMesh = GetComponent<TextMeshProUGUI>();
            }

            _textMesh.OnPreRenderText += _OnPreRenderText;
            
            if (_originText.Length > 0)
            {
                _textMesh.text = _originText;
            }
        }

        /// <summary>
        /// OnDisable.
        /// </summary>
        void OnDisable()
        {
            _textMesh.OnPreRenderText -= _OnPreRenderText;

            _removedTagText = string.Empty;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = null;
        }

        #endregion

        #region ----- TMPro Implementation -----

        /// <summary>
        /// Process effects for each frame.
        /// </summary>
        private void _OnPreRenderText(TMP_TextInfo textInfo)
        {
            string text = textInfo.textComponent.text;
            if (text.Equals(_removedTagText))
            {
                return;
            }

            _originText = text;

            _tagModels.Clear();

            int startIndex = text.IndexOf("<c=");
            while (startIndex != -1)
            {
                string subText = text.Substring(startIndex);

                MyUGUITextMeshEffectTagModel tagModel = new MyUGUITextMeshEffectTagModel();
                for (int i = _config.Infos.Count - 1; i >= 0; --i)
                {
                    MyUGUIConfigTextMeshEffectTag info = _config.Infos[i];
                    if (subText.StartsWith(string.Format("<c={0}", info.Name)))
                    {
                        int endTagIndex = subText.IndexOf(">");
                        if (endTagIndex != -1)
                        {
                            tagModel.TagLength = endTagIndex + 1;

                            string tagText = subText.Substring(0, tagModel.TagLength);
                            if (tagText.Contains(","))
                            {
                                MatchCollection matches = Regex.Matches(tagText, PATTERN_C_S_T);
                                if (matches.Count > 0)
                                {
                                    Match match = matches[0];
                                    
                                    float speed;
                                    if (float.TryParse(match.Groups[2].Value, out speed))
                                    {
                                        tagModel.IsOverrideSpeed = true;
                                        tagModel.Speed = speed;
                                    }

                                    string tiltText = match.Groups[3].Value;
                                    if (tiltText.Length >= 2)
                                    {
                                        tiltText = tiltText[0].ToString().ToUpper() + tiltText.Substring(1).ToLower();
                                        ETilt tilt;
                                        if (Enum.TryParse(tiltText, out tilt))
                                        {
                                            tagModel.Tilt = tilt;
                                        }
                                    }
                                }
                                else
                                {
                                    matches = Regex.Matches(tagText, PATTERN_C_T_S);
                                    if (matches.Count > 0)
                                    {
                                        Match match = matches[0];
                                        
                                        float speed;
                                        if (float.TryParse(match.Groups[3].Value, out speed))
                                        {
                                            tagModel.IsOverrideSpeed = true;
                                            tagModel.Speed = speed;
                                        }

                                        string tiltText = match.Groups[2].Value;
                                        if (tiltText.Length >= 2)
                                        {
                                            tiltText = tiltText[0].ToString().ToUpper() + tiltText.Substring(1).ToLower();
                                            ETilt tilt;
                                            if (Enum.TryParse(tiltText, out tilt))
                                            {
                                                tagModel.Tilt = tilt;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        matches = Regex.Matches(tagText, PATTERN_C_S);
                                        if (matches.Count > 0)
                                        {
                                            Match match = matches[0];
                                            
                                            float speed;
                                            if (float.TryParse(match.Groups[2].Value, out speed))
                                            {
                                                tagModel.IsOverrideSpeed = true;
                                                tagModel.Speed = speed;
                                            }
                                        }
                                        else
                                        {
                                            matches = Regex.Matches(tagText, PATTERN_C_T);
                                            if (matches.Count > 0)
                                            {
                                                Match match = matches[0];
                                                
                                                string tiltText = match.Groups[2].Value;
                                                if (tiltText.Length >= 2)
                                                {
                                                    tiltText = tiltText[0].ToString().ToUpper() + tiltText.Substring(1).ToLower();
                                                    ETilt tilt;
                                                    if (Enum.TryParse(tiltText, out tilt))
                                                    {
                                                        tagModel.Tilt = tilt;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        tagModel.Info = info;
                        break;
                    }
                }

                int endIndex = text.IndexOf("</c>", startIndex);
                if (endIndex == -1)
                {
                    break;
                }

                if (tagModel.Info == null)
                {
                    startIndex = text.IndexOf("<c=", endIndex);
                    continue;
                }
                
                int length = endIndex - (startIndex + tagModel.TagLength);
                text = text.Remove(endIndex, 4);
                text = text.Remove(startIndex, tagModel.TagLength);
                tagModel.StartIndex = startIndex;
                tagModel.EndIndex = startIndex + length - 1;
                _tagModels.Add(tagModel);

                startIndex = text.IndexOf("<c=");
            }
            
            _removedTagText = text;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (_tagModels.Count > 0)
            {
                _coroutine = StartCoroutine(_DoEffect(text, _tagModels.ToArray()));
            }
            else
            {
                _coroutine = null;
            }
        }

        #endregion

        #region ----- Private Method -----

        /// <summary>
        /// Process effects for each frame.
        /// </summary>
        private IEnumerator _DoEffect(string text, MyUGUITextMeshEffectTagModel[] tagModels)
        {
            _textMesh.SetText(text);
            _textMesh.ForceMeshUpdate();

            TMP_TextInfo textInfo = _textMesh.textInfo;
            Color32[] newVertexColors;

            while (_textMesh != null && text.Equals(_textMesh.text))
            {
                for (int i = 0; i < tagModels.Length; ++i)
                {
                    MyUGUITextMeshEffectTagModel tagModel = tagModels[i];

                    tagModel.LastColor = tagModel.Info.Gradient.Evaluate(0);

                    float speed = tagModel.IsOverrideSpeed ? tagModel.Speed : _speed;
                    tagModel.TotalTime += Time.deltaTime * Mathf.Abs(speed);

                    ETilt tilt = tagModel.Tilt;
                    if (tilt == ETilt.None)
                    {
                        tilt = _tilt;
                    }

                    float length = 0;
                    switch (_gradientLengthType)
                    {
                        case EGradientLength.TextLengthPlus:
                            length = (tagModel.EndIndex - tagModel.StartIndex) + _gradientLengthPlus;
                            break;

                        case EGradientLength.CharacterLength:
                            length = _gradientLength;
                            break;

                        case EGradientLength.TextLengthPercent:
                            length = (tagModel.EndIndex - tagModel.StartIndex) * _gradientLengthPercent / 100f;
                            break;
                    }
                    for (int j = tagModel.StartIndex; j <= tagModel.EndIndex; ++j)
                    {
                        float offset = (j - tagModel.StartIndex) / length;
                        float progress = (tagModel.TotalTime + (speed > 0 ? -offset : offset)) % 1f;
                        Color32 nextColor = tagModel.Info.Gradient.Evaluate(progress);

                        TMP_CharacterInfo characterInfo = textInfo.characterInfo[j];
                        int materialIndex = characterInfo.materialReferenceIndex;
                        int vertexIndex = characterInfo.vertexIndex;
                        newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                        // {}  ->  []  ->  ()  ->  <>
                        switch (tilt)
                        {
                            case ETilt.Left:
                                {
                                    // [1] [2] . (1) (2) . <1> <2>
                                    // {0} {3} . [0] [3] . (0) (3)
                                    newVertexColors[vertexIndex] = tagModel.LastColor;
                                    newVertexColors[vertexIndex + 1] = nextColor;
                                    newVertexColors[vertexIndex + 2] = nextColor; 
                                    newVertexColors[vertexIndex + 3] = tagModel.LastColor; 
                                }
                                break;

                            case ETilt.Right:
                                {
                                    // {1} {2} . [1] [2] . (1) (2)
                                    // [0] [3] . (0) (3) . <0> <3>
                                    newVertexColors[vertexIndex] = nextColor; 
                                    newVertexColors[vertexIndex + 1] = tagModel.LastColor;
                                    newVertexColors[vertexIndex + 2] = tagModel.LastColor; 
                                    newVertexColors[vertexIndex + 3] = nextColor;
                                }
                                break;
                            
                            default:
                                {
                                    // {1} [2] . [1] (2) . (1) <2>
                                    // {0} [3] . [0] (3) . (0) <3>
                                    newVertexColors[vertexIndex] = tagModel.LastColor; 
                                    newVertexColors[vertexIndex + 1] = tagModel.LastColor; 
                                    newVertexColors[vertexIndex + 2] = nextColor; 
                                    newVertexColors[vertexIndex + 3] = nextColor;
                                }
                                break;
                        }
                        _textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                        tagModel.LastColor = nextColor;
                    }
                }

                yield return null;
            }
        }

        #endregion

        #region ----- Enumeration -----

        public enum ETilt
        {
            None,
            Straight,
            Left,
            Right,
        }

        public enum EGradientLength
        {
            TextLengthPlus,
            CharacterLength,
            TextLengthPercent,
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MyUGUITextMeshEffect))]
    public class MyUGUITextMeshEffectEditor : Editor
    {
        private MyUGUITextMeshEffect _script;
        private SerializedProperty _tilt;
        private SerializedProperty _speed;
        private SerializedProperty _gradientLengthType;
        private SerializedProperty _gradientLengthPlus;
        private SerializedProperty _gradientLength;
        private SerializedProperty _gradientLengthPercent;

        private MyUGUIConfigTextMeshEffect _config;

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            _script = (MyUGUITextMeshEffect)target;
            _tilt = serializedObject.FindProperty("_tilt");
            _speed = serializedObject.FindProperty("_speed");
            _gradientLengthType = serializedObject.FindProperty("_gradientLengthType");
            _gradientLengthPlus = serializedObject.FindProperty("_gradientLengthPlus");
            _gradientLength = serializedObject.FindProperty("_gradientLength");
            _gradientLengthPercent = serializedObject.FindProperty("_gradientLengthPercent");

            TextMeshProUGUI textMesh = _script.gameObject.GetComponent<TextMeshProUGUI>();
            if (textMesh.text.Equals("New Text"))
            {
                textMesh.text = "<c=rainbow,s=1,t=right>Rainbow of Hope</c>\n"
                                +"<c=silver,s=-0.75,t=left>Silver Spoon</c>\n"
                                +"<c=gold>Golden Fork</c>\n"
                                +"<c=diamond,t=right>Sparking Diamond</c>\n";
                serializedObject.ApplyModifiedProperties();
            }

            string filePath = "Assets/Resources/" + MyUGUIManagerBase.CONFIG_DIRECTORY + typeof(MyUGUIConfigTextMeshEffect).Name + ".asset";
            _config = AssetDatabase.LoadAssetAtPath(filePath, typeof(MyUGUIConfigTextMeshEffect)) as MyUGUIConfigTextMeshEffect;
            if (_config == null)
            {
                _config = ScriptableObject.CreateInstance<MyUGUIConfigTextMeshEffect>();

                {
                    MyUGUIConfigTextMeshEffectTag tagRainBow = new MyUGUIConfigTextMeshEffectTag();
                    tagRainBow.Name = "rainbow";
                    GradientColorKey[] colorKeys = new GradientColorKey[6];
                    colorKeys[0] = new GradientColorKey(new Color(255 / 255f, 0, 0), 0);
                    colorKeys[1] = new GradientColorKey(new Color(255 / 255f, 127 / 255f, 0), 1 / 6f);
                    colorKeys[2] = new GradientColorKey(new Color(255 / 255f, 255 / 255f, 0), 2 / 6f);
                    colorKeys[3] = new GradientColorKey(new Color(0, 255 / 255f, 0), 3 / 6f);
                    colorKeys[4] = new GradientColorKey(new Color(0, 0, 255 / 255f), 4 / 6f);
                    colorKeys[5] = new GradientColorKey(new Color(75 / 255f, 0, 130 / 255f), 5 / 6f);
                    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
                    alphaKeys[0] = new GradientAlphaKey(1f, 1f);
                    alphaKeys[1] = new GradientAlphaKey(0, 1f);
                    tagRainBow.Gradient = new Gradient();
                    tagRainBow.Gradient.SetKeys(colorKeys, alphaKeys);
                    _config.Infos.Add(tagRainBow);
                }

                {
                    MyUGUIConfigTextMeshEffectTag tagSilver = new MyUGUIConfigTextMeshEffectTag();
                    tagSilver.Name = "silver";
                    GradientColorKey[] colorKeys = new GradientColorKey[4];
                    colorKeys[0] = new GradientColorKey(new Color(200 / 255f, 200 / 255f, 200 / 255f), 0);
                    colorKeys[1] = new GradientColorKey(new Color(240 / 255f, 240 / 255f, 240 / 255f), 0.05f);
                    colorKeys[2] = new GradientColorKey(new Color(200 / 255f, 200 / 255f, 200 / 255f), 0.1f);
                    colorKeys[3] = new GradientColorKey(new Color(160 / 255f, 160 / 255f, 160 / 255f), 1f);
                    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
                    alphaKeys[0] = new GradientAlphaKey(255 / 255f, 0);
                    alphaKeys[1] = new GradientAlphaKey(200 / 255f, 0.05f);
                    alphaKeys[2] = new GradientAlphaKey(255 / 255f, 0.1f);
                    alphaKeys[3] = new GradientAlphaKey(255 / 255f, 1f);
                    tagSilver.Gradient = new Gradient();
                    tagSilver.Gradient.SetKeys(colorKeys, alphaKeys);
                    _config.Infos.Add(tagSilver);
                }

                {
                    MyUGUIConfigTextMeshEffectTag tagGold = new MyUGUIConfigTextMeshEffectTag();
                    tagGold.Name = "gold";
                    GradientColorKey[] colorKeys = new GradientColorKey[4];
                    colorKeys[0] = new GradientColorKey(new Color(255 / 255f, 190 / 255f, 0), 0);
                    colorKeys[1] = new GradientColorKey(new Color(255 / 255f, 240 / 255f, 0), 0.05f);
                    colorKeys[2] = new GradientColorKey(new Color(255 / 255f, 190 / 255f, 0), 0.1f);
                    colorKeys[3] = new GradientColorKey(new Color(255 / 255f, 130 / 255f, 0), 1f);
                    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
                    alphaKeys[0] = new GradientAlphaKey(255 / 255f, 0);
                    alphaKeys[1] = new GradientAlphaKey(200 / 255f, 0.05f);
                    alphaKeys[2] = new GradientAlphaKey(255 / 255f, 0.1f);
                    alphaKeys[3] = new GradientAlphaKey(255 / 255f, 1f);
                    tagGold.Gradient = new Gradient();
                    tagGold.Gradient.SetKeys(colorKeys, alphaKeys);
                    _config.Infos.Add(tagGold);
                }

                {
                    MyUGUIConfigTextMeshEffectTag tagDiamond = new MyUGUIConfigTextMeshEffectTag();
                    tagDiamond.Name = "diamond";
                    GradientColorKey[] colorKeys = new GradientColorKey[4];
                    colorKeys[0] = new GradientColorKey(new Color(0, 170 / 255f, 255 / 255f), 0);
                    colorKeys[1] = new GradientColorKey(new Color(0, 220 / 255f, 255 / 255f), 0.05f);
                    colorKeys[2] = new GradientColorKey(new Color(0, 170 / 255f, 255 / 255f), 0.1f);
                    colorKeys[3] = new GradientColorKey(new Color(0, 120 / 255f, 255 / 255f), 1f);
                    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
                    alphaKeys[0] = new GradientAlphaKey(255 / 255f, 0);
                    alphaKeys[1] = new GradientAlphaKey(200 / 255f, 0.05f);
                    alphaKeys[2] = new GradientAlphaKey(255 / 255f, 0.1f);
                    alphaKeys[3] = new GradientAlphaKey(255 / 255f, 1f);
                    tagDiamond.Gradient = new Gradient();
                    tagDiamond.Gradient.SetKeys(colorKeys, alphaKeys);
                    _config.Infos.Add(tagDiamond);
                }

                AssetDatabase.CreateAsset(_config, filePath);
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(_script), typeof(MyUGUITextMeshEffect), false);

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Override");
            EditorGUI.indentLevel++;
            _tilt.enumValueIndex = (int)(MyUGUITextMeshEffect.ETilt)EditorGUILayout.EnumPopup("Tilt", (MyUGUITextMeshEffect.ETilt)_tilt.enumValueIndex);
            _speed.floatValue = EditorGUILayout.FloatField("Speed", _speed.floatValue);
            _gradientLengthType.enumValueIndex =  (int)(MyUGUITextMeshEffect.EGradientLength)EditorGUILayout.EnumPopup("Gradient Length", (MyUGUITextMeshEffect.EGradientLength)_gradientLengthType.enumValueIndex);
            switch (_gradientLengthType.enumValueIndex)
            {
                case (int)MyUGUITextMeshEffect.EGradientLength.TextLengthPlus:
                    _gradientLengthPlus.intValue = EditorGUILayout.IntField("Extra Character Length", _gradientLengthPlus.intValue);
                    break;

                case (int)MyUGUITextMeshEffect.EGradientLength.CharacterLength:
                    _gradientLength.intValue = EditorGUILayout.IntField("Character Length", _gradientLength.intValue);
                    break;

                case (int)MyUGUITextMeshEffect.EGradientLength.TextLengthPercent:
                    _gradientLengthPercent.floatValue = EditorGUILayout.FloatField("Length Percent", _gradientLengthPercent.floatValue);
                    break;
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            if (_config.IsFoldOut)
            {
                int countTag = _config.Infos.Count;

                EditorGUILayout.BeginHorizontal();
                _config.IsFoldOut = EditorGUILayout.Foldout(_config.IsFoldOut, "Tags (" + countTag + ")");
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    _config.Infos.Add(new MyUGUIConfigTextMeshEffectTag()
                    {
                        Name = "NewTag",
                        Gradient = new Gradient()
                    });
                }
                GUI.enabled = countTag > 0;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    _config.Infos.RemoveAt(countTag - 1);
                }
                GUI.enabled = true;
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel++;
                for (int i = 0; i < countTag; ++i)
                {
                    EditorGUILayout.LabelField(i.ToString());
                    
                    EditorGUI.indentLevel++;
                    MyUGUIConfigTextMeshEffectTag tag = _config.Infos[i];
                    tag.Name = EditorGUILayout.TextField("Name", tag.Name);
                    tag.Gradient = EditorGUILayout.GradientField("Gradient", tag.Gradient);
                    _config.Infos[i] = tag;
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }
            else
            {
                _config.IsFoldOut = EditorGUILayout.Foldout(_config.IsFoldOut, "Tags (" + _config.Infos.Count + ")");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}