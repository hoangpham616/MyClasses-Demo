using UnityEngine;
using System.Text;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_StringBuilder : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 15000;
    [SerializeField] private string _note = "String Builder is the best";
    
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
    public string Concatenation()
    {
        string result = string.Empty;
        for (int i = 0; i < _iteration; ++i)
        {
            result += "Hello";
        }
        return result;
    }

    [Benchmark]
    public string Builder()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < _iteration; ++i)
        {
            stringBuilder.Append("Hello");
        }
        return stringBuilder.ToString();
    }

    #endregion
}