using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace RoomTempLogger
{
	public class NetListener
	{
		private const int ReceiveBufferSize = 1024;
		private readonly StateContainer _container;
		private Socket _socket;

		public bool IsOpen { get; private set; }
		public int Port { get; private set; }

		public NetListener(StateContainer container, int port)
		{
			_container = container;
			Port = port;
		}

		public void Start()
		{
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_socket.Bind(new IPEndPoint(IPAddress.Any, Port));
			_socket.Listen(int.MaxValue);

			IsOpen = true;

			new Thread(Listen).Start();
		}

		public void Stop()
		{
			IsOpen = false;
			_socket.Close();
		}

		private void Listen()
		{
			while (IsOpen)
			{
				using (Socket client = _socket.Accept())
				{
					var str = _container.GetCSV();
					var bytes = Encoding.UTF8.GetBytes(str);

					client.Send(bytes, bytes.Length, SocketFlags.None);
					client.Close();
				}
			}
		}
	}
}