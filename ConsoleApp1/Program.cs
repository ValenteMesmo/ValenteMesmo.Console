
using System.Threading;
using System.Threading.Tasks;
using ValenteMesmo;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var progress = Console.ProgressBar(100);

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(i * 10);
                progress.Set(i);
            }
            Console.ReadKey();
        }
    }
}
