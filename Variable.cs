using System.Text;

namespace Grapple
{
    public class Data
    {
        public byte[]? data { get; set; }
        public byte type { get; set; }
    }
    public class Variable
    {
        private static Dictionary<string, Data> variables = new Dictionary<string, Data>();
        public static void SetVariable(string name, byte[] data, byte type)
        {
            Data dt = new()
            {
                type = type,
                data = data
            };
            variables.Add(name, dt);
        }
        public static void SetVariable(string name, int integer)
        {
            Data dt = new()
            {
                type = 1,
                data = BitConverter.GetBytes(integer)
            };
            variables.Add(name, dt);
        }
        public static void SetVariable(string name, string? text)
        {
            Data dt = new()
            {
                type = 0,
                data = Encoding.UTF8.GetBytes(text == null ? string.Empty : text)
            };
            variables.Add(name, dt);
        }
        public static string? GetString(string name)
        {
            if (variables.ContainsKey(name))
            {
                Data dt = variables[name];
                if (dt.type == 0)
                {
                    if (dt.data != null)
                    {
                        return Encoding.UTF8.GetString(dt.data);
                    }
                }
            }
            return null;
        }
        public static int? GetInt(string name)
        {
            if (variables.ContainsKey(name))
            {
                Data dt = variables[name];
                if (dt.type == 1)
                {
                    if (dt.data != null)
                    {
                        return BitConverter.ToInt32(dt.data);
                    }
                }
            }
            return null;
        }
        public static Data? Get(string name)
        {
            if (variables.ContainsKey(name))
            {
                Data dt = variables[name];
                return dt;
            }
            return null;
        }
    }
}
