using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentsServerWeb
{
    public partial class ServerManager : System.Web.UI.Page
    {

        static private SimpleServer server;
        static private Thread serverThread;
        private string localIp;

        private const string SERVER_TYPE_ECHO = "Echo";
        private const string SERVER_TYPE_STUDENTS = "Students data";

        private const string STATUS_NOT_RUNNING = "Server not running.";
        private const string STATUS_NO_TYPE_SELECTED = "Please select a server type.";
        private const string STATUS_INITIATING_SERVER = "Initiating server.";
        private const string STATUS_SERVER_RUNNING = " server running.";

        private SqlHelper _helper;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            List<Class> classesList = new List<Class>();
            _helper = new SqlHelper(StudentsDatabase.GetInstance());
            classesList = _helper.GetClasses();
            foreach(Class c in classesList){
                ddlClasses.Items.Add(c.ToString());
            }
        }

        protected void bStart_Click(object sender, EventArgs e)
        {

            localIp = "192.168.0.105";
            
            if (rblServerType.SelectedItem != null)
            {
                string selectedType = rblServerType.SelectedValue;
                
                switch (selectedType)
               {
                    case SERVER_TYPE_ECHO:
                        server = new SimpleServer(localIp, new EchoResponder());
                        lblStatus.Text = "Starting Echo server.";
                        break;

                    case SERVER_TYPE_STUDENTS:
                        lblStatus.Text = "Starting Students data server.";
                        server = new SimpleServer(localIp, new StudentsDataResponder(_helper));
                        break;

               }
                lblStatus.Text = STATUS_INITIATING_SERVER;
                serverThread =new Thread(new ThreadStart(() => server.startListening(666)));
                serverThread.Start();
                lblStatus.Text = selectedType + STATUS_SERVER_RUNNING;
            }

            else
            {
                lblStatus.Text = STATUS_NO_TYPE_SELECTED;
            }

            

        }

        private string GetLocalIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string result = "";
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    result = ip.ToString();
                    break;
                }
            }
            return result;
        }

        protected void bStop_Click(object sender, EventArgs e)
        {
            server.RequestStop();
            serverThread.Join();
            lblStatus.Text = STATUS_NOT_RUNNING;
        }
    }
}