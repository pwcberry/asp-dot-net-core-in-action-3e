namespace MyLearning.Chapter09.Data;

public class DataContext(IEnumerator<int> numbers)
{
    public int RowCount => numbers.MoveNext() ? numbers.Current : 0;
}
