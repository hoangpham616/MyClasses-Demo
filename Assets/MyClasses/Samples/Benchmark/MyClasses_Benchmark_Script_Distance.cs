using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_Distance : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 3000000;
    [SerializeField] private string _note = "Winner: Vector3.Distance()";

    private Vector3 _a;
    private Vector3 _b;

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
        _a = new Vector3(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f));
        _b = new Vector3(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f));
    }

    [Benchmark]
    public void Vector3_Distance()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if (Vector3.Distance(_a, _b) < 0) {}
        }
    }

    [Benchmark]
    public void Magnitude()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if ((_a - _b).magnitude < 0) {}
        }
    }

    #endregion
}