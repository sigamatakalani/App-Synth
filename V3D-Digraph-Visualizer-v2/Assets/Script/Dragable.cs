using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{
    public float catchingDistance = 3f;
    bool isDragging = false;
    GameObject draggingObject;

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isDragging)
            {
                draggingObject = GetObjectFromMouseRaycast();
                if (draggingObject)
                {
                    draggingObject.GetComponent<Rigidbody>().isKinematic = true;
                    //draggingObject.GetComponent<Renderer>().material.color = Color.blue;
                    isDragging = true;
                }
            }
            else if (draggingObject != null)
            {
                draggingObject.GetComponent<Rigidbody>().MovePosition(CalculateMouse3DVector(draggingObject.transform.position.z));
            }
        }
        else
        {
            if (draggingObject != null)
            {
                draggingObject.GetComponent<Rigidbody>().isKinematic = false;
                draggingObject.transform.GetComponent<Renderer>().material.color = Color.red;
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
    private Vector3 CalculateMouse3DVector(float z)
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        Debug.Log(v3); //Current Position of mouse in world space
        return v3;
    }
}