namespace Grapple
{
    internal class Base
    {
        private static List<int> ifs = new();
        private static int currentPower = 0;
        public static void RunScript(string[] code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (Loop.run)
                {
                    string line = code[i];
                    int power = PowerOfLine(line);
                    while (line.StartsWith(' ') || line.StartsWith('\t'))
                    {
                        line = line.Remove(0, 1);
                    }
                    if (power < currentPower)
                    {
                        currentPower = power;
                    }
                    if (power == currentPower)
                    {
                        if (!line.ToLower().StartsWith("else"))
                        {
                            if (ifs.Contains(currentPower))
                            {
                                ifs.Remove(currentPower);
                            }
                        }
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
                            string? text = Console.ReadLine();
                            Variable.SetVariable("answer", text);
                        }
                        if (line.ToLower().StartsWith("set "))
                        {
                            int nzx = line.IndexOf(" to ");
                            Master.SetPro(line[4..nzx], line[(nzx + 4)..]);
                        }
                        if (line.StartsWith("add ") && line.Contains(" to "))
                        {
                            int nzx = line.IndexOf(" to ");
                            Master.AddPro(line[4..nzx], line[(nzx + 4)..]);
                        }
                        if (line.StartsWith("remove ") && line.Contains(" from "))
                        {
                            int nzx = line.IndexOf(" from ");
                            Master.RemovePro(line[7..nzx], line[(nzx + 6)..]);
                        }
                        if (line.ToLower().StartsWith("if ") && line.ToLower().Contains(" is "))
                        {
                            int nzx = line.IndexOf(" is ");
                            if (Master.Check(line[3..nzx], line[(nzx + 4)..^1]))
                            {
                                currentPower += 1;
                            }
                            else
                            {
                                ifs.Add(power);
                            }
                        }
                        if (line.ToLower() == "else:")
                        {
                            if (ifs.Contains(currentPower))
                            {
                                ifs.Remove(currentPower);
                                currentPower += 1;
                            }
                        }
                        if (line.ToLower().StartsWith("else if "))
                        {
                            if (ifs.Contains(currentPower))
                            {
                                int nzx = line.IndexOf(" is ");
                                if (Master.Check(line[8..nzx], line[(nzx + 4)..^1]))
                                {
                                    ifs.Remove(currentPower);
                                    currentPower += 1;
                                }
                            }
                        }
                        if (line.ToLower().StartsWith("loop ") && line.ToLower().EndsWith(" times:"))
                        {
                            int num = 1;
                            Data? lp = Master.Get(line[5..^7]);
                            if (lp != null && lp.data != null && lp.type == 1)
                            {
                                num = BitConverter.ToInt32(lp.data);
                            }
                            List<string> loopLines = Loop.LoopField(code, i + 1);
                            int loopID = ++Loop.loops;
                            for (int x = 0; x < num; x++)
                            {
                                Variable.SetVariable("loop-value", x + 1);
                                Variable.SetVariable("loop-value-" + loopID, x + 1);
                                RunScript(loopLines.ToArray());
                                Variable.Remove("loop-value");
                                Variable.Remove("loop-value-" + loopID);
                            }
                            Loop.loops -= 1;
                            i += loopLines.Count;
                        }
                        if (line.ToLower().StartsWith("while ") && line.ToLower().EndsWith(':') && line.Contains(" is "))
                        {
                            int nzx = line.IndexOf(" is ");
                            List<string> loopLines = Loop.LoopField(code, i + 1);
                            int x = 0;
                            int loopID = ++Loop.loops;
                            while (Master.Check(line[6..nzx], line[(nzx + 4)..^1]) && Loop.run)
                            {
                                x += 1;
                                Variable.SetVariable("loop-value", x);
                                Variable.SetVariable("loop-value-" + loopID, x);
                                RunScript(loopLines.ToArray());
                            }
                            Loop.run = true;
                            Variable.Remove("loop-value");
                            Variable.Remove("loop-value-" + loopID);
                            Loop.loops -= 1;
                            i += loopLines.Count;

                        }
                        if (line.ToLower() == "break")
                        {
                            Loop.run = false;
                        }
                    }
                }
            }
        }
        public static int PowerOfLine(string line)
        {
            int power = 0;
            while (true)
            {
                if (line[..4] == "    ")
                {
                    power += 1;
                    line = line.Remove(0, 4);
                }
                else if (line.StartsWith('\t'))
                {
                    power += 1;
                    line = line.Remove(0, 1);
                }
                else
                {
                    break;
                }
            }
            return power;
        }
    }
}
