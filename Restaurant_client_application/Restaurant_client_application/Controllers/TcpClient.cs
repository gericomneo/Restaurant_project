using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Controllers
{

    public class TcpClient
    {
        Socket sender;
        public bool updating = false;
        public int requestStatus = 0;
        public delegate void RefreshDelegate();
        public event RefreshDelegate Refresh;
        public bool Connect()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 13000);

                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                sender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                try
                {
                    sender.Connect(remoteEP);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;

            }

        }
        public bool SendMessage(string message)
        {
            string _message = message + "<EOM>";
            try
            {
                byte[] msg = new byte[8192];
                msg = Encoding.UTF8.GetBytes(_message);
                sender.Send(msg);
                return true;
            }
            catch (SocketException )
            {
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public string ReceiveMessage()
        {
            string message = string.Empty;

            try
            {
                while (!message.Contains("<EOM>"))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRec = sender.Receive(buffer);
                    message += Encoding.UTF8.GetString(buffer, 0, bytesRec);
                }
                message = message.Substring(0, message.Length - 5);
                string[] msg = message.Split(new string[] { "<EOR>"}, StringSplitOptions.None);
                msg = msg.Take(msg.Length - 1).ToArray();
                RequestHandler requestHandler = new RequestHandler();
                requestHandler.Update += new RequestHandler.UpdateDelegate(Updated);
                requestHandler.Response += new RequestHandler.ResponseDelegate(RequestStatus);
                foreach (string request in msg)
                {
                    message = requestHandler.RequestRecognition(request);
                }
                return message;
            }
            catch (SocketException)
            {
                requestStatus = 3;
                return "-1";
            }
            catch (Exception ex)
            {
                requestStatus = 3;
                MessageBox.Show(ex.ToString());
                return "-1";
            }
        }   

        public void Updated()
        {
            if (updating) Dispatcher.CurrentDispatcher.Invoke(Refresh);
        }

        public void RequestStatus(int status)
        {
            requestStatus = status;
        }
    }
}