
using System.Threading;
using ValenteMesmo;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var progress = Console.ProgressBar(100))
                for (int i = 0; i <= 100; i++)
                {
                    progress.Set(i);
                    Thread.Sleep(100);
                }
            Console.ReadKey();
        }
    }
}
