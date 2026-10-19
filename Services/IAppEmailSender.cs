namespace NotesAPI.Services;
public interface IAppEmailSender
{
    Task SendAsync(string to, string subject, string body);
}

public class  ConsoleEmailSender : IAppEmailSender
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"--- EMAIL to {to} ---\n{subject}\n{body}\n---");
        return Task.CompletedTask;
    }
}