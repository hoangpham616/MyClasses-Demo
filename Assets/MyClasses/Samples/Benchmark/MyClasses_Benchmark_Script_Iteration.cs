using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_Iteration : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 20000000;
    [SerializeField] private string _editorNote = "- Array Winner: foreach\n- List Winner: Parallel.ForEach()";
    [SerializeField] private string _androidNote = "- Winner: foreach\n- Parallel is truly terrible";

    private List<int> _list;
    private int[] _array;

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
        if (_array == null)
        {
            _list = Enumerable.Range(0, _iteration).Select(x => UnityEngine.Random.Range(-1000, 1000)).ToList();
            _array = _list.ToArray();
        }
    }

    [Benchmark]
    public void Array_for()
    {
        for (int i = 0; i < _array.Length; ++i)
        {
            var item = _array[i];
        }
    }

    [Benchmark]
    public void Array_Cached_for()
    {
        for (int i = 0, length = _array.Length; i < length; ++i)
        {
            var item = _array[i];
        }
    }

    [Benchmark]
    public void Array_foreach()
    {
        foreach (int item in _array)
        {
        }
    }

    [Benchmark]
    public void Array_Parallel_ForEach()
    {
        Parallel.ForEach(_array, x => {});
    }

    [Benchmark]
    public void List_for()
    {
        for (int i = 0; i < _list.Count; ++i)
        {
            var item = _list[i];
        }
    }

    [Benchmark]
    public void List_Cached_for()
    {
        for (int i = 0, count = _list.Count; i < count; ++i)
        {
            var item = _list[i];
        }
    }

    [Benchmark]
    public void List_foreach()
    {
        foreach (int item in _list)
        {
        }
    }

    [Benchmark]
    public void List_ForEach()
    {
        _list.ForEach(x => {});
    }

    [Benchmark]
    public void List_Parallel_ForEach()
    {
        Parallel.ForEach(_list, x => {});
    }

    #endregion
}