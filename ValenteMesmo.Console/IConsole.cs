﻿using System;
using System.IO;

namespace ValenteMesmo
{
    public interface IConsole : IDisposable
    {
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        string ReadPassword();

        void WriteLine(string text = "");
        void Write(string text);
        
        int CursorTop { get; set; }
        int CursorLeft { get; set; }
        bool CursorVisible { get; set; }
        void SetCursorPosition(int left, int top);

        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }

        IProgressBar ProgressBar(long total);
        void SetOut(StringWriter sw);
    }
}
