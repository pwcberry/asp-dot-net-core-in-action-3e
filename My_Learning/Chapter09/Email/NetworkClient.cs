namespace MyLearning.Chapter09.Email;

public class NetworkClient(EmailServerSettings settings, ILogger<NetworkClient> logger)
{
    private readonly EmailServerSettings _settings = settings;
    private readonly ILogger<NetworkClient> _logger = logger;
    
    public void SendEmail(Email email)
    {
        // Simulate sending email using the provided settings
        _logger.LogInformation("Sending email to {To} with subject '{Subject}' using server {Server}", email.To, email.Subject, $"{_settings.Host}:{_settings.Port}");
        // Here you would implement the actual email sending logic, e.g., using SMTP client
    }
}