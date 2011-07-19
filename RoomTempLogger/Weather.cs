using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using astra.http;

namespace RoomTempLogger
{
	public class Weather
	{
		public int ZipCode { get; set; }

		public Weather(int zip)
		{
			ZipCode = zip;
		}

		private int Fetch()
		{
			using (var s = new HttpSocketImpl())
			{
				var response = s.SendRequest("www.google.com", 80, "GET /ig/api?weather=" + ZipCode + "\nHost: www.google.com\n\n");

				var text = response.Contents;
				var start = text.IndexOf("temp_f data=\"");
				var end = text.IndexOf("\"", start);

				return int.Parse(text.Substring(start + 1, end - start));
			}
		}
	}
}