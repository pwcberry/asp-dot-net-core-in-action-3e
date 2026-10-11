using System.Collections;

namespace MyLearning.Chapter09.Data;

public class NumberData : IEnumerator<int>
{
    private static readonly int[] Numbers = { 42, 7, 93, 18, 64, 3, 81, 27, 56, 39 };

    private int _index = -1;

    public int Current =>
        _index >= 0 && _index < Numbers.Length
            ? Numbers[_index]
            : throw new InvalidOperationException("Enumeration has not started or has already finished.");

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_index < Numbers.Length)
        {
            _index++;
        }

        return _index < Numbers.Length;
    }

    public void Reset() => _index = -1;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}