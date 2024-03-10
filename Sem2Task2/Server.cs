using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sem2Task2
{
    internal class Server
    {
        public static void AcceptMsg()
        {
            bool flag = true;
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(16874);
            Console.WriteLine("Сервер ожидает сообщение");
            {
                

                byte[] buffer = udpClient.Receive(ref ep);
                string data = Encoding.UTF8.GetString(buffer);
                Thread tr = new Thread(() =>
                {
                    Message msg = Message.fromJson(data);
                    Message responseMsg;
                    string responseMsgJs;
                    byte[] responseDate;
					if (msg.Text == "exit")
					{
						Console.WriteLine("Serv stop working!");
                        responseMsg = new Message("Server", "Serv stop working!");
						responseMsgJs = responseMsg.toJson();
						responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
						udpClient.Send(responseDate, ep);
                        flag = false;
                        
					}
                    else
                    {
						responseMsg = new Message("Server", "Message accept on serv!");
						responseMsgJs = responseMsg.toJson();
                    Console.WriteLine(msg.ToString());
                    udpClient.Send(responseDate, ep);
					}
                }); 

				if (flag)
                {

                tr.Start();
                    

				}
                else
                {
                    return;
                }
            }
        }
    }
}
