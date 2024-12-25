using MyClasses;
using UnityEngine;

#pragma warning disable 0219
#pragma warning disable 0414

public class MyClasses_Benchmark_Script_MyLogger : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 5000;
    [SerializeField] private string _note = "- Conditional attribute works like a magic\n- Add or remove define symbol 'MY_LOGGER_ALL' to check result";
    
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

    [Benchmark]
    public void NonCall()
    {
        for (int i = 0; i < _iteration; ++i)
        {
        }
    }

    [Benchmark]
    public void ArgumentPreparation()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            string className = "Class";
            string methodName = "Method";
            string log = "Log" + i;
        }
    }

    [Benchmark]
    public void MyLogger()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            MyClasses.MyLogger.Info("Class", "Method", "Log" + i);
        }
    }

    [Benchmark]
    public void Extension()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            this.LogInfo("Method", "Log" + i);
        }
    }

    #endregion
}