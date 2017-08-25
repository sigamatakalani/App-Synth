﻿using UnityEngine;
using UnityEngine.Events;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class VREventHandler : MonoBehaviour {

    private VRInteractiveItem interactiveItem;
    public UnityEvent GazeEnterEvent;
    public UnityEvent GazeExitEvent;

    // Use this for initialization
    void Start () {
        interactiveItem = GetComponent<VRInteractiveItem>();
        interactiveItem.OnOver += OnGazeEnter;
        interactiveItem.OnOut += OnGazeExit;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGazeEnter() {
        GazeEnterEvent.Invoke();
    }


    void OnGazeExit()
    {
        GazeExitEvent.Invoke();
    }
}