using System;

namespace CheckIfEqualFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckIfEqual.RunCheck(args);
            Console.Write(Environment.NewLine + "Press any key to close the application...");
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
