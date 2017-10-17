using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    bool rotateUpTrigger;
    bool rotateDownTrigger;
    bool rotateLeftTrigger;
    bool rotateRightTrigger;
    bool zoomInTrigger;
    bool zoomOutTrigger;

    // Use this for initialization
    void Start ()
    {
        rotateUpTrigger = false;
        rotateDownTrigger = false;
        rotateLeftTrigger = false;
        rotateRightTrigger = false;
        zoomInTrigger = false;
        zoomOutTrigger = false;
    }

    public void rotateUp()
    {
        rotateUpTrigger = true;
    }
    public void rotateDown()
    {
        rotateDownTrigger = true;
    }
    public void rotateLeft()
    {
        rotateLeftTrigger = true;
    }
    public void rotateRight()
    {
        rotateRightTrigger = true;
    }    
    public void zoomIn()
    {
        zoomInTrigger = true;
    }

    public void zoomOut()
    {
        zoomOutTrigger = true;
    }

	//haltTriggers
    public void rotateUpHalt()
    {
        rotateUpTrigger = false;
    }
    public void rotateDownHalt()
    {
        rotateDownTrigger = false;
    }
    public void rotateLeftHalt()
    {
        rotateLeftTrigger = false;
    }
    public void rotateRightHalt()
    {
        rotateRightTrigger = false;
    }

    public void zoomInHalt()
    {
        zoomInTrigger = false;
		zoomOutTrigger = false;
    }

    public void zoomOutHalt()
    {
		zoomInTrigger = false;
        zoomOutTrigger = false;
		//GameObject.FindWithTag ("Graph").GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void back()
    {
        SceneManager.LoadScene("startScreen");
    }


    // Update is called once per frame
    void Update ()
    {
		if(rotateUpTrigger)
        {
            GameObject.FindWithTag("Graph").transform.RotateAround(GameObject.FindWithTag("Graph").transform.position, new Vector3(1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * 1f);
        }

        if (rotateDownTrigger)
        {
            GameObject.FindWithTag("Graph").transform.RotateAround(GameObject.FindWithTag("Graph").transform.position, new Vector3(-1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * 1f);
        }

        if (rotateLeftTrigger)
        {
            GameObject.FindWithTag("Graph").transform.RotateAround(GameObject.FindWithTag("Graph").transform.position, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * 1f);
        }

        if (rotateRightTrigger)
        {
            GameObject.FindWithTag("Graph").transform.RotateAround(GameObject.FindWithTag("Graph").transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * 1f);
        }

        if(zoomInTrigger)
        {
            GameObject.FindWithTag("Graph").transform.Translate(Vector3.back * Time.deltaTime, Space.World);
        }

		if (zoomOutTrigger) 
		{
			GameObject.FindWithTag("Graph").transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
		} 

    }
}
