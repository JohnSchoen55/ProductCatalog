using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyHostService : IHostedService
    {
        public ISayThings SayThings { get; }

        public MyHostService(ISayThings sayThings)
            {

            sayThings = SayThings;

            }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(SayThings.saySomething());

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(SayThings.saySomething());
        }
    }
}
