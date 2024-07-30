using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_OrderOfOperation : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 10000000;
    [SerializeField] private string _note = "Alert and Attentive";

    private Vector3 _vector = new Vector3(6, 16, 91);
    
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
    public void Number_Number_Vector()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var result = 5.5f * i * _vector;
        }
    }

    [Benchmark]
    public void Number_Vector_Number()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var result = 5.5f * _vector * i;
        }
    }

    [Benchmark]
    public void Vector_Number_Number()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var result = _vector * 5.5f * i;
        }
    }

    #endregion
}