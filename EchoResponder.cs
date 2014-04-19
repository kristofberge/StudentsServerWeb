using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StudentsServerWeb
{
    class EchoResponder:AbstractResponder
    {
        public override void Respond(TcpClient client, NetworkStream stream, byte[] bytesReceived) 
        {
            string msgIncoming = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);
            byte[] bytesResponse = Encoding.ASCII.GetBytes("Echo: " + msgIncoming);
            stream.Write(bytesResponse, 0, bytesResponse.Length);


        }
    }
}
