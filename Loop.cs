namespace Grapple
{
	internal class Loop
	{
		public static int loops = 0;
		public static bool run = true;
		public static List<string> LoopField(string[] code, int start)
		{
			List<string> theCode = new();
			int? powerScan = Base.PowerOfLine(code[start]);
			for (int i = start; i < code.Length; i++)
			{
				if (Base.PowerOfLine(code[i]) >= powerScan)
                {
					string line = code[i];
                    for (int x = 0; x < powerScan; x++)
                    {
						if (line.StartsWith("    "))
						{
							line = line.Remove(0, 4);
						}
						else if (line.StartsWith('\t'))
						{
							line = line.Remove(0, 1);
						}
					}
					theCode.Add(line);
				}
                else
                {
					return theCode;
                }
			}
			return theCode;
		}
	}
}
