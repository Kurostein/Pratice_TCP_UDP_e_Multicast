using MulticastMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MulticastServer
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            string message = string.Empty; 

            if (Server.ServerCreate())
            {
                while(message != "quit")
                {
                    Console.WriteLine("Servidor iniciado!");
                    Console.WriteLine("Escreva algo para o grupo:");
                    message = Console.ReadLine();

                    Console.WriteLine("Servidor enviando... ");
                    Send.MessageTo(Server.server, Server.clientGroupAddress, message);

                    Console.WriteLine("Recebendo...");
                    message = Receive.MessageFrom(Server.server);

                    Console.WriteLine("Cliente enviou:");
                    Console.WriteLine(message);
                }                
            }
        }
    }
}
