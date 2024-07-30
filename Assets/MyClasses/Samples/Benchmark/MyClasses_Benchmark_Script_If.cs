using UnityEngine;
using System.Linq;

#pragma warning disable 0414

public class MyClasses_Benchmark_Script_If : MyBenchmark
{
    #region ----- Variable -----

    [SerializeField] private TextMesh _textResult;
    [SerializeField] private int _numberOfTest = 5;
    [SerializeField] private int _iteration = 15000001;
    [SerializeField] private string _note = "Removing \"if\" gives a super speed up";
    [SerializeField] private string _androidNote = "- Due to the Java Compiler, bitwise is not as good as expected\n- \"Conditions\" cases are the same speed\n- \"Parallel Technique\" does not make sense";

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
            _array = Enumerable.Range(0, _iteration).Select(x => UnityEngine.Random.Range(-100, 100)).ToArray();
        }
    }

    [Benchmark]
    public void Conditions_Split()
    {
        foreach (int item in _array)
        {
            if (item % 5 == 0)
            {
                continue;
            }
            if (item % 3 == 0)
            {
                continue;
            }
        }
    }

    [Benchmark]
    public void Conditions_Merge()
    {
        foreach (int item in _array)
        {
            if (item % 5 == 0 || item % 3 == 0)
            {
                continue;
            }
        }
    }

    [Benchmark]
    public void Conditions_BranchFree()
    {
        foreach (int item in _array)
        {
            bool result = item % 7 == 0 || item % 5 == 0;
            continue;
        }
    }

    [Benchmark]
    public int SumOdd_Mod_Equal()
    {
        int sum = 0;
        for (int i = 0; i < _array.Length; ++i)
        {
            int item = _array[i];
            if (item % 2 == 1)
            {
                sum += item;
            }
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_Mod_NotEqual()
    {
        int sum = 0;
        for (int i = 0; i < _array.Length; ++i)
        {
            int item = _array[i];
            if (item % 2 != 0)
            {
                sum += item;
            }
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_BitTrick()
    {
        int sum = 0;
        for (int i = 0; i < _array.Length; ++i)
        {
            int item = _array[i];
            if ((item & 1) != 0)
            {
                sum += item;
            }
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_BranchFree() // remove "if" & "comparition operator"
    {
        int sum = 0;
        for (int i = 0; i < _array.Length; ++i)
        {
            int item = _array[i];
            int odd = item & 1;
            sum += odd * item;
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_Parallel_Mod_NotEqual()
    {
        int sum = 0;
        int length = _array.Length % 2 == 0 ? _array.Length : _array.Length - 1;
        for (int i = 0; i < length; i += 2)
        {
            int item = _array[i];
            if (item % 2 != 0)
            {
                sum += item;
            }
            int itemNext = _array[i + 1];
            if (itemNext % 2 != 0)
            {
                sum += itemNext;
            }
        }
        if (length < _array.Length)
        {
            int item = _array[length];
            if (item % 2 != 0)
            {
                sum += item;
            }
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_Parallel_BitTrick()
    {
        int sum = 0;
        int length = _array.Length % 2 == 0 ? _array.Length : _array.Length - 1;
        for (int i = 0; i < length; i += 2)
        {
            int item = _array[i];
            if ((item & 1) != 0)
            {
                sum += item;
            }
            int itemNext = _array[i + 1];
            if ((itemNext & 1) != 0)
            {
                sum += itemNext;
            }
        }
        if (length < _array.Length)
        {
            int item = _array[length];
            if ((item & 1) != 0)
            {
                sum += item;
            }
        }
        return sum;
    }

    [Benchmark]
    public int SumOdd_Parallel_BranchFree()
    {
        int sum = 0;
        int length = _array.Length % 2 == 0 ? _array.Length : _array.Length - 1;
        for (int i = 0; i < length; i += 2)
        {
            int item = _array[i];
            int nextItem = _array[i + 1];
            int odd = item & 1;
            int oddNext = nextItem & 1;
            sum += odd * item;
            sum += oddNext * nextItem;
        }
        if (length < _array.Length)
        {
            int item = _array[length];
            int odd = item & 1;
            sum += odd * item;
        }
        return sum;
    }

    #endregion
}