using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;
using SimpleJson;

public class Client : MonoBehaviour {

    static NetworkStream stream = null;
    public static TcpClient client = null;

    // Use this for initialization
    void Start () {
        client = new TcpClient();
        client.Connect("127.0.0.1", 8086);
        stream = client.GetStream();
        Thread t = new Thread(() => ReadFromStream(stream));
        t.Start();
        
    }

    // Update is called once per frame
    void Update () {
        
    }

    public Client GetClient()
    {
        return this;
    }

    public static void RequestSpawnPlayer(string playername)
    {
        JSONObject json = new JSONObject();
        json.AddField("RequestType", 2);
        json.AddField("PlayerName", playername);
        string msg = json.Print();
        Send(msg);
    }

    public JSONObject ResponseSpawnPlayer(string response)
    {
        JSONObject jsonResponse = new JSONObject(response);
        return jsonResponse;
    }

    public static void Send(object message)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(message.ToString());
        stream.Write(bytes, 0, bytes.Length);
    }

    public void ReadFromStream(NetworkStream stream)
    {
        byte[] buffer = new byte[2048];
        var bytes = 0;
        while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, bytes);
            Debug.Log(Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length));
            var fromServer = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
            JSONObject json = new JSONObject(fromServer);
            //this.SendMessage(json.GetField("functionName").str, json.GetField("functionObject").str);
            ms.Close();
        }
    }
}
