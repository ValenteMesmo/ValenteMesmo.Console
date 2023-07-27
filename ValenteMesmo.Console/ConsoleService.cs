using System;
using System.IO;
using System.Text;
using ConsoleKey = System.ConsoleKey;
using ConsoleKeyInfo = System.ConsoleKeyInfo;

namespace ValenteMesmo
{
    public class ConsoleService : IConsole
    {
        internal static readonly object threadLock = new object();
        private Stream stdout;

        public ConsoleService()
        {
            System.Console.OutputEncoding = Encoding.UTF8;
            this.stdout = System.Console.OpenStandardOutput();
        }

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

        public ConsoleKeyInfo ReadKey()
        {
            lock (threadLock)
                return System.Console.ReadKey();
        }

        public string ReadLine()
        {
            lock (threadLock)
                return System.Console.ReadLine();
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

        byte[] buffer;
        public void WriteLine(string text = "")
        {
            Write($"{text}{Environment.NewLine}");
        }

        public void SetCursorPosition(int left, int top)
        {
            lock (threadLock)
                System.Console.SetCursorPosition(left, top);
        }

        public void Write(string text)
        {
            lock (threadLock)
            {
                buffer = Encoding.UTF8.GetBytes(text);
                stdout.Write(buffer, 0, buffer.Length);
            }
        }

        public string ReadPassword()
        {
            string pass = "";
            lock (threadLock)
            {
                do
                {
                    var key = System.Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        lock (threadLock)
                            System.Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, (pass.Length - 1));
                            lock (threadLock)
                                System.Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);

                System.Console.WriteLine("");
            }

            return pass;
        }

        public void Dispose() { }

        public void SetOut(StringWriter sw)
        {
            System.Console.SetOut(sw);
            this.stdout = new StringWriterStream(sw);
        }

        ~ConsoleService()
        {
            Dispose();
        }
    }
}