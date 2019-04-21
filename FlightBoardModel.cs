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
        private TcpClient mySender;
        // Incoming data from the client.  
        public static string data = null;
        private string[] myValues;

        public void sendMessage(string message)
        {
            string temp = message;
            temp += " ";
            temp += "\r\n";
            // Encode the data string into a byte array.  
            byte[] encodeMessage = Encoding.ASCII.GetBytes(temp);
            NetworkStream stream = this.mySender.GetStream();
            stream.Write(encodeMessage, 0, encodeMessage.Length);
            stream.Flush();
            stream.Close();
            // Send the data through the socket.  
        }

        public void closeClient()
        {
            // Release the socket.  
            this.mySender.Close();
        }

        public void closeServer()
        {
            // Release the socket.  
            this.myServer.Stop();
        }

        public void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.    
                IPAddress ipAddress = IPAddress.Parse(Model.ApplicationSettingsModel.Instance.FlightServerIP);
                // Create a TCP/IP  socket.  
                TcpClient sender = new TcpClient();
                this.mySender = sender;
                IPEndPoint local = new IPEndPoint(ipAddress, Model.ApplicationSettingsModel.Instance.FlightCommandPort);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(local);
                    Console.WriteLine("Socket connected");
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
