
using System.IO;

namespace NegativeStatsMod
{
	public static class Logger
	{
		public static bool Enabled { get; set; } = true;
		
		public static void Log(object message)
		{
			if (!Enabled)
			{
				return;
			}
			
			if (!File.Exists("NegativeStatsMod.log"))
			{
				File.Create("NegativeStatsMod.log").Close();
			}
			
			File.AppendAllText("NegativeStatsMod.log", message.ToString() + "\n");
		}
	}
}