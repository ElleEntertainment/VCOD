using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using VCOD_Server.Database;

namespace VCOD_Server
{
    //Packet Header
    enum RequestType
    {
        SpawnWorld = 1,
        SpawnPlayer = 2
    }

    class Program
    {

        static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8086);
            server.Start();

            while(true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Thread t = new Thread(() => Session.createThread(client));
                t.Start();
                Console.WriteLine("client connected.");
            }
        }
    }

    class Session
    {
        public static void createThread(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[2048];
            var bytes = 0;
            while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(buffer, 0, bytes);
                Console.WriteLine(Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length));
                var json = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
                dynamic obj = JsonConvert.DeserializeObject(json);
                ms.Close();

                switch((RequestType)obj.RequestType)
                {
                    case RequestType.SpawnWorld:
                        var WorldInfo = JsonConvert.SerializeObject(Nemici_info.GetAllNemiciInfo());
                        Send(stream, WorldInfo);
                        break;
                    case RequestType.SpawnPlayer:
                        Player p = new Player((string)obj.PlayerName);
                        var PlayerInfo = JsonConvert.SerializeObject(p);
                        Send(stream, PlayerInfo);
                        break;
                }
            }
            
        }

        public static void Send(NetworkStream stream, object message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }

    }
}
