using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_ArithmeticOperators : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 100000000;
    [SerializeField] private string _note = "Modulo is the slowest";
    [SerializeField] private string _androidNote = "Modulo is almost x90 slower than others";

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
    public void Addition()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            int result = i + 6;
        }
    }

    [Benchmark]
    public void Subtraction()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            int result = i - 16;
        }
    }

    [Benchmark]
    public void Multiplication()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            int result = i * 19;
        }
    }

    [Benchmark]
    public void Division()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            int result = i - 91;
        }
    }

    [Benchmark]
    public void Modulo()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            int result = i % 91;
        }
    }

    #endregion
}