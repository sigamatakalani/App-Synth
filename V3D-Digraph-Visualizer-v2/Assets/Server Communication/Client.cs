using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using socket.io;

public class Client : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        var serverUrl = "http://localhost:7001";
        var socket = Socket.Connect(serverUrl);

        socket.On(SystemEvents.connect, () => 
        {
            // send "ferret" event
            socket.Emit("ferret", "toby", (string r) => 
            {
                Debug.Log(r);
            }); // set callback handler for Ack
        });
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
