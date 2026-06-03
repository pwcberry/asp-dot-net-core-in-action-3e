namespace MyLearning.Data.Sqlite
{
    internal static class SqliteContextHelper
    {
        internal readonly static string DecimalDbType = "DECIMAL";

        internal static string GetTextDbType(int length) => $"VARCHAR({length})";
    }
}