using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_IncrementOperator : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 100000000;
    [SerializeField] private string _note = "They seem to be the same";

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
    public void PreIncrement()
    {
        for (int i = 0; i < _iteration; ++i)
        {
        }
    }

    [Benchmark]
    public void PreIncrement2()
    {
        int count = 0;
        for (int i = 0; i < _iteration; ++i)
        {
            int temp = ++count;
        }
    }

    [Benchmark]
    public void PostIncrement()
    {
        for (int i = 0; i < _iteration; i++)
        {
        }
    }

    [Benchmark]
    public void PostIncrement2()
    {
        int count = 0;
        for (int i = 0; i < _iteration; i++)
        {
            int temp = count++;
        }
    }

    #endregion
}