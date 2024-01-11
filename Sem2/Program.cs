using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Sem2
{
    internal class Program
    {
        static int[] arr1 = { 1, 2, 3 };
        static int[] arr2 = { 1, 2, 3 };

        static int val1;
        static int val2;

        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();
            Dictionary<IPAddress, long> pings = new Dictionary<IPAddress, long>();

            string[] resources = { "yandex.ru", "google.com" };
            foreach (var item in resources)
            {
                IPAddress[] ipAdreses = Dns.GetHostAddresses(item, AddressFamily.InterNetwork);
                foreach (IPAddress ip in ipAdreses)
                {

                    var tr = new Thread(() =>
                    {
                        Ping p = new Ping();
                        PingReply pingReply = p.Send   (ip);
                        pings.Add(ip, pingReply.RoundtripTime);

                    });
                    tr.Start();
                    threads.Add(tr);
                    
                }
                foreach (Thread tr in threads)
                {
                    tr.Join();
                }
                long min = long.MaxValue;
                foreach(var ping in pings)
                {
                    Console.WriteLine(String.Format($"{ping.Key} - {ping.Value} ms"));
                    if (ping.Value < min)
                    {
                        min = ping.Value;
                    }
                }
                Console.WriteLine(min);
            }
            
            

            /*Thread t1 = new Thread(countSumArr1);
            Thread t2 = new Thread(countSumArr2);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(val1 + val2);*/
        }

        public static void countSumArr1()
        {
            val1 = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                val1 += arr1[i];
            }
        }

        public static void countSumArr2()
        {
            val2 = 0;
            for (int i = 0; i < arr2.Length; i++)
            {
                val2 += arr2[i];
            }
        }

    }
}
