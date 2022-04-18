using System.Text;

namespace Grapple
{
    public class Terminal
    {
        private static Dictionary<char, ConsoleColor> colors = new() { { 'a', ConsoleColor.Green }, { 'b', ConsoleColor.Cyan }, { 'c', ConsoleColor.Red }, { 'd', ConsoleColor.Magenta }, { 'e', ConsoleColor.Yellow }, { 'f', ConsoleColor.White }, { '0', ConsoleColor.Black }, { '1', ConsoleColor.DarkBlue }, { '2', ConsoleColor.DarkGreen }, { '3', ConsoleColor.DarkCyan }, { '4', ConsoleColor.DarkRed }, { '5', ConsoleColor.DarkMagenta }, { '6', ConsoleColor.DarkYellow }, { '7', ConsoleColor.Gray }, { '8', ConsoleColor.DarkGray }, { '9', ConsoleColor.Blue } };
        public static void WriteLine(string text)
        {
            Write(text);
            Console.WriteLine();
        }
        public static void Write(string text)
        {
            int? lvl = 0;
            bool color = false;
            string? var = null;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (var != null && lvl != null)
                {
                    if (c == '%')
                    {
                        lvl += 1;
                        var += "%";
                        if (lvl % 2 == 0)
                        {
                            Data? data = Master.Get(var[1..^1]);
                            if (data != null && data.data != null)
                            {
                                if (data.type == 0)
                                {
                                    Console.Write(Encoding.UTF8.GetString(data.data));
                                    var = null;
                                    lvl = null;
                                }
                                else if (data.type == 1)
                                {
                                    Console.Write(BitConverter.ToInt32(data.data));
                                    var = null;
                                    lvl = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        var += c;
                    }
                }
                else
                {
                    if (color)
                    {
                        color = false;
                        if (colors.ContainsKey(c))
                        {
                            Console.ForegroundColor = colors[c];
                        }
                        else if (c == 'r')
                        {
                            Console.ResetColor();
                        }
                        else
                        {
                            Master.errors += 1;
                        }
                    }
                    else
                    {
                        if (c is '&')
                        {
                            color = true;
                        }
                        else if (c is '%')
                        {
                            lvl = 1;
                            var = "%";
                        }
                        else
                        {
                            Console.Write(c);
                        }
                    }
                }
            }
            Console.ResetColor();
        }
    }
}
