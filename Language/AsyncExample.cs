using System;
using System.Threading;
using System.Threading.Tasks;

namespace Language
{
    class AsyncExample
    {
        static void Main(string[] args)
        {
            var program = new AsyncExample();
            program.DoWork();
            var r2 = program.DoWorkStr();
            Console.WriteLine(r2);

            Console.ReadLine();
        }

        private async void DoWork()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Working...");
                Thread.Sleep(3000);
                Console.WriteLine("Finished");
            });
        }

        private async Task<string> DoWorkStr()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Working...");
                Thread.Sleep(3000);
                Console.WriteLine("Finished");
            });
            return "Done.";
        }
    }
}
