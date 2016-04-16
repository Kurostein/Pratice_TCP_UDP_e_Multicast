using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppServerTCP
{
    public class Server
    {
        public static string data = string.Empty;

        static void Main(string[] args)
        {
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4500);
            TcpListener serverListener = new TcpListener(localEndPoint);

            byte[] buffer;

            try
            {
                serverListener.Start(5);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    data = string.Empty;

                    TcpClient client = serverListener.AcceptTcpClient();
                    client.SendTimeout = 30000;
                    client.ReceiveTimeout = 30000;
                    client.ReceiveBufferSize = 256;
                    client.SendBufferSize = 256;

                    Console.WriteLine("Client connected!");

                    NetworkStream stream = client.GetStream();

                    buffer = new byte[256];
                    int bufferCounter = -1;
                    
                    do
                    {
                        bufferCounter = stream.Read(buffer, 0, buffer.Length);
                        data = Encoding.ASCII.GetString(buffer, 0, bufferCounter);
                        
                        Console.WriteLine("Text received: {0}", data);
                    } while (stream.DataAvailable);

                    data = data.ToUpper();
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);

                    stream.Close();
                    client.Close();
                }
            }
            catch(SocketException se)
            {
                Console.WriteLine("Erro de conexão:");
                Console.WriteLine(se.Message);
            }
            catch(IOException ioe)
            {
                Console.WriteLine("Tempo máximo de espera para leitura ou envio de dados excedido, conexão encerrada.");
                Console.WriteLine(ioe.Message);
            }
            finally
            {
                serverListener.Stop();
            }

            Console.WriteLine("Enter to continue...");
            Console.Read();

        }
    }
}
