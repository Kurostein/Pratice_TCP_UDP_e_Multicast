using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastMessage
{
    public class Receive
    {
        public static string MessageFrom(UdpClient receiver)
        {
            string message = string.Empty;

            //Coloca o cliente para escutar em qualquer interface na porta 50
            IPEndPoint endpoint = new IPEndPoint(IPAddress.IPv6Any, 50);

            //Recebe o pacote UDP de dados de quem enviou
            byte[] data = receiver.Receive(ref endpoint);

            message = Encoding.UTF8.GetString(data);
            message += "/n";

            return message;
        }
    }
}
