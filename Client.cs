using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public sealed class Client
    {
        /* #region Singleton
         private static TcpClient myClient = null;
         public static TcpClient Instance
         {
             get
             {
                 if (myClient == null)
                 {
                     myClient = new TcpClient();
                 }
                 return myClient;
             }
         }
         #endregion */

        private TcpClient myClient;
        private static int counter = 0;
        private static Client client = null;
        public static Client InstanceClass
        {
            get
            {
                if (client == null)
                {
                    client = new Client();
                }
                counter++;
                return client;
            }
        }
        


        public void sendMessage(string message)
        {
            string temp = message;
            temp += " ";
            temp += "\r\n";
            // Encode the data string into a byte array.  
            byte[] encodeMessage = Encoding.ASCII.GetBytes(temp);
            NetworkStream stream = myClient.GetStream();
            stream.Write(encodeMessage, 0, encodeMessage.Length);
            stream.Flush();
            // Send the data through the socket.  
        }

        public void closeClient()
        {
            // Release the socket.  
            myClient.Close();
        }

        public void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.    
                IPAddress ipAddress = IPAddress.Parse(Model.ApplicationSettingsModel.Instance.FlightServerIP);
                // Create a TCP/IP  socket.  
                IPEndPoint local = new IPEndPoint(ipAddress, Model.ApplicationSettingsModel.Instance.FlightCommandPort);
                // Connect the socket to the remote endpoint. Catch any errors.  
                myClient = new TcpClient();
                try
                {
                    myClient.Connect(local);
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

    }
}
