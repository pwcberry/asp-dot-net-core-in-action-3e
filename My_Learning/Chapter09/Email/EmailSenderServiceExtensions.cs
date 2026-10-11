namespace MyLearning.Chapter09.Email;

public static class EmailSenderServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEmailSender()
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<NetworkClient>();
            services.AddSingleton<IMessageFactory, MessageFactory>();
            services.AddSingleton(new EmailServerSettings
            {
                Host = "smpt.example.com",
                Port = 25
            });
            
            return services;
        }
    }
}