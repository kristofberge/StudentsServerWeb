using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StudentsServerWeb
{
    abstract class AbstractResponder
    {

        public abstract void Respond(TcpClient client, NetworkStream stream, byte[] message);
    }
}
