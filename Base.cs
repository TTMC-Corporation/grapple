namespace Grapple
{
    internal class Base
    {
        private static int currentPower = 0;
        public static void RunScript(string[] code)
        {
            foreach (string theLine in code)
            {
                string line = theLine;
                int power = PowerOfLine(line);
                line.Remove(0, power);
                if (line.ToLower().StartsWith("say \"") && line.EndsWith("\""))
                {
                    Terminal.WriteLine(line[5..^1]);
                }
                if (line.ToLower() == "pause")
                {
                    Console.ReadKey();
                }
                if (line.ToLower().StartsWith("ask \"") && line.EndsWith("\""))
                {
                    Terminal.Write(line[5..^1]);
                    Master.answer = Console.ReadLine();
                }
                if (line.ToLower().StartsWith("set "))
                {
                    int nzx = line.IndexOf(" to ");
                    Master.SetPro(line[4..nzx], line[(nzx+4)..]);
                }
                if (line.ToLower().StartsWith("loop "))
                {
                    currentPower += 1;
                }
            }
        }
        private static int PowerOfLine(string line)
        {
            int power = 0;
            while (line.StartsWith('\n'))
            {
                power += 1;
                line = line.Remove(0, 1);
            }
            return power;
        }
    }
}
