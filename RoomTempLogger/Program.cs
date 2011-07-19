using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MFToolkit.Net.Ntp;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace RoomTempLogger
{
	public class Program
	{
		public static void Main()
		{
			// Try to get clock at system start
			try
			{
				var time = NtpClient.GetNetworkTime();
				Utility.SetLocalTime(time);
			}
			catch (Exception ex)
			{
				// Don't depend on time
				Debug.Print("Error setting clock: " + ex.Message);
			}

			var led = new OutputPort(Pins.ONBOARD_LED, false);
			var pin0 = new AnalogInput(Pins.GPIO_PIN_A0);
			var pin1 = new AnalogInput(Pins.GPIO_PIN_A1);

			var state = new StateContainer();
			var light = new Photocell(pin0);
			var temp = new Thermistor(pin1);
			//var weather = new Weather(48638);
			var netlisten = new NetListener(state, 5348);

			netlisten.Start();

			while (true)
			{
				led.Write(true);

				state.LightValue = light.Read();
				state.Temperature = temp.Fahrenheit;

				//Logger.WriteLine(state.GetCSV());
				
				Thread.Sleep(500);

				led.Write(false);

				Thread.Sleep(10000);
			}
		}

	}
}
