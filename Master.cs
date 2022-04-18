using System.Text;

namespace Grapple
{
    public class Master
    {
        public static string? answer = null;
        public static int errors = 0;
        public static int warnings = 0;
        public static void SetPro(string one, string two)
        {
            Data? data = Get(two);
            if (data != null && data.data != null)
            {
                Variable.SetVariable(one[1..^1], data.data, data.type);
            }
        }
        public static string Solve(string key)
        {
            string resp = string.Empty;
            string req = string.Empty;
            bool trigger = false;
            foreach (char c in key)
            {
                if (c == '%')
                {
                    if (trigger)
                    {
                        Data? data = Variable.Get(req[1..^1]);
                        if (data != null && data.data != null)
                        {
                            if (data.type == 0)
                            {
                                resp += Encoding.UTF8.GetString(data.data);
                            }
                            if (data.type == 1)
                            {
                                resp += BitConverter.ToInt32(data.data);
                            }
                        }
                    }
                    req = string.Empty;
                    trigger = !trigger;
                }
                else
                {
                    if (trigger)
                    {
                        req += c;
                    }
                    else
                    {
                        resp += c;
                    }
                }
            }
            return resp;
        }
        public static Data? Get(string key)
        {
            if (key.StartsWith('%') && key.EndsWith('%'))
            {
                key = key[1..^1];
            }
            if (int.TryParse(key, out int intData))
            {
                Data data = new Data()
                {
                    type = 1,
                    data = BitConverter.GetBytes(intData)
                };
                return data;
            }
            else if (key.StartsWith('{') && key.EndsWith('}'))
            {
                return Variable.Get(Solve(key[1..^1]));
            }
            else if (key.StartsWith('\"') && key.EndsWith('\"'))
            {
                return new Data()
                {
                    type = 0,
                    data = Encoding.UTF8.GetBytes(Solve(key[1..^1]))
                };
            }
            return null;
        }
    }
}
