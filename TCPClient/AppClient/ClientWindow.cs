using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppClientTCP
{
    public partial class ClientWindow : Form
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostEntry.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 4500);

            TcpClient client = null;
            NetworkStream stream = null;

            byte[] buffer;
            int bufferCounter;

            try
            {
                client = new TcpClient();
                client.ReceiveTimeout = 30000;
                client.SendTimeout = 30000;
                client.ReceiveBufferSize = 256;
                client.SendBufferSize = 256;

                client.Connect(remoteEndPoint);

                lblConsole.Text = "Conectado ao servidor!";

                buffer = Encoding.ASCII.GetBytes(txtMessage.Text);

                stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);

                lblConsole.Text = "Sent: " + txtMessage.Text;

                buffer = new byte[256];

                string response = string.Empty;

                do
                {
                    bufferCounter = stream.Read(buffer, 0, buffer.Length);
                    response = Encoding.ASCII.GetString(buffer, 0, bufferCounter);
                } while (stream.DataAvailable);

                lblConsole.Text = "Received: " + response;

                stream.Close();
                client.Close();

            }
            catch (SocketException se)
            {
                lblConsole.Text = "Não foi possível conectar ao servidor. " + se.Message;
            }
            catch (IOException ioe)
            {
                lblConsole.Text = "Tempo máximo de espera para leitura ou envio \n de dados excedido, conexão encerrada.";
            }
        }
    }
}
