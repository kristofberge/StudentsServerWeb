using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentsServerWeb
{
    class SimpleServer
    {

        private IPAddress _ipAddress;

        private AbstractResponder _helper;

        private volatile bool _continue = true;

        private TcpListener listener;
        private NetworkStream stream;
        private TcpClient client;

        public SimpleServer() { }

        public SimpleServer(string ipAddress, AbstractResponder helper)
        {
            try
            {
                _ipAddress = IPAddress.Parse(ipAddress);
            }
            catch (Exception)
            {
                throw;
            }
            this._helper = helper;
        }

        

        public void startListening(int port) {
            try
            {
                while (_continue)
                {
                    Thread.Sleep(10);
                    listener = new TcpListener(_ipAddress, port);

                    listener.Start();

                    Console.WriteLine("Listening on port " + port);
                    Console.WriteLine("The local End point is: " + listener.LocalEndpoint);

                    client = listener.AcceptTcpClient();
                    Console.WriteLine("Connection accepted from " + client.ToString());

                    byte[] bytes = new byte[8];

                    stream = client.GetStream();
                    stream.Read(bytes, 0, bytes.Length);
                    
                    _helper.Respond(client, stream, bytes);

                    StopListening();
                    
                }
                Console.WriteLine("server stopped");
                
            }
            catch (Exception e)
            {
                
                Console.WriteLine("Error..... " + e.ToString());
            }
        }

        public void RequestStop()
        {
            _continue = false;
            StopListening();
        }

        private void StopListening()
        {
            if (stream!=null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
            listener.Stop(); 
            
        }

        
    }
}
