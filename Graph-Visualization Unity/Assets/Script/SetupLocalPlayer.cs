using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar] //tells server to sync(one way) variable out to all clients
    public string Pname = "player";
    public void OnGUI()
    {
        if (isLocalPlayer)
            Pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), Pname);
    }

    // Use this for initialization
    public void Start () {
		if(isLocalPlayer)
        {
            GetComponent<Drive>().enabled = true;
        }
	}

    public void Update()
    {
        if (isLocalPlayer)
            this.GetComponentInChildren<TextMesh>().text = Pname;
    }

}
