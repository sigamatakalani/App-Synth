using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using socket.io;

public class CubeScript : MonoBehaviour {

	// Use this for initialization
	GameObject cube;
	JsonObj jsonObj;
	string json;

	GameObject currentGameObj; 
	void Start () 
	{
		var serverUrl = "http://localhost:3000";
        var socket = Socket.Connect(serverUrl);
        cube = GameObject.FindGameObjectWithTag("cube");
		jsonObj = new JsonObj(cube.tag, cube.transform.position, cube.transform.rotation);
		string json = JsonUtility.ToJson(jsonObj);

        socket.On(SystemEvents.connect, () => 
        {
            socket.Emit("I have officially connected...");
            socket.Emit("instantiateObjects", "graph1");
        });

		socket.On("recieveInstantiation", (string data) =>
        {
			currentGameObj = GameObject.FindGameObjectWithTag("cube");
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey("left"))
            this.transform.Rotate(0f, -1f, 0f);
        if (Input.GetKey("right"))
            this.transform.Rotate(0f, 1f, 0f);
        if (Input.GetKey("up"))
            this.transform.Rotate(1f, 0f, 0f);
        if (Input.GetKey("down"))
            this.transform.Rotate(-1f, 0f, 0f);
		
		cube = GameObject.FindGameObjectWithTag("cube");
		jsonObj = new JsonObj(cube.tag, cube.transform.position, cube.transform.rotation);
		json = JsonUtility.ToJson(jsonObj);
		Debug.Log(json);
	}
}

[System.Serializable]
public class JsonObj
{
	public string name;
	public string tag;
	public float [] position;
	public float [] rotation;
	public JsonObj(string n, Vector3 p, Quaternion r)
	{
		name = n;
		tag = n;
		position = new float [3];
		rotation = new float [7];
		position[0] = p.x;
		position[1] = p.y;
		position[2] = p.z;
		rotation[0] = r.eulerAngles.x;
		rotation[1] = r.eulerAngles.y;
		rotation[2] = r.eulerAngles.z;
		rotation[3] = r.x;
		rotation[4] = r.y;
		rotation[5] = r.z;
		rotation[6] = r.w;
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
