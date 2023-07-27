using System.IO;
using ConsoleKeyInfo = System.ConsoleKeyInfo;

namespace ValenteMesmo
{
    //TODO: store progressbar list to dispose
    public static class Console
    {
        static Console()
        {
            Instance = Create();
        }

        public static readonly IConsole Instance;
            
        public static IConsole Create() => new ConsoleService();

        public static int CursorTop
        {
            get => Instance.CursorTop;
            set => Instance.CursorTop = value;
        }

        public static int CursorLeft
        {
            get => Instance.CursorLeft;
            set => Instance.CursorLeft = value;
        }

        public static bool CursorVisible
        {
            get => Instance.CursorVisible;
            set => Instance.CursorVisible = value;
        }

        public static ConsoleKeyInfo ReadKey() => Instance.ReadKey();
        public static string ReadLine() => Instance.ReadLine();
        public static string ReadPassword() => Instance.ReadPassword();

        public static IProgressBar ProgressBar(int total) =>
            Instance.ProgressBar(total);

        public static void WriteLine(string text = "") =>
            Instance.WriteLine(text);

        public static void SetCursorPosition(int left, int top) =>
            Instance.SetCursorPosition(left, top);

        public static void Write(string text) =>
            Instance.Write(text);

        public static void SetOut(StringWriter sw)
        {
            Instance.SetOut(sw);
        }
    }
}
