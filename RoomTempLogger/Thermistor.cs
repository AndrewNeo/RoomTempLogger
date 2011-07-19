using netduino.helpers.Math;
using SecretLabs.NETMF.Hardware;

namespace RoomTempLogger
{
	public class Thermistor
	{
		private readonly AnalogInput _input;

		public double Kelvin
		{
			get
			{
				var rawAdc = _input.Read();
				double temp = Trigo.Log(((10240000 / rawAdc) - 10000));
				temp = (1 / (0.001129148 + (0.000234125 + (0.0000000876741 * temp * temp)) * temp));
				return temp;
			}
		}

		public double Celcius { get { return Kelvin - 273.15; } }

		public double Fahrenheit { get { return (Celcius*9.0)/5.0 + 32.0; } }

		public Thermistor(AnalogInput input)
		{
			_input = input;
		}
	}
}