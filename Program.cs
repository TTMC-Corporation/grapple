using System.Text;

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
                    if (args.Length >= 2)
                    {
                        if (args[1] == "compress")
                        {
                            if (args[0].EndsWith(".grap"))
                            {
                                File.WriteAllBytes(args[0] + "x", Engine.Compress(File.ReadAllBytes(args[0])));
                            }
                        }
                        if (args[1] == "decompress")
                        {
                            if (args[0].EndsWith(".grapx"))
                            {
                                File.WriteAllBytes(args[0][..^1], Engine.Decompress(File.ReadAllBytes(args[0])));
                            }
                        }
                    }
                    else
                    {
                        List<string> lines = new();
                        if (args[0].EndsWith(".grap"))
                        {
                            lines.AddRange(File.ReadAllLines(args[0]));
                        }
                        else if (args[0].EndsWith(".grapx"))
                        {
                            File.WriteAllBytes(args[0][..^1], Engine.Decompress(File.ReadAllBytes(args[0])));
                            lines.AddRange(File.ReadAllLines(args[0][..^1]));
                            File.Delete(args[0][..^1]);
                        }
                        Engine.LoadBase();
                        Base.RunScript(lines.ToArray());
                    }
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