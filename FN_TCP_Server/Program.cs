using System;
using SimpleTCP;

using System.Text;

namespace FN_TCP_Server
{
    class Program
    {
        
        static SimpleTcpClient client;
        // static SimpleTcpServer server;
        static string response;
    


        static void Main(string[] args)
        {
            string cmd;
            bool run = true;
            TimeSpan t = TimeSpan.FromSeconds(5);

            try
            {
                client = new SimpleTcpClient();
                client.DataReceived += responseHandler;
                
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e);
                Console.WriteLine("$");
                Console.ReadLine();
            }

            while (run)
            {
                Console.WriteLine("$");
                cmd = Console.ReadLine().ToLower();
                string[] cmdArgs = cmd.Split(" ");
                switch (cmdArgs[0])
                {
                    case "start":
                        //Start Program:
                        //Initiate TCP Client Connections to Devices

                        try
                        {
                            client.Connect(cmdArgs[1], 7078);
                            Console.WriteLine("TCP Connectrion established");

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could not establish connection");
                            Console.WriteLine("ERROR: {0}", e);
                            Console.WriteLine("$");
                            Console.ReadLine();
                        }
                        break;
                    case "stop":
                        try
                        {
                            client.Dispose();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: {0}", e);
                            Console.ReadLine();
                        }
                        Console.WriteLine("Server stopped");
                        break;
                    case "returnstate":
                       
                        var replyMsg = client.WriteLineAndGetReply("FN,SRE\r",TimeSpan.FromSeconds(3));
                        Console.WriteLine("FN State:{0}",replyMsg.GetType()) ;
                        
                        break;
                    case "quit":
                        break;
                    case "help":
                        Console.WriteLine("Type \"start\" to start the server\nType \"stop\" to stop the server\nType \"quit\" to quit");
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }

        }
        
        private static void responseHandler(object sender, Message e)
        {
         
            response = e.MessageString;
            Console.WriteLine(response);
        }
        
    }
}
