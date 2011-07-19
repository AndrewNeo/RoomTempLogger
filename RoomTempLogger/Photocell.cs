using SecretLabs.NETMF.Hardware;

namespace RoomTempLogger
{
	public class Photocell
	{
		public AnalogInput Input { get; set; }

		public Photocell(AnalogInput input)
		{
			Input = input;
		}

		public int Read()
		{
			return Input.Read();
		}
	}
}