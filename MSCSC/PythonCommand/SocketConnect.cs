using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using MSCLoader;



namespace MSCSC
{
    public static class SocketConnect
    {
        public static Thread mThread;
        public static string connectionIP = "127.0.0.1";
        public static int connectionPort = 25001;
        static IPAddress localAdd;
        public static TcpListener listener;
        public static TcpClient client;
        static bool running;
        public static string recievedMessage;
        public static string dataReceived;
        public static bool message_recieved;
        private static NetworkStream nwStream;
        private static byte[] buffer;
        public static ThreadStart ts;
        //public static string response_message;

        public static void StartThread()
        {
            ts = new ThreadStart(GetInfo);
            mThread = new Thread(ts);
            mThread.Start();
            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Starting new thread...</color>");
            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>New thread started.</color>");
            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Searching for TCP client...</color>");
        }
        public static void GetInfo()
        {
            localAdd = IPAddress.Parse(connectionIP);
            listener = new TcpListener(IPAddress.Any, connectionPort);
            listener.Start();

            client = listener.AcceptTcpClient();

            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>TCP client found.</color>");
            ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>Connected to: </color>" + connectionIP.ToString() + ":" + connectionPort.ToString());

            running = true;
            while (running)
            {
                RecieveData();
            }
            listener.Stop();
        }

        public static void RecieveData()
        {
            nwStream = client.GetStream();
            buffer = new byte[client.ReceiveBufferSize];

            //---receiving Data from the Host----
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
            dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

            if (dataReceived != null)
            {
                //---Using received data---
                message_recieved = true;
                recievedMessage = dataReceived;
            }
            else
            {
                message_recieved = false;
            }
        }

        public static void SendData(string response_message)
        {
            if (dataReceived != null)
            {
                //---Sending Data to Host----
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(response_message); //Converting string to byte data
                nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
                message_recieved = true;
            }
            else
            {
                message_recieved = false;
            }
        }
    }
}

