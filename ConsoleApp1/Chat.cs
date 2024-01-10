using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Chat
    {

        public static void Server()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            UdpClient ucl = new UdpClient(12345);
            Console.WriteLine("Server wait msg for client");
            while (true)
            {
                try
                {
                    byte[] bytes = ucl.Receive(ref ep);
                    string str1 = Encoding.UTF8.GetString(bytes);

                    Message msg = Message.fromJson(str1);
                    if (msg != null)
                    {
                        Console.WriteLine(msg);
                        string responseMsgJson = (new Message("Server", "I accept your message").toJson());
                        byte[] responseMsgBytes = Encoding.UTF8.GetBytes(responseMsgJson);
                        ucl.Send(responseMsgBytes, ep);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect message");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

        }

        public static void Client(string name, string text)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient ucl = new UdpClient();



            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(text))
            {
                Console.WriteLine("Incorrect data, please try again ");

            }
            else
            {
                Message msg = new Message(name, text);
                string msgJs = msg.toJson();
                byte[] data = Encoding.UTF8.GetBytes(msgJs);
                ucl.Send(data, ep);
                Console.WriteLine();

            }

            byte[] bytes = ucl.Receive(ref ep);
            string str1 = Encoding.UTF8.GetString(bytes);

            Message responseMsg = Message.fromJson(str1);
            if (responseMsg != null)
            {
                Console.WriteLine(responseMsg);
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }

    }
}
