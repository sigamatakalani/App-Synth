using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    
	// Use this for initialization
	void Start ()
    {
        graphStart graph = GetComponent<graphStart>();
        GameObject.FindWithTag("RotateLeft");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
