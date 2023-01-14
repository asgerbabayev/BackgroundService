using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundServiceIHostedService
{
    internal class Program
    {
        static async Task  Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<BackgroundWorker>();
                }).RunConsoleAsync();
        }
    }
}