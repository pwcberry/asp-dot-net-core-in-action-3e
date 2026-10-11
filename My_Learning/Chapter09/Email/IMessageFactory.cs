namespace MyLearning.Chapter09.Email;

public interface IMessageFactory
{
    Email CreateFromTemplate(string email);
}