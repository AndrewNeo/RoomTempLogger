using System;

namespace RoomTempLogger
{
	public class StateContainer
	{
		public double Temperature { get; set; }
		public int LightValue { get; set; }

		public string GetCSV()
		{
			return DateTime.Now + "," + Temperature.ToString("f2") + "," + LightValue;
		}
	}
}