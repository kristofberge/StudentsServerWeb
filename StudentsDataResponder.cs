using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace StudentsServerWeb
{
    class StudentsDataResponder:AbstractResponder
    {
        private SqlHelper _helper;

        private const string REQ_SEND_CLASS_LIST = "sndclslt";
        private const string REQ_SEND_STUD_LIST = "sndstdlt";
        private const string CMD_START_CLASSLIST = "stclassl";
        private const string CMD_END_LIST = "end_list";
        private const string CMD_START_STUDLIST = "ststudlt";
        private const string CMD_SEND_CLASS_ID = "sndclsid";


        public StudentsDataResponder(SqlHelper helper) {
            this._helper = helper;
        }

        

        public override void Respond(TcpClient client, NetworkStream stream, byte[] message)
        {
            string msgIncoming = Encoding.ASCII.GetString(message, 0, message.Length);
            System.Diagnostics.Debug.WriteLine("RECEIVED//" + msgIncoming + "//");

            switch (msgIncoming) 
            {
                case REQ_SEND_CLASS_LIST:
                    
                    List<Class> classList = _helper.GetClasses();
                    SendData(classList, stream);
                    break;

                case REQ_SEND_STUD_LIST:
                    System.Diagnostics.Debug.WriteLine("Requesting class ID");
                    string msgToSend = CMD_SEND_CLASS_ID;
                    byte[] bytesToSend = Encoding.ASCII.GetBytes(msgToSend);
                    stream.Write(bytesToSend, 0, bytesToSend.Length);

                    message = new byte[5];
                    stream.Read(message, 0, message.Length);
                    msgIncoming = Encoding.ASCII.GetString(message, 0, message.Length).Trim();
                    System.Diagnostics.Debug.WriteLine("RECEIVED//" + msgIncoming + "//");
                    List<Student> studentsList = _helper.GetStudentsInClass(new Class(Convert.ToInt32(msgIncoming), ""));
                    SendData(studentsList, stream);
                    break;
            }
        }

        private void SendData(List<Student> studentsList, NetworkStream stream)
        {
            string line = CMD_START_STUDLIST;

            System.Diagnostics.Debug.WriteLine("SENDING//" + line + "//");
            byte[] bytesResponse = Encoding.ASCII.GetBytes(line);

            stream.Write(bytesResponse, 0, bytesResponse.Length);

            foreach(Student s in studentsList){
                line = s.id + ";" + s.name + ";" + s.grade + ";" + s.height;
                SendLine(stream, line);
            }

            line = CMD_END_LIST;
            SendLine(stream, line);
        }

        private void SendData(List<Class> classList, NetworkStream stream)
        {
            string line = CMD_START_CLASSLIST;

            System.Diagnostics.Debug.WriteLine("SENDING//" + line + "//");
            byte[] bytesResponse = Encoding.ASCII.GetBytes(line);

            stream.Write(bytesResponse, 0, bytesResponse.Length);
            

            foreach(Class c in classList){
                line = c.id + ";" + c.name;
                SendLine(stream, line);
            }

            line = CMD_END_LIST;
            SendLine(stream, line);
        }

        private void SendLine(NetworkStream stream, string line)
        {
            System.Diagnostics.Debug.WriteLine("SENDING//" + line + "//");
            byte[] bytesResponse = Encoding.ASCII.GetBytes(line);
            byte[] lengthOfNextLine = GetLength(bytesResponse);

            stream.Write(lengthOfNextLine, 0, lengthOfNextLine.Length);
            
            stream.Write(bytesResponse, 0, bytesResponse.Length);
        }

        private byte[] GetLength(byte[] bytesResponse)
        {
            int length = bytesResponse.Length;
            byte[] result = new byte[1];
            System.Diagnostics.Debug.WriteLine("length: " + length);

            result[0] = Convert.ToByte(length);
            
            return result;
        }
        
    }
}