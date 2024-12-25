using MyClasses;
using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_String : MyBenchmark
{
    #region ----- Variable -----

    private const string FORMAT = "{0} something {1}";

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 1000000;
    [SerializeField] private string _note = "- Speed Winner: Concatenation\n- Memory Loser: Concatenation";
    
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
    public void StringConcatenation()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            string temp = i + " something " + i;
        }
    }

    [Benchmark]
    public void InterpolatedString()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            string temp = $"{i} something {i}";
        }
    }

    [Benchmark]
    public void StringFormat()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            string temp = string.Format("{0} something {1}", i, i);
        }
    }

    [Benchmark]
    public void StringFormatConst()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            string temp = string.Format(FORMAT, i, i);
        }
    }

    #endregion
}