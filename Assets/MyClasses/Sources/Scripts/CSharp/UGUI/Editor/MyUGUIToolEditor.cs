/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyUGUIToolEditor (version 2.36)
 */

#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MyClasses.UI.Tool
{
    public class MyUGUIToolEditor
    {
        #region ----- Setup -----

        /// <summary>
        /// Create a game object with Event System attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create EventSystem (Menu Bar->GameObject->UI->EventSystem)", false, 1)]
        public static void CreateEventSystem()
        {
            Debug.Log("[MyClasses] Menu Bar -> GameObject -> UI -> EventSystem.");
        }

        /// <summary>
        /// Create a game object with Camera attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create UICamera", false, 2)]
        public static void CreateUICamera()
        {
            GameObject goCamera = new GameObject("UICamera", typeof(Camera));
            goCamera.AddComponent<Camera>();
            goCamera.transform.localPosition = new Vector3(0, 0, -10);
            Camera camera = goCamera.GetComponent<Camera>();
            camera.clearFlags = CameraClearFlags.Depth;
            camera.cullingMask |= LayerMask.GetMask("UI");
            goCamera.AddComponent<AudioListener>();

            EditorGUIUtility.PingObject(goCamera);
            Selection.activeGameObject = goCamera;

            Debug.Log("[MyClasses] UICamera was created.");
        }

        /// <summary>
        /// Create portrait Canvases.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create Portrait Canvases (Screen Space - Overlay)", false, 3)]
        public static void CreatePortraitCanvasesOverlay()
        {
            GameObject goCanvas = new GameObject("Canvas", typeof(Canvas));
            goCanvas.layer = LayerMask.NameToLayer("UI");
            Canvas canvasCanvas = goCanvas.GetComponent<Canvas>();
            canvasCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasCanvas.sortingOrder = -1000;
            CanvasScaler canvasCanvasScaler = goCanvas.AddComponent<CanvasScaler>();
            canvasCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasCanvasScaler.referenceResolution = new Vector2(1080, 2340);
            canvasCanvasScaler.matchWidthOrHeight = 1;
            GraphicRaycaster canvasGraphicRaycaster = goCanvas.AddComponent<GraphicRaycaster>();
            canvasGraphicRaycaster.ignoreReversedGraphics = true;
            canvasGraphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            GameObject goCanvasOnTop = GameObject.Instantiate(goCanvas);
            goCanvasOnTop.name = "CanvasOnTop";
            Canvas canvasOnTopCanvas = goCanvasOnTop.GetComponent<Canvas>();
            canvasOnTopCanvas.sortingOrder = 1000;

            GameObject goCanvasSceneFading = GameObject.Instantiate(goCanvas);
            goCanvasSceneFading.name = "CanvasSceneFading";
            Canvas canvasSceneFadingCanvas = goCanvasSceneFading.GetComponent<Canvas>();
            canvasSceneFadingCanvas.sortingOrder = 10000;
            CanvasScaler canvasSceneFadingCanvasScaler = goCanvasSceneFading.GetComponent<CanvasScaler>();
            canvasSceneFadingCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            EditorGUIUtility.PingObject(goCanvas);
            Selection.activeGameObject = goCanvas.gameObject;

            Debug.Log("[MyClasses] Portrait Canvases (Screen Space - Overlay) was created.");
        }

        /// <summary>
        /// Create landscape Canvases.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create Landscape Canvases (Screen Space - Overlay)", false, 4)]
        public static void CreateLandscapeCanvasesOverlay()
        {
            GameObject goCanvas = new GameObject("Canvas", typeof(Canvas));
            goCanvas.layer = LayerMask.NameToLayer("UI");
            Canvas canvasCanvas = goCanvas.GetComponent<Canvas>();
            canvasCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasCanvas.sortingOrder = -1000;
            CanvasScaler canvasCanvasScaler = goCanvas.AddComponent<CanvasScaler>();
            canvasCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasCanvasScaler.referenceResolution = new Vector2(2340, 1080);
            canvasCanvasScaler.matchWidthOrHeight = 1;
            GraphicRaycaster canvasGraphicRaycaster = goCanvas.AddComponent<GraphicRaycaster>();
            canvasGraphicRaycaster.ignoreReversedGraphics = true;
            canvasGraphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            GameObject goCanvasOnTop = GameObject.Instantiate(goCanvas);
            goCanvasOnTop.name = "CanvasOnTop";
            Canvas canvasOnTopCanvas = goCanvasOnTop.GetComponent<Canvas>();
            canvasOnTopCanvas.sortingOrder = 1000;

            GameObject goCanvasSceneFading = GameObject.Instantiate(goCanvas);
            goCanvasSceneFading.name = "CanvasSceneFading";
            Canvas canvasSceneFadingCanvas = goCanvasSceneFading.GetComponent<Canvas>();
            canvasSceneFadingCanvas.sortingOrder = 10000;
            CanvasScaler canvasSceneFadingCanvasScaler = goCanvasSceneFading.GetComponent<CanvasScaler>();
            canvasSceneFadingCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            EditorGUIUtility.PingObject(goCanvas);
            Selection.activeGameObject = goCanvas.gameObject;

            Debug.Log("[MyClasses] Landscape Canvases (Screen Space - Overlay) was created.");
        }

        /// <summary>
        /// Create portrait Canvases.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create Portrait Canvases (Screen Space - Camera)", false, 5)]
        public static void CreatePortraitCanvasesCamera()
        {
            GameObject goCamera = MyUtilities.FindObjectFromRoot("UICamera");
            Camera camera = goCamera != null ? goCamera.GetComponent<Camera>() : null;
            if (camera != null)
            {
                camera.transform.position = new Vector3(540, 1140, -935.3079f);
            }

            GameObject goCanvas = new GameObject("Canvas", typeof(Canvas));
            goCanvas.layer = LayerMask.NameToLayer("UI");
            Canvas canvasCanvas = goCanvas.GetComponent<Canvas>();
            canvasCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasCanvas.worldCamera = camera;
            canvasCanvas.planeDistance = 935.3079f;
            canvasCanvas.sortingOrder = -1000;
            CanvasScaler canvasCanvasScaler = goCanvas.AddComponent<CanvasScaler>();
            canvasCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasCanvasScaler.referenceResolution = new Vector2(1080, 2340);
            canvasCanvasScaler.matchWidthOrHeight = 1;
            GraphicRaycaster canvasGraphicRaycaster = goCanvas.AddComponent<GraphicRaycaster>();
            canvasGraphicRaycaster.ignoreReversedGraphics = true;
            canvasGraphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            GameObject goCanvasOnTop = GameObject.Instantiate(goCanvas);
            goCanvasOnTop.name = "CanvasOnTop";
            Canvas canvasOnTopCanvas = goCanvasOnTop.GetComponent<Canvas>();
            canvasOnTopCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasOnTopCanvas.worldCamera = camera;
            canvasOnTopCanvas.planeDistance = 935.3079f;
            canvasOnTopCanvas.sortingOrder = 1000;

            GameObject goCanvasSceneFading = GameObject.Instantiate(goCanvas);
            goCanvasSceneFading.name = "CanvasSceneFading";
            Canvas canvasSceneFadingCanvas = goCanvasSceneFading.GetComponent<Canvas>();
            canvasSceneFadingCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasSceneFadingCanvas.worldCamera = camera;
            canvasSceneFadingCanvas.planeDistance = 935.3079f;
            canvasSceneFadingCanvas.sortingOrder = 10000;
            CanvasScaler canvasSceneFadingCanvasScaler = goCanvasSceneFading.GetComponent<CanvasScaler>();
            canvasSceneFadingCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            EditorGUIUtility.PingObject(goCanvas);
            Selection.activeGameObject = goCanvas.gameObject;

            Debug.Log("[MyClasses] Portrait Canvases (Screen Space - Camera) was created.");
        }

        /// <summary>
        /// Create landscape Canvases.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create Landscape Canvases (Screen Space - Camera)", false, 6)]
        public static void CreateLandscapeCanvasesCamera()
        {
            GameObject goCamera = MyUtilities.FindObjectFromRoot("UICamera");
            Camera camera = goCamera != null ? goCamera.GetComponent<Camera>() : null;
            if (camera != null)
            {
                camera.transform.position = new Vector3(1140, 540, -935.3079f);
            }

            GameObject goCanvas = new GameObject("Canvas", typeof(Canvas));
            goCanvas.layer = LayerMask.NameToLayer("UI");
            Canvas canvasCanvas = goCanvas.GetComponent<Canvas>();
            canvasCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasCanvas.worldCamera = camera;
            canvasCanvas.planeDistance = 935.3079f;
            canvasCanvas.sortingOrder = -1000;
            CanvasScaler canvasCanvasScaler = goCanvas.AddComponent<CanvasScaler>();
            canvasCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasCanvasScaler.referenceResolution = new Vector2(2340, 1080);
            canvasCanvasScaler.matchWidthOrHeight = 1;
            GraphicRaycaster canvasGraphicRaycaster = goCanvas.AddComponent<GraphicRaycaster>();
            canvasGraphicRaycaster.ignoreReversedGraphics = true;
            canvasGraphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            GameObject goCanvasOnTop = GameObject.Instantiate(goCanvas);
            goCanvasOnTop.name = "CanvasOnTop";
            Canvas canvasOnTopCanvas = goCanvasOnTop.GetComponent<Canvas>();
            canvasOnTopCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasOnTopCanvas.worldCamera = camera;
            canvasOnTopCanvas.planeDistance = 935.3079f;
            canvasOnTopCanvas.sortingOrder = 1000;

            GameObject goCanvasSceneFading = GameObject.Instantiate(goCanvas);
            goCanvasSceneFading.name = "CanvasSceneFading";
            Canvas canvasSceneFadingCanvas = goCanvasSceneFading.GetComponent<Canvas>();
            canvasSceneFadingCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvasSceneFadingCanvas.worldCamera = camera;
            canvasSceneFadingCanvas.planeDistance = 935.3079f;
            canvasSceneFadingCanvas.sortingOrder = 10000;
            CanvasScaler canvasSceneFadingCanvasScaler = goCanvasSceneFading.GetComponent<CanvasScaler>();
            canvasSceneFadingCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            EditorGUIUtility.PingObject(goCanvas);
            Selection.activeGameObject = goCanvas.gameObject;

            Debug.Log("[MyClasses] Landscape Canvases (Screen Space - Camera) was created.");
        }

        /// <summary>
        /// Open UI Config ID Panel.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Config ID", false, 21)]
        public static void OpenConfigID()
        {
            EditorWindow.GetWindow(typeof(MyUGUIConfigIDEditorWindow));
        }

        /// <summary>
        /// Open UI Config Scene Panel.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Config Scene", false, 22)]
        public static void OpenConfigScene()
        {
            EditorWindow.GetWindow(typeof(MyUGUIConfigSceneEditorWindow));
        }

        /// <summary>
        /// Open UI Config Popup Panel.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Config Popup", false, 23)]
        public static void OpenConfigPopup()
        {
            EditorWindow.GetWindow(typeof(MyUGUIConfigPopupEditorWindow));
        }

        /// <summary>
        /// Open UI Config Toast Notification Panel.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Config Toast Notification", false, 24)]
        public static void OpenConfigToastNotification()
        {
            EditorWindow.GetWindow(typeof(MyUGUIConfigToastNotificationEditorWindow));
        }

        /// <summary>
        /// Create a game object with MyUGUIBooter attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Setup/Create MyUGUIBooter", false, 35)]
        public static void CreateMyUGUIConfig()
        {
            GameObject obj = new GameObject("MyUGUIBooter");

            obj.AddComponent<MyUGUIBooter>();

            EditorGUIUtility.PingObject(obj);
            Selection.activeGameObject = obj.gameObject;

            Debug.Log("[MyClasses] " + typeof(MyUGUIBooter) + " was created.");
        }

        #endregion

        #region ----- Create -----

        /// <summary>
        /// Create a sample MyUGUIHUD script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/HUD", false, 1)]
        public static void CreateMyUGUIHUD()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourHUD", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleHUD.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleHUD", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUIScene script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Scene", false, 2)]
        public static void CreateMyUGUIScene()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourScene", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleScene.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleScene", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUISubScene script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Sub Scene", false, 3)]
        public static void CreateMyUGUISubScene()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourSubScene", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleSubScene.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleSubScene", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUIPopup script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Popup", false, 4)]
        public static void CreateMyUGUIPopup()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourPopup", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISamplePopup.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISamplePopup", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUIToastNotification script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Toast Notification", false, 5)]
        public static void CreateMyUGUIToastNotification()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourToastNotification", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleToastNotification.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleToastNotification", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUISampleReusableListItemView script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Reusable List Item View (Fixed Size)", false, 6)]
        public static void CreateMyUGUIReusableListViewItem()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourListItemView", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleReusableListItemView.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleReusableListItemView", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a sample MyUGUISampleReusableListItemView2 script.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Script/Reusable List Item View 2 (Dynamic Size)", false, 7)]
        public static void CreateMyUGUIReusableListViewItem2()
        {
            string directory = "Assets";
            Object selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(DefaultAsset))
            {
                directory = AssetDatabase.GetAssetPath(selectedObject);
            }

            string filePath = EditorUtility.SaveFilePanel("Save Text File", directory, "YourListItemView2", "cs");
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            fileName = fileName.Substring(0, fileName.Length - 3);

            string[] samplePaths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < samplePaths.Length; i++)
            {
                string samplePath = samplePaths[i] + "/Sources/Scripts/CSharp/UGUI/Sample/MyUGUISampleReusableListItemView2.cs";
                if (File.Exists(samplePath))
                {
                    string content = File.ReadAllText(samplePath);
                    content = content.Substring(content.IndexOf("using")).Replace("MyUGUISampleReusableListItemView2", fileName);
                    File.WriteAllText(filePath, content);
                    AssetDatabase.Refresh();
                    Debug.Log("[MyClasses] " + fileName + ".cs was created.");
                    return;
                }
            }
        }

        /// <summary>
        /// Create a game object with MyUGUIButton & Text attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Text Button - Color Tint", false, 1)]
        public static void CreateMyUGUIButtonColorTint()
        {
			GameObject go = MyUGUIButton.CreateTextTemplate();

            MyUGUIButton button = go.GetComponent<MyUGUIButton>();
            button.Button.transition = Selectable.Transition.ColorTint;
            button.PressAnimationType = MyUGUIButton.EAnimationType.None;
            button.ClickAnimationType = MyUGUIButton.EAnimationType.None;

            Debug.Log("[MyClasses] Text Button - Color Tint was created.");
		}

		/// <summary>
		/// Create a game object with MyUGUIButton & Text attached.
		/// </summary>
		[MenuItem("MyClasses/UGUI/Create/Game Object/Text Button - Scale Animation", false, 2)]
        public static void CreateMyUGUIButtonScaleAnimation()
		{
            GameObject go = MyUGUIButton.CreateTextTemplate();

            MyUGUIButton button = go.GetComponent<MyUGUIButton>();
            button.Button.transition = Selectable.Transition.None;
            button.PressAnimationType = MyUGUIButton.EAnimationType.Scale;
            button.ClickAnimationType = MyUGUIButton.EAnimationType.None;

            Animator animator = go.AddComponent<Animator>();
            string[] paths = new string[] { "Assets/MyClasses", "Assets/Core/MyClasses", "Assets/Plugin/MyClasses", "Assets/Plugins/MyClasses", "Assets/Framework/MyClasses", "Assets/Frameworks/MyClasses" };
            for (int i = 0; i < paths.Length; i++)
            {
                if (System.IO.File.Exists(paths[i] + "/Sources/Animations/MyAnimatorButtonClickScale.controller"))
                {
                    animator.runtimeAnimatorController = (RuntimeAnimatorController)UnityEditor.AssetDatabase.LoadAssetAtPath(paths[i] + "/Sources/Animations/MyAnimatorButtonClickScale.controller", typeof(RuntimeAnimatorController));
                    break;
                }
            }

            Debug.Log("[MyClasses] Text Button - Scale Animation was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIButton & TextMeshPro attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/TextMeshPro Button - Color Tint", false, 3)]
        public static void CreateMyUGUITextMeshProButtonColorTint()
		{
            GameObject go = MyUGUIButton.CreateTextMeshProTemplate();

            MyUGUIButton button = go.GetComponent<MyUGUIButton>();
            button.Button.transition = Selectable.Transition.ColorTint;
            button.PressAnimationType = MyUGUIButton.EAnimationType.None;
            button.ClickAnimationType = MyUGUIButton.EAnimationType.None;

            Debug.Log("[MyClasses] TextMeshPro Button - Color Tint was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIButton & TextMeshPro attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/TextMeshPro Button - Scale Animation", false, 4)]
        public static void CreateMyUGUITextMeshProButtonScaleAnimation()
		{
            GameObject go = MyUGUIButton.CreateTextMeshProTemplate();

            MyUGUIButton button = go.GetComponent<MyUGUIButton>();
            button.Button.transition = Selectable.Transition.None;
            button.PressAnimationType = MyUGUIButton.EAnimationType.Scale;
            button.ClickAnimationType = MyUGUIButton.EAnimationType.None;

            Debug.Log("[MyClasses] TextMeshPro Button - Scale Animation was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIToggleButton attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Toggle Button", false, 5)]
        public static void CreateMyUGUIToggleButton()
		{
            MyUGUIToggleButton.CreateTemplate();

            Debug.Log("[MyClasses] Toggle Button was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIReusableListView attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Reusable List View (Fixed Item Size)", false, 6)]
        public static void CreateMyUGUIReusableListView()
		{
            MyUGUIReusableListView.CreateTemplate();

            Debug.Log("[MyClasses] Reusable List View (Fixed Item Size) was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIReusableListView2 attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Reusable List View 2 (Dynamic Item Size) - Horizontal", false, 7)]
        public static void CreateMyUGUIReusableListView2Horizontal()
		{
            MyUGUIReusableListView2.CreateTemplate(true);

            Debug.Log("[MyClasses] Reusable List View 2 (Dynamic Item Size) - Horizontal was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIReusableListView2 attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Reusable List View 2 (Dynamic Item Size) - Vertical", false, 8)]
        public static void CreateMyUGUIReusableListView2Vertical()
		{
            MyUGUIReusableListView2.CreateTemplate(false);

            Debug.Log("[MyClasses] Reusable List View 2 (Dynamic Item Size) - Vertical was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUICircle attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Circle", false, 19)]
        public static void CreateMyUGUICircle()
		{
            MyUGUICircle.CreateTemplate();

            Debug.Log("[MyClasses] Circle was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUILine attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Line", false, 19)]
        public static void CreateMyUGUILine()
		{
            MyUGUILine.CreateTemplate();

            Debug.Log("[MyClasses] Line was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIPieChart attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Pie Chart", false, 19)]
        public static void CreateMyUGUIPieChart()
		{
            MyUGUIPieChart.CreateTemplate();

            Debug.Log("[MyClasses] Pie Chart was created.");
        }

        /// <summary>
        /// Create a game object with MyUGUIRadarChart attached.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Create/Game Object/Radar Chart", false, 19)]
        public static void CreateMyUGUIRadarChart()
		{
            MyUGUIRadarChart.CreateTemplate();

            Debug.Log("[MyClasses] Radar Chart was created.");
        }

        #endregion

		#region ----- Utilities -----

        /// <summary>
        /// Invoke all "Anchor Now" buttons in current scene.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Utilities/Refresh All MyUGUIAnchors Now", false, 1)]
        public static void OrientationAnchorAllNow()
        {
            MyUGUIAnchor[] scripts = GameObject.FindObjectsOfType<MyUGUIAnchor>();
            foreach (var item in scripts)
            {
                item.Anchor();
            }

            Debug.Log("[MyClasses] All \"Anchor Now\" buttons were invoked.");
        }

        /// <summary>
        /// Invoke all "Layout Now" buttons in current scene.
        /// </summary>
        [MenuItem("MyClasses/UGUI/Utilities/Refresh All MyUGUIGridLayoutGroups Now", false, 2)]
        public static void AspectRatioScaleAllNow()
        {
            MyUGUIGridLayoutGroup[] scripts = GameObject.FindObjectsOfType<MyUGUIGridLayoutGroup>();
            foreach (var item in scripts)
            {
                item.Layout();
            }

            Debug.Log("[MyClasses] All \"Layout Now\" buttons were invoked.");
        }

		#endregion
    }
}

#endif