using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InformationScript : MonoBehaviour
{

    public string jsonToSend = "";
	public string fileName = "";
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
