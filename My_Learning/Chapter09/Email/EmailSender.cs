namespace MyLearning.Chapter09.Email;

public class EmailSender(IMessageFactory messageFactory, NetworkClient networkClient) : IEmailSender
{
    private readonly IMessageFactory _messageFactory = messageFactory;
    private readonly NetworkClient _networkClient = networkClient;

    public void SendEmail(string email)
    {
        SendEmail(email, null, null);
    }
    
    public void SendEmail(string email, string? subject, string? body)
    {
        if (subject is null && body is null)
        {
            var message = _messageFactory.CreateFromTemplate(email);
            _networkClient.SendEmail(message);  
        }
        else
        {
            var message = new Email(email, subject ?? "Default Subject", body ?? "Default Body");
            _networkClient.SendEmail(message);
        }
    }
}
