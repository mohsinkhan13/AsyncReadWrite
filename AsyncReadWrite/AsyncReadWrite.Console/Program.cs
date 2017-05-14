using AsyncReadWrite.IO;
using System;
using System.Threading;
using System.Threading.Tasks;
using console = System.Console;

namespace AsyncReadWrite.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationSource = new CancellationTokenSource(); 
            var writer = new FileWriter();
            writer.CancellationToken = cancellationSource.Token;
            writer.CancellationToken.Register(() => 
                console.WriteLine("Write & Refresh Operation Cancelled."));

            var reader = new FileReader();
            reader.CancellationToken = cancellationSource.Token;
            reader.CancellationToken.Register(() =>
                console.WriteLine("Read Operation Cancelled."));

            try
            {
                Task.Run(async () =>
                    {
                        await writer.Write(@"testFile.txt", 5);

                    }, writer.CancellationToken)
                    .Wait(1000);// added 1 sec delay just for the 1st write to complete before reading

                Task.Run(async () =>
                {
                    await reader.Read(@"testFile.txt", 10);

                }, reader.CancellationToken);

                Task.Run(async () =>
                {
                    await writer.Refresh(15);

                }, writer.CancellationToken);

            }
            catch (AggregateException e)
            {
                foreach (var exception in e.Flatten().InnerExceptions)
                {
                    console.WriteLine(exception.Message);
                }
            }

            console.WriteLine("Press Escape to cancel Read Write operations");

            ConsoleKeyInfo key = new ConsoleKeyInfo(); 
            while (key.Key != ConsoleKey.Escape)
            {
                key = console.ReadKey();
            }
            cancellationSource.Cancel();
            console.ReadLine();

        }
    }
}
