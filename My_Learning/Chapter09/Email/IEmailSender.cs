namespace MyLearning.Chapter09.Email;

public interface IEmailSender
{
    void SendEmail(string email);
    
    void SendEmail(string email, string? subject, string? body);
}