using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application
{
    class TcpServer
    {
        private TcpListener Listener { get; set; }
        private Dictionary<TcpClient, int> Clients = new Dictionary<TcpClient, int>();
        private bool Accept { get; set; } = false;
        private int ClientsCount { get; set; } = 0;
        private Thread ServerThread { get; set; }

        public TcpServer()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);
                Listener = new TcpListener(localEndPoint);
                ServerThread = new Thread(() => Listen(Accept));
                Listener.Start();
                Accept = true;
                ServerThread.Start();
                Console.WriteLine("Server started.");
                Console.WriteLine("Address: {0}\tPort: {1}", localEndPoint.Address, localEndPoint.Port);
                Console.WriteLine("Listening to TCP clients");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
        }

        private void Listen(bool accept)
        {
            if (Listener != null && accept)
            {
                while (true)
                {
                    Console.WriteLine("Waiting for next client. {0} connected at the moment.", ClientsCount);
                    _listen();
                }
            }
        }

        private async void _listen()
        {
            bool connections = true;
            var clientTask = Listener.AcceptTcpClientAsync();
            if (clientTask.Result != null)
            {
                ClientsCount++;
                var client = clientTask.Result;
                Console.WriteLine("Client connected. ip: {0}\n", client.Client.RemoteEndPoint.ToString());

                try
                {
                    await Task.Run(async() =>
                    {
                        int loggedType = 0;
                        while (connections && loggedType == 0)
                        {
                            try
                            {
                                string response = ReceiveMessage(client, loggedType);
                                switch (response.Substring(0,4))
                                {
                                    case "0005":
                                        SendMessage(client, response + "<EOM>");
                                        Console.WriteLine("Sent to {0}: {1}", client.Client.RemoteEndPoint.ToString(), response + "<EOM>");
                                        connections = false;
                                        break;
                                    default:
                                        loggedType = int.Parse((response.Split(new string[] { "<EOP>" }, StringSplitOptions.None)[1].Split(new string[] { "<EOR>" }, StringSplitOptions.None))[0]);
                                        Clients.Add(client, loggedType);
                                        if (!SendMessage(client, response + "<EOM>")) connections = false;
                                        Console.WriteLine("Sent to {0}: {1}", client.Client.RemoteEndPoint.ToString(), response + "<EOM>");
                                        break;
                                }
                            }
                            catch (IOException)
                            {
                                connections = false;
                            }
                            catch (Exception ex)
                            {
                                connections = false;
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        while (connections)
                        {
                            try
                            {
                                string response = ReceiveMessage(client, loggedType);
                                switch (response.Substring(0, 4))
                                {
                                    case "0003":
                                        string[] updatingClients = response.Split('#');
                                        if (!SendMessage(client, updatingClients[0] + "<EOM>"))
                                        {
                                            connections = false;
                                            Clients.Remove(client);
                                        }
                                        Console.WriteLine("Sent to {0}: {1}", client.Client.RemoteEndPoint.ToString(), updatingClients[0] + "<EOM>");
                                        foreach (KeyValuePair<TcpClient, int> connected in Clients)
                                        {
                                            if (updatingClients[1].Contains(connected.Value.ToString()))
                                            {
                                                await Task.Run(() => SendMessage(connected.Key, updatingClients[2] + "<EOM>"));
                                                Console.WriteLine("Sent update to {0}: {1}", connected.Key.Client.RemoteEndPoint.ToString(), updatingClients[2] + "<EOM>");
                                            }
                                        }
                                        break;
                                    case "0000":
                                        connections = false;
                                        Clients.Remove(client);
                                        break;

                                    case "0004":
                                        if (!SendMessage(client, response + "<EOM>"))
                                        {
                                            connections = false;
                                            Clients.Remove(client);
                                        }
                                        Console.WriteLine("Sent to {0}: {1}", client.Client.RemoteEndPoint.ToString(), response + "<EOM>");
                                        break;
                                    case "0002":
                                        if (!SendMessage(client, response.Substring(9) + "<EOM>"))
                                        {
                                            connections = false;
                                            Clients.Remove(client);
                                        }
                                        Console.WriteLine("Sent to {0}: {1}", client.Client.RemoteEndPoint.ToString(), response.Substring(9) + "<EOM>");
                                        break;
                                }
                            }
                            catch (IOException)
                            {
                                connections = false;
                            }
                            catch (Exception ex)
                            {
                                connections = false;
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    Console.WriteLine("Closing connection. {0} connected at the moment.", --ClientsCount);
                    try
                    {
                        client.Dispose();
                    }
                    catch(InvalidOperationException)
                    {

                    }
                    finally
                    {
                        client.Close();
                    }
                }
            }
        }
        public bool SendMessage(TcpClient client, string message)
        {
            try
            {
                byte[] buffer = new byte[8192];
                buffer = Encoding.UTF8.GetBytes(message);
                client.GetStream().Write(buffer, 0, buffer.Length);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string ReceiveMessage(TcpClient client, int loggedType)
        {
            TcpClient _client = client;
            string message = string.Empty;
            byte[] buffer = new byte[4096];
            try
            {
                while (!message.Contains("<EOM>"))
                {
                    client.GetStream().Read(buffer, 0, buffer.Length);
                    message += Encoding.UTF8.GetString(buffer);
                }
                string[] msg = message.Split(new string[] { "<EOM>" }, StringSplitOptions.None);
                Console.WriteLine("Received from {0}: {1} ", client.Client.RemoteEndPoint.ToString(), msg[0]);
                RequestHandler requestHandler = new RequestHandler(loggedType);
                return requestHandler.RequestRecognition(msg[0]);
            }
            catch (IOException)
            {
                return "0004<EOP>";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "0004<EOP>";
            }
        }
    }
}