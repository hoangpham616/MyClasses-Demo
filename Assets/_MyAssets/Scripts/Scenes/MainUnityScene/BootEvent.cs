
#if UNITY_EDITOR
#define MY_UI_DEBUG
#endif

using UnityEngine;
using MyClasses;
using MyClasses.UI;

public class BootEvent : MonoBehaviour
{
    public void OnPreLoad()
    {
        this.LogInfo("OnPreLoad", "You should do something like configuration", ELogColor.DARK_UI);
    }

    public void OnCustomShow()
    {
        this.LogInfo("OnCustomShow", "You can customize the start scene", ELogColor.DARK_UI);

        if (Random.Range(0, 100) % 2 == 1)
        {
            MyUGUIManager.Instance.ShowUnityScene(EUnitySceneID.MainUnityScene, ESceneID.MainScene);
        }
        else
        {
            MyUGUIManager.Instance.ShowUnityScene(EUnitySceneID.MainUnityScene, ESceneID.GameScene);
        }
    }

    public void OnPostLoad()
    {
        this.LogInfo("OnPostLoad", "You should do something like initialize SDK", ELogColor.DARK_UI);

        Application.targetFrameRate = 60;
    }
}
