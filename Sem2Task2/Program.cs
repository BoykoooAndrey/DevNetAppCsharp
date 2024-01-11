using System;

namespace Sem2Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Server.AcceptMsg();
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    
                    Client.SendMsg($"{args[0]} {i}");
                    
                    
                }
                
            }
            
            
        }
    }
}
