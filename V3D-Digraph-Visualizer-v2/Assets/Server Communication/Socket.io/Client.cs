using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using socket.io;
using WebSocketSharp;
using WebSocketSharp.Net;

public class Client : MonoBehaviour
{
    private WebSocket socket;
	// Use this for initialization
	void Start ()
    {
        // var serverUrl = "http://localhost:3000";
        // var socket = Socket.Connect(serverUrl);

        // socket.On(SystemEvents.connect, () => 
        // {
        //     socket.Emit("I have officially connected...");
        //     socket.Emit("getGraphData", "graph1");
        // });

        // //receive graph data
        // socket.On("receiveGraphData", (string data) =>
        // {
        //     Debug.Log("Recieving graph data: " + data);
        // });
        // string message = "";

        // socket = new WebSocket("ws://localhost:8080");

        // socket.OnOpen += (sender, e) =>
        // { 
        //     socket.Send ("{\"request\":\"getGraphData\", \"payload\":\"graph1\"}");
        // };

        // socket.OnMessage += (sender, e) => 
        // {
        //     if(e.IsText)
        //     {
        //         message = JsonUtility.FromJson<string>(e.Data);
        //         Debug.Log("The message says: " + message);
        //     }
        // };
            
        // socket.OnError += (sender, e) =>
        // {
        //     socket.Send("Error: " +  e.Message);
        // };

        // socket.OnClose += (sender, e) =>
        // {
        //     socket.Send("Connection closed: " + e.Reason);
        // };

        // socket.Connect();

        // Debug.Log("I should have connected at this point in time!");
    }
	
	//Update is called once per frame
	void Update()
    {
		
	}
}
