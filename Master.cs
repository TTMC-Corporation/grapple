using System.Text;

namespace Grapple
{
    public class Master
    {
        public static int errors = 0;
        public static int warnings = 0;
        public static void AddPro(string one, string two)
        {
            Data? first = Get(one);
            Data? second = Get(two);
            if (first != null && second != null && first.data != null && second.data != null && first.type == second.type)
            {
                Variable.SetVariable(two, BitConverter.ToInt32(second.data) + BitConverter.ToInt32(first.data));
            }
        }
        public static void RemovePro(string one, string two)
        {
            Data? first = Get(one);
            Data? second = Get(two);
            if (first != null && second != null && first.data != null && second.data != null && first.type == second.type)
            {
                Variable.SetVariable(two, BitConverter.ToInt32(second.data) - BitConverter.ToInt32(first.data));
            }
        }
        public static bool Check(string one, string two)
        {
            Data? first = Get(one);
            Data? second = Get(two);
            if (first != null && second != null && first.data != null && second.data != null && first.type == second.type && Convert.ToBase64String(first.data) == Convert.ToBase64String(second.data))
            {
                return true;
            }
            return false;
        }
        public static void SetPro(string one, string two)
        {
            Data? data = Get(two);
            if (data != null && data.data != null)
            {
                Variable.SetVariable(one, data.data, data.type);
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
                        Data? data = Variable.Get(req);
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
            if (int.TryParse(key, out int intData))
            {
                Data data = new Data()
                {
                    type = 1,
                    data = BitConverter.GetBytes(intData)
                };
                return data;
            }
            else if (key.StartsWith('\"') && key.EndsWith('\"'))
            {
                return new Data()
                {
                    type = 0,
                    data = Encoding.UTF8.GetBytes(Solve(key[1..^1]))
                };
            }
            return Variable.Get(Solve(key));
        }
    }
}
