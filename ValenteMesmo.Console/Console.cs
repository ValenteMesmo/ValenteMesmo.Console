using ConsoleKeyInfo = System.ConsoleKeyInfo;

namespace ValenteMesmo
{
    //TODO: store progressbar list to dispose
    public static class Console
    {
        public static readonly System.Lazy<IConsole> Instance
            = new System.Lazy<IConsole>(Create);

        public static IConsole Create() => new ConsoleService();

        public static int CursorTop
        {
            get => Instance.Value.CursorTop;
            set => Instance.Value.CursorTop = value;
        }

        public static int CursorLeft
        {
            get => Instance.Value.CursorLeft;
            set => Instance.Value.CursorLeft = value;
        }

        public static bool CursorVisible
        {
            get => Instance.Value.CursorVisible;
            set => Instance.Value.CursorVisible = value;
        }

        public static ConsoleKeyInfo ReadKey() => Instance.Value.ReadKey();
        public static string ReadLine() => Instance.Value.ReadLine();
        public static string ReadPassword() => Instance.Value.ReadPassword();

        public static IProgressBar ProgressBar(int total) =>
            Instance.Value.ProgressBar(total);

        public static void WriteLine(string text = "") =>
            Instance.Value.WriteLine(text);

        public static void SetCursorPosition(int left, int top) =>
            Instance.Value.SetCursorPosition(left, top);

        public static void Write(string text) =>
            Instance.Value.Write(text);
    }
}
