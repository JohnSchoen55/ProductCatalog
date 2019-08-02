using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureAppConfiguration(configBuilder =>
            {
               

            });

            hostBuilder.ConfigureServices(collection =>
            {
                collection.AddHostedService<MyHostService>();
                collection.AddTransient<ISayThings, SayThings>();
                collection.AddTransient<ISayThings, SayStuff>();

            });

            var host = hostBuilder.Build();
            host.Run();


        }
    }

    public interface ISayThings
    {
        string saySomething();

        string sayStuff();
    }

    public class SayThings : ISayThings
    {
        public string saySomething()
        {
            throw new System.NotImplementedException();

        }

        public string sayStuff()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SayStuff : ISayThings
    {
        SayStuff
        

        public string sayStuff()
        {
            throw new System.NotImplementedException();
        }
    }

}
