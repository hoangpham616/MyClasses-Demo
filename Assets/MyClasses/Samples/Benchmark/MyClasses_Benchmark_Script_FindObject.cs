using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_FindObject : MyBenchmark
{
    #region ----- Define -----

    private readonly string TAG = "Player";

    #endregion

    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 30000;
    [SerializeField] private string _note = "- Winner: FindWithTag()\n- FindOfType() is truly terrible";
    [SerializeField] private string _editorNote = "for is faster than Find()";
    [SerializeField] private string _androidNote = "Find() better than for";

    #endregion

    #region ----- MonoBehaviour Implementation -----

    void OnEnable()
    {
        StartBenchmark(_numberOfTest, 0.5f, (result) =>
        {
            _textResult.text = result;
        });
    }

    #endregion

    #region ----- MyBenchmark Implementation -----

    [Setup]
    public void Setup()
    {
        if (transform.childCount == 0)
        {
            Transform transformRoot = transform;
            for (int i = 0; i < 10; ++i)
            {
                Transform transformA = new GameObject($"A {i}").transform;
                transformA.SetParent(transformRoot);
                for (int j = 0; j < 10; ++j)
                {
                    Transform transformB = new GameObject($"B {j}").transform;
                    transformB.SetParent(transformA);
                    for (int k = 0; k < 10; ++k)
                    {
                        GameObject gameObjectC = new GameObject($"C {k}");
                        gameObjectC.transform.SetParent(transformB);
                        switch (i + j + k % 7)
                        {
                            case 5:
                                gameObjectC.AddComponent<MyClasses_Benchmark_Script_FindObject_Helper>();
                                break;
                            case 2:
                                gameObjectC.tag = TAG;
                                break;
                            case 0:
                                gameObjectC.AddComponent<MyClasses_Benchmark_Script_FindObject_Helper>();
                                gameObjectC.tag = TAG;
                                break;
                        }
                    }
                }
            }
        }
    }

    [Benchmark]
    public void Object_for()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            Transform t = transform;
            for (int j = 0, count = t.childCount; j < count; ++j)
            {
                Transform child = t.GetChild(j);
                if (child.name.Equals("A 7"))
                {
                    GameObject gameObject = child.gameObject;
                    break;
                }
            }
        }
    }

    [Benchmark]
    public void Object_transform_Find()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            GameObject gameObject = transform.Find("A 7").gameObject;
        }
    }

    [Benchmark]
    public void Object_Find()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            GameObject gameObject = GameObject.Find("A 7");
        }
    }

    [Benchmark]
    public void Object_FindGameObjectWithTag()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(TAG);
        }
    }

    [Benchmark]
    public void Object_FindFirstObjectByType()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            MyClasses_Benchmark_Script_FindObject_Helper script = FindFirstObjectByType<MyClasses_Benchmark_Script_FindObject_Helper>();
        }
    }

    [Benchmark]
    public void ObjectArray_FindGameObjectsWithTag()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(TAG);
        }
    }

    [Benchmark]
    public void ObjectArray_FindObjectsOfType()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            MyClasses_Benchmark_Script_FindObject_Helper[] scripts = FindObjectsOfType<MyClasses_Benchmark_Script_FindObject_Helper>();
        }
    }

    #endregion
}