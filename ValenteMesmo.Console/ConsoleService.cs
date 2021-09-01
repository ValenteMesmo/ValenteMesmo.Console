namespace ValenteMesmo
{
    public class ConsoleService : IConsole
    {
        internal static readonly object threadLock = new object();

        public int CursorTop
        {
            get => System.Console.CursorTop;
            set
            {
                lock (threadLock)
                    System.Console.CursorTop = value;
            }
        }

        public int CursorLeft
        {
            get => System.Console.CursorLeft;
            set
            {
                lock (threadLock)
                    System.Console.CursorLeft = value;
            }
        }

        public bool CursorVisible
        {
            get => System.Console.CursorVisible;
            set
            {
                lock (threadLock)
                    System.Console.CursorVisible = value;
            }
        }

        public void ReadKey()
        {
            lock (threadLock)
                System.Console.ReadKey();
        }

        public System.ConsoleColor ForegroundColor
        {
            get => System.Console.ForegroundColor;
            set
            {
                System.Console.ForegroundColor = value;
            }
        }

        public System.ConsoleColor BackgroundColor
        {
            get => System.Console.BackgroundColor;
            set
            {
                System.Console.BackgroundColor = value;
            }
        }

        public IProgressBar ProgressBar(long total) =>
            new ProgressBar(total, this);

        public void WriteLine(string text = "")
        {
            lock (threadLock)
                System.Console.WriteLine(text);
        }

        public void SetCursorPosition(int left, int top)
        {
            lock (threadLock)
                System.Console.SetCursorPosition(left, top);
        }

        public void Write(string text)
        {
            lock (threadLock)
                System.Console.Write(text);
        }

        public void Dispose() { }

        ~ConsoleService()
        {
            Dispose();
        }
    }
}
