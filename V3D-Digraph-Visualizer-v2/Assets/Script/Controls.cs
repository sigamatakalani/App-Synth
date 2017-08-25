using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    bool rotateUpTrigger;
    bool rotateDownTrigger;
    bool rotateLeftTrigger;
    bool rotateRightTrigger;

    // Use this for initialization
    void Start ()
    {
        rotateUpTrigger = false;
        rotateDownTrigger = false;
        rotateLeftTrigger = false;
        rotateRightTrigger = false;

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

    public void back()
    {
        SceneManager.LoadScene("fileSelector");
    }


    // Update is called once per frame
    void Update ()
    {
		if(rotateUpTrigger)
        {
            GameObject.FindWithTag("Graph").transform.Rotate(1f, 0f, 0f);
        }
        if (rotateDownTrigger)
        {
            GameObject.FindWithTag("Graph").transform.Rotate(-1f, 0f, 0f);
        }
        if (rotateLeftTrigger)
        {
            GameObject.FindWithTag("Graph").transform.Rotate(0f, -1f, 0f);
        }
        if (rotateRightTrigger)
        {
            GameObject.FindWithTag("Graph").transform.Rotate(0f, 1f, 0f);
        }
    }
}
