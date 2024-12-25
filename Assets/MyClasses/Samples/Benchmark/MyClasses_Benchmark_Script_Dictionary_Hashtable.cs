using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_Dictionary_Hashtable : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 1000000;
    [SerializeField] private string _note = "- Generic Winner: Dictionary\n- Solution Winner: Add()";

    private Dictionary<int, string> _dictionary = new();
    private Dictionary<int, string> _dictionary2 = new();
    private Dictionary<int, string> _dictionary3 = new();
    private Hashtable _hashtable = new();
    private Hashtable _hashtable2 = new();

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
        _dictionary.Clear();
        _dictionary2.Clear();
        _dictionary3.Clear();
        _hashtable.Clear();
        _hashtable2.Clear();
    }

    [Benchmark]
    public void Dictionary_Add()
    {
        _dictionary.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            _dictionary.Add(i, (i - 1).ToString());
        }
    }

    [Benchmark]
    public void Dictionary_AddWithCheck()
    {
        _dictionary2.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            if (!_dictionary2.ContainsKey(i))
            {
                _dictionary2.Add(i, (i - 1).ToString());
            }
        }
    }

    [Benchmark]
    public void Dictionary_AddByIndexer()
    {
        _dictionary3.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            _dictionary3[i] = (i - 1).ToString();
        }
    }

    [Benchmark]
    public void Dictionary_Update()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            _dictionary[i] = (i + 1).ToString();
        }
    }

    [Benchmark]
    public void Dictionary_ContainsKey()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if (_dictionary.ContainsKey(i)) {}
        }
    }

    [Benchmark]
    public void Dictionary_Get()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if (_dictionary[i] != null) {}
        }
    }

    [Benchmark]
    public void Dictionary_foreach()
    {
        foreach (var key in _dictionary)
        {
        }
    }

    [Benchmark]
    public void Hastable_Add()
    {
        _hashtable.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            _hashtable.Add(i, (i - 1).ToString());
        }
    }

    [Benchmark]
    public void Hastable_AddWithCheck()
    {
        _hashtable.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            if (_hashtable.ContainsKey(i))
            {
                _hashtable.Add(i, (i - 1).ToString());
            }
        }
    }


    [Benchmark]
    public void Hashtable_AddByIndexer()
    {
        _hashtable.Clear();
        for (int i = 0; i < _iteration; ++i)
        {
            _hashtable[i] = (i - 1).ToString();
        }
    }

    [Benchmark]
    public void Hashtable_Update()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            _hashtable[i] = (i + 1).ToString();
        }
    }

    [Benchmark]
    public void Hashtable_ContainsKey()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if (_hashtable.ContainsKey(i)) {}
        }
    }

    [Benchmark]
    public void Hashtable_Get()
    {
        for (int i = 0; i < _iteration; ++i)
        {
            if (_hashtable[i] != null) {}
        }
    }

    [Benchmark]
    public void Hashtable_foreach()
    {
        foreach (var key in _hashtable)
        {
        }
    }

    #endregion
}