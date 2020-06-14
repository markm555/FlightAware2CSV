using System;
/* 
Author: Mark Moore
Created: 3/12/2017
Pull data directly from Dump 1090 on a PiAware using the default port of 30003 and write it to a csv file
*/
using System.Net.Sockets;

namespace PiAware2CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Connect("YourPiAwareIPaddress","");

        }
        static void Connect(String server, String message)
        {
            int i = 0;
            int n = 20000000;
            Int32 port = 30003;

            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\users\\markm.moorecasa\\documents\\data.csv");

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                port = 30003;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                file.WriteLine("MSG,Type,ID,Mode,ID1,ID2,Date,Time,Date2,Time2,CallSign,Altitue,Speed,Track,Lat,Long,Vrate,Squawk,Alert,EMG,SPI,IsOnGround");

                while (i < n)
                {
                  
                   
                    NetworkStream stream = client.GetStream();

                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    //Console.WriteLine(responseData);

                    file.Write(responseData);
                    i++;
                }

                // stream.Close();
                client.Close();
                file.Close();
            }

            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}