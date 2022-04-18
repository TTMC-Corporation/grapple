namespace Grapple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                PrintLogo();
            }
            else
            {
                if (File.Exists(args[0]))
                {
                    string[] lines = File.ReadAllLines(args[0]);
                    Base.RunScript(lines);
                }
                else
                {
                    Debug.Error("File Not Found");
                }
            }
        }
        private static void PrintLogo()
        {
            Debug.Print("   ______                       __        _____           _       __ \n  / ____/________ _____  ____  / /__     / ___/__________(_)___  / /_\n / / __/ ___/ __ `/ __ \\/ __ \\/ / _ \\    \\__ \\/ ___/ ___/ / __ \\/ __/\n/ /_/ / /  / /_/ / /_/ / /_/ / /  __/   ___/ / /__/ /  / / /_/ / /_  \n\\____/_/   \\__,_/ .___/ .___/_/\\___/   /____/\\___/_/  /_/ .___/\\__/  \n               /_/   /_/                               /_/           ", ConsoleColor.Red);
            Terminal.WriteLine("&3Grapple Script &bv0.1 &8by &9TheBlueLines &8(TTMC Corporation)");
        }
    }
}