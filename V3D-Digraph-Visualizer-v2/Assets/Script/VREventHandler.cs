using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class VREventHandler : MonoBehaviour {

    private VRInteractiveItem interactiveItem;

    // Use this for initialization
    void Start () {
        interactiveItem = GetComponent<interactiveItem>();
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
