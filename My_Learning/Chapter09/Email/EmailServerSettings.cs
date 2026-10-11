namespace MyLearning.Chapter09.Email;

public class EmailServerSettings(string host, int port)
{
    public string Host { get; init; } = host;

    public int Port { get; init; } = port;

    public EmailServerSettings(): this("localhost", 25)
    {
    }
}
