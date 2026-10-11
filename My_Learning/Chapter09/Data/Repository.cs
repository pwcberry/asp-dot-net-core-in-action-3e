namespace MyLearning.Chapter09.Data;

public class Repository(DataContext dataContext)
{
    public int RowCount => dataContext.RowCount;
}
