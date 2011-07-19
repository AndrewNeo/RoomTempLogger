using System;
using System.IO;
using System.Text;
using Microsoft.SPOT;

namespace RoomTempLogger
{
	public class Logger
	{
		public static void WriteLine(string line)
		{
			try
			{
				using (var fs = new StreamWriter(File.OpenWrite(@"\sd\rtl.csv")))
				{
					fs.WriteLine(line);
				}
			}
			catch (IOException ex)
			{
				Debug.Print("Error writing to SD card: " + ex.Message);
			}
		}
	}
}