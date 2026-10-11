namespace MyLearning.Chapter09.Email;

public class MessageFactory : IMessageFactory
{
    public Email CreateFromTemplate(string email)
    {
        return new Email(email, "Subject", "Body");
    }
}
