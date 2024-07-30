using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_ComparisonOperators : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 100000000;
    [SerializeField] private string _note = "Avoid LessThanOrEqual and GreaterThanOrEqual if possible";

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
    public void Equal()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i == 61691;
        }
    }

    [Benchmark]
    public void Negative_Equal()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = !(i == 61691);
        }
    }

    [Benchmark]
    public void NotEqual()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i != 61691;
        }
    }

    [Benchmark]
    public void LessThan()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i < 61691;
        }
    }

    [Benchmark]
    public void LessThanOrEqual()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i <= 61691;
        }
    }

    [Benchmark]
    public void GreaterThan()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i > 61691;
        }
    }

    [Benchmark]
    public void GreaterThanOrEqual()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            bool result = i >= 61691;
        }
    }

    #endregion
}