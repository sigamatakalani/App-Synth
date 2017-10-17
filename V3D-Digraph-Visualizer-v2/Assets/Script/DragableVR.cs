using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableVR : MonoBehaviour {

    public float catchingDistance = 3f;
    bool isDragging = false;
    GameObject draggingObject;
    Rigidbody dragObjectRigidBody;
    Color prevColour;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isDragging)
            {
                draggingObject = GetObjectFromMouseRaycast();
                if (draggingObject)
                {
                    draggingObject.GetComponent<Rigidbody>().isKinematic = true;
                    prevColour = draggingObject.GetComponent<Renderer>().material.color;
                    draggingObject.GetComponent<Renderer>().material.color = Color.blue;
                    isDragging = true;
                }
            }
            else if (draggingObject != null)
            {
                draggingObject.GetComponent<Rigidbody>().MovePosition(CalculateMouse3DVector(draggingObject.transform.position));
            }
        }
        else
        {
            if (draggingObject != null)
            {
                dragObjectRigidBody = draggingObject.GetComponent<Rigidbody>();
                dragObjectRigidBody.isKinematic = false;
                draggingObject.transform.GetComponent<Renderer>().material.color = prevColour;
            }
            isDragging = false;
        }
    }

    private GameObject GetObjectFromMouseRaycast()
    {
        GameObject gmObj = null;
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() &&
                Vector3.Distance(hitInfo.collider.gameObject.transform.position,
                transform.position) <= catchingDistance)
            {
                gmObj = hitInfo.collider.gameObject;
            }
        }
        return gmObj;
    }
    private Vector3 CalculateMouse3DVector(Vector3 vertexPos)
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }
}
