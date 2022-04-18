using System.IO.Compression;
using System.Text;

namespace Grapple
{
    public class Engine
    {
        public static void LoadBase()
        {
            Variable.SetVariable("user", Environment.UserName, true);
        }
        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.SmallestSize))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
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
