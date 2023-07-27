using System.Threading.Tasks;
using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;

namespace ValenteMesmo
{
    public interface IProgressBar : System.IDisposable
    {
        void Set(long currentValue);

        void Increment(long incrementValue = 1);
    }

    public class ProgressBar : IProgressBar
    {
        private readonly IConsole console;
        private readonly long total;
        private readonly Task task;
        private long progress = 0;
        private int location;
        DateTime startTime = DateTime.Now;
        private bool disposed;
        private long previousProgress;

        public ProgressBar(long total, IConsole console)
        {
            this.console = console;
            this.total = total;
            console.WriteLine();
            location = console.CursorTop;
            console.WriteLine();
            console.WriteLine();

            task = Task.Factory.StartNew(async () =>
            {
                while (!disposed)
                {
                    Render();
                    await Task.Delay(900);
                }
            });

        }

        public void Dispose()
        {
            disposed = true;
        }

        public void Set(long progress)
        {
            this.progress = progress;
            if (progress >= total)
                Render();
        }

        public void Increment(long progressIncrement = 1)
        {
            progress += progressIncrement;
            if (progress >= total)
                Render();
        }

        private void Render()
        {
            if (disposed)
                return;

            lock (ConsoleService.threadLock)
            {
                console.CursorVisible = false;
                var cursorTop = console.CursorTop;

                if (progress > total)
                    progress = total;

                previousProgress = (int)Lerp(previousProgress, progress, 0.5f);
                var percentage = (int)((previousProgress * 100) / total);

                if (total == progress)
                {
                    previousProgress = progress;
                    percentage = 100;
                    if (!disposed)
                        Dispose();
                }

                var timeTaken = DateTime.Now.Subtract(startTime);
                var timeRemaining = TimeSpan.FromTicks(
                    (timeTaken.Ticks / (progress + 1)) * (total - progress + 1)
                );

                console.CursorLeft = 1;

                float onechunk = 18.0f / total;

                int position = 1;
                console.CursorLeft = 0;
                if (disposed)
                    position = 18;
                else
                    position = (int)(onechunk * previousProgress);
                var delta = 18 - position;
                //            Console.WriteLine(@"
                //╔══════════════════╗ 60%
                //║█████▒▒▒▒▒▒▒▒▒▒▒▒▒║ Total: 5min
                //╚══════════════════╝ Estimated time remaining: 4min");
                console.SetCursorPosition(0, location - 1);
                console.Write($"╔══════════════════╗ {percentage}%");
                console.SetCursorPosition(0, location);
                var background = console.ForegroundColor;

                console.Write("║");
                console.ForegroundColor = disposed ? System.ConsoleColor.DarkGreen : System.ConsoleColor.Green;
                console.Write($"{new string('█', position)}");
                console.ForegroundColor = System.ConsoleColor.DarkGray;
                console.Write($"{new string('█', delta)}");
                console.ForegroundColor = background;

                console.Write($"║ Total:     {Format(timeTaken)}");
                console.SetCursorPosition(0, location + 1);
                console.Write($"╚══════════════════╝ {(disposed ? "Completed!                        " : FormatEstimatedTime(timeRemaining))}");

                //TODO: restore left
                console.SetCursorPosition(0, cursorTop);
                console.CursorVisible = true;
            }
        }

        private string FormatEstimatedTime(TimeSpan timeRemaining)
        {
            return $"Remaining: {Format(timeRemaining)}";
        }

        private string Format(TimeSpan time)
        {
            return $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
        }

        private float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}
