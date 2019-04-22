using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightBoardModel
    {
        
        private TcpListener myServer;
        // Incoming data from the client.  
        public static string data = null;
        private string[] myValues;
        public string[] Values
        {
            get
            {
                return this.myValues;
            }
        }

        public void closeServer()
        {
            // Release the socket.  
            this.myServer.Stop();
        }


        public void StartListening()
        {
            // Data buffer for incoming data.  
            string adressIP = Model.ApplicationSettingsModel.Instance.FlightServerIP;
            IPAddress ipAddress = IPAddress.Parse(adressIP);
            // Create a TCP/IP socket.  
            TcpListener listener = new TcpListener(ipAddress, Model.ApplicationSettingsModel.Instance.FlightInfoPort);
            listener.Start();
            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.AcceptSocket();
                Thread serverThread = new Thread(() => Read(handler, this.myValues));
                serverThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.Write("OKKKKK"); 
        }

        public static void Read(Socket handler, string[] values)
        {
            byte[] bytes = new Byte[1024];
                data = null;
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    values = data.Split(',');
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                // Show the data on the console.  
                Console.WriteLine("Text received : {0}", data);   
        }
    }
}
