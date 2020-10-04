using System;
using System.IO.Ports;

namespace TestCOM
{
	class Program
	{
		static int outlen = 2;
		static int inplen = 48;
		static byte outdata = 0x6e;

		static void Main()
		{
			SerialPort port = new SerialPort();
			byte[] buffer = new byte[inplen+outlen];
			int i;
			if (outlen == 1) buffer[0] = outdata;
			else
			{
				buffer[0] = (byte)((int)outdata ^ 0xff);
				buffer[1] = outdata;
			}
			port.PortName = "COM2";
			port.BaudRate = 9600;
			port.DataBits = 8;
			port.Parity = Parity.None;
			port.StopBits = StopBits.One;
			port.Handshake = Handshake.RequestToSend;
			port.Open();
			port.Write(buffer, 0, outlen);
			for (i = 0; i < outlen; i++) Console.Write("{0,2:X} ", buffer[i]);
			Console.Write("\n");
			port.Read(buffer, outlen, inplen);
			port.Close();
			for (i = outlen; i < (inplen+outlen); i++) Console.Write("{0,2:X} ", buffer[i]);
			Console.Write("\n");
		}
	}
}
