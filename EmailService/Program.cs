using Aero.Services;

namespace EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSchedulerService service = new EmailSchedulerService();
            service.ProcessEmails();
        }
    }
}
