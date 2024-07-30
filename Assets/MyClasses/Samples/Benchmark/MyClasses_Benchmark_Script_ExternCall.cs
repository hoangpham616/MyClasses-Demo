using UnityEngine;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_ExternCall : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 750000;
    [SerializeField] private string _note = "Always use fully cached";
    
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
    public void Transform_Direct()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var position = transform.position;
            var rotation = transform.rotation;
            var scale = transform.localScale;
        }
    }

    [Benchmark]
    public void Transform_CachedPerTest()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var t = transform;
            var position = t.position;
            var rotation = t.rotation;
            var scale = t.localScale;
        }
    }

    [Benchmark]
    public void Transform_FullyCached() // the best
    {
        var t = transform;
        for (int i = 0; i < _iteration; ++i)
        {
            var position = t.position;
            var rotation = t.rotation;
            var scale = t.localScale;
        }
    }

    [Benchmark]
    public void MainCamera_Direct()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var aspect = Camera.main.aspect;
            var fieldOfView = Camera.main.fieldOfView;
            var backgroundColor = Camera.main.backgroundColor;
        }
    }

    [Benchmark]
    public void MainCamera_CachedPerTest()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            var camera = Camera.main;
            var aspect = camera.aspect;
            var fieldOfView = camera.fieldOfView;
            var backgroundColor = camera.backgroundColor;
        }
    }

    [Benchmark]
    public void MainCamera_FullyCached() // the best
    {
        var camera = Camera.main;
        for (int i = 0; i < _iteration; ++i)
        {
            var aspect = camera.aspect;
            var fieldOfView = camera.fieldOfView;
            var backgroundColor = camera.backgroundColor;
        }
    }

    #endregion
}