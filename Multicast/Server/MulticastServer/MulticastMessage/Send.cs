using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MulticastMessage
{
    public class Send
    {
        public static void MessageTo(UdpClient sender, IPEndPoint receiver, string message)
        {
            sender.Send(GetByteArray(message), message.ToArray().Length, receiver);
            Thread.Sleep(1000);
        }

        private static byte[] GetByteArray(string message)
        {
            char[] messageArray = message.ToCharArray();
            byte[] bytes = new byte[messageArray.Length];

            for (int i = 0; i < messageArray.Length; i++)
                bytes[i] = (byte)messageArray[i];

            return bytes;
        }
    }
}
