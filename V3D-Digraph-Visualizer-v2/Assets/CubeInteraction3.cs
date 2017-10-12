using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class CubeInteraction3 : MonoBehaviour
{

    private float timer;
    public float gazeTime = 2f;
    private bool gazedAt;
    private bool gazeDown;

    private Vector3 initialPosition = new Vector3(-0.5f, 0f, -0.1f);
    private Vector3 initialScale = new Vector3(0.01f, 1.1f, 1.1f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (gazedAt)
        {

            //Debug.Log("Gazed At True");
            timer += Time.deltaTime;
            //Debug.Log(timer);

            Transform child = transform.GetChild(0);
            Vector3 newScale = new Vector3(timer / gazeTime, child.localScale.y, child.localScale.z);
            Vector3 newPosition = new Vector3(-0.5f + (timer / gazeTime) / 2, child.localPosition.y, child.localPosition.z);

            child.localScale = newScale;
            child.localPosition = newPosition;

            if (timer >= gazeTime)
            {

                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0f;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void pointerEnter()
    {
        Debug.Log("Pointer Enter");
        gazedAt = true;
    }

    public void pointerExit()
    {
        Debug.Log("Pointer Exit");
        gazedAt = false;

        if (!gazeDown)
        {

            //reset position
            Transform child = transform.GetChild(0);
            child.localScale = initialScale;
            child.localPosition = initialPosition;
            timer = 0f;
        }
    }

    public void pointerDown(string sceneNameVar)
    {

        Debug.Log("Pointer Down");
        gazeDown = true;
        GameObject info = GameObject.Find("InformationObject");
        info.GetComponent<InformationScript>().jsonToSend = "{\"items\": [ {\"child\": \"Node2\",\"parent\": \"Node1\",\"value\": 11},{\"parent\": \"Node2\",\"value\": 22,\"child\": \"Node3\"},{\"parent\": \"Node2\",\"value\": 33,\"child\": \"Node4\"},{\"parent\": \"Node2\",\"value\": 44,\"child\": \"Node5\"},{\"parent\": \"Node3\",\"value\": 55,\"child\": \"Node5\"},{\"parent\": \"Node4\",\"value\": 66,\"child\": \"Node5\"},{\"parent\": \"Node5\",\"value\": 77,\"child\": \"Node6\"},{\"parent\": \"Node7\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"},{\"parent\": \"Node5\",\"value\": 88,\"child\": \"Node4\"}]}";
        Application.LoadLevel(sceneNameVar);
    }
}
