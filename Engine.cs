namespace Grapple
{
    public class Engine
    {
        public static void LoadBase()
        {
            Variable.SetVariable("user", Environment.UserName, true);
        }
    }
    public class Debug
    {
        public static void Print(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void Error(string text)
        {
            Print(text, ConsoleColor.Red);
        }
        public static void Warn(string text)
        {
            Print(text, ConsoleColor.DarkYellow);
        }
        public static void Ok(string text)
        {
            Print(text, ConsoleColor.Green);
        }
        public static void Info(string text)
        {
            Print(text, ConsoleColor.Blue);
        }
        public static void Comment(string text)
        {
            Print(text, ConsoleColor.DarkGray);
        }
    }
}
