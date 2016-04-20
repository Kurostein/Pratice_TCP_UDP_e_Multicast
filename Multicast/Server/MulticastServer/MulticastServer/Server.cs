using System;
using System.Net;
using System.Net.Sockets;

namespace MulticastServer
{
    public class Server
    {
        public static UdpClient server;
        private static IPAddress multicastGroupAddress; //IP do grupo
        public static IPEndPoint clientGroupAddress; //IP do grupo e porta dos clientes
        
        public static bool ServerCreate()
        {
            try
            {
                server = new UdpClient(3000, AddressFamily.InterNetworkV6);
            
                //Define IP do gropo multicast
                multicastGroupAddress = IPAddress.Parse("FF01::1");

                IPv6MulticastOption ipv6MulticastOption = new IPv6MulticastOption(multicastGroupAddress);
                //Obtém IP do grupo e índice associado a interface do grupo
                IPAddress group = ipv6MulticastOption.Group;
                long interfaceIndex = ipv6MulticastOption.InterfaceIndex;

                //Entra no grupo
                server.JoinMulticastGroup((int)interfaceIndex, group);

                //Define o IP de endpoint dos clientes, com o IP do grupo e socket que os clientes recebem mensagens
                clientGroupAddress = new IPEndPoint(multicastGroupAddress, 1000);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível criar o servidor ou o grupo.");
                return false;
            }
        }

        public static void LeaveMulticastGroup()
        {
            server.DropMulticastGroup(multicastGroupAddress);
        }
    }
}
