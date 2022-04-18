using System.Text;

namespace Grapple
{
    public class Data
    {
        public byte[]? data { get; set; }
        public byte type { get; set; }
        public bool readOnly = false;
    }
    public class Variable
    {
        private static Dictionary<string, Data> variables = new Dictionary<string, Data>();
        public static void SetVariable(string name, string? text, bool readOnly = false)
        {
            SetVariable(name, Encoding.UTF8.GetBytes(text == null ? string.Empty : text), 0, readOnly);
        }
        public static void SetVariable(string name, int integer, bool readOnly = false)
        {
            SetVariable(name, BitConverter.GetBytes(integer), 1, readOnly);
        }
        public static void SetVariable(string name, byte[] data, byte type, bool readOnly = false)
        {
            if (!(variables.ContainsKey(name) && variables[name].readOnly))
            {
                Data dt = new()
                {
                    type = type,
                    data = data,
                    readOnly = readOnly
                };
                variables[name] = dt;
            }
        }
        public static string? GetString(string name)
        {
            Data? x = Get(name);
            if (x != null && x.data != null && x.type == 0)
            {
                return Encoding.UTF8.GetString(x.data);
            }
            return null;
        }
        public static int? GetInt(string name)
        {
            Data? x = Get(name);
            if (x != null && x.data != null && x.type == 1)
            {
                return BitConverter.ToInt32(x.data);
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
        public static void Remove(string name, bool force = false)
        {
            if (variables.ContainsKey(name))
            {
                if (force || variables[name].readOnly)
                {
                    variables.Remove(name);
                }
            }
        }
    }
}
