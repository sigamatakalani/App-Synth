using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using socket.io;

public class BlackBoardScript : MonoBehaviour 
{
	Socket socket;
	string serverUrl;
	// Use this for initialization
	void Start () 
	{
		// serverUrl = "http://localhost:3000";
        // socket = Socket.Connect(serverUrl);
        // socket.On(SystemEvents.connect, () => 
        // {
        //     socket.Emit("status", "BlackBoard connected...");
        // });

		// socket.On("externalGraphData", (string data) =>
        // {
		// 	Debug.Log(data);
		// 	//JsonObj [] graphData = JsonHelper.FromJson<JsonObj>(data);
		// });

		// socket.On("receiveGraphData", (string data) =>
		// {
		// 	Debug.Log(data);
		// });
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.data;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.data = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.data = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] data;
    }
}	


