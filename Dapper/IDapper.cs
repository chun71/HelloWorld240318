using System.Data;

namespace HelloWorld240318.Dapper
{
    public interface IDapper
    {
        IEnumerable<T> Query<T>(string sql, object? dp = null, CommandType commandType = CommandType.Text, string connType = "QA");
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? dp = null, string splitOn = "ColumnName", CommandType commandType = CommandType.Text, string connType = "QA");
        int Execute(string sql, object? param, CommandType commandType = CommandType.Text, string connType = "QA");
    }
}
