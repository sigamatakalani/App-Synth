using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using vdrGraph;

using UnityEditor;
 
[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{
    public float catchingDistance = 3f;
    bool isDragging = false;
    GameObject draggingObject;
    Rigidbody dragObjectRigidBody;

    List<EdgePairs> graphPairs;
    int [] visitedNodes;
    Vector3 startPosition;

    Vector3 forceGenerated;

    // Use this for initialization
    void Start()
    {
        startPosition = draggingObject.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (!isDragging)
            {
                draggingObject = GetObjectFromMouseRaycast();
                startPosition = draggingObject.transform.position;
                if (draggingObject)
                {
                    draggingObject.GetComponent<Rigidbody>().isKinematic = true;
                    draggingObject.GetComponent<Renderer>().material.color = Color.blue;
                    isDragging = true;
                }
            }
            else if (draggingObject != null)
            {
                Vector3 finalPosition = CalculateMouse3DVector(draggingObject.transform.position);
                visitedNodes = null;
                

                draggingObject.GetComponent<Rigidbody>().MovePosition(finalPosition);

                forceGenerated = startPosition - finalPosition;
                //graphPairs = draggingObject.GetComponent<List<EdgePairs>>();
                LinkedList<GameObject> linked = new LinkedList<GameObject>();
                //List<EdgePairs> graphPairs = graphStart.getNodePairsList();
                Debug.Log("Hello");
                linked.AddFirst(draggingObject);
               
                 graphPairs = graphStart.getNodePairList();
                foreach (EdgePairs pairing in graphPairs)
                {
                    GameObject nodeOne = pairing.parent;
                    GameObject nodeTwo = pairing.child;
                    if(nodeOne == draggingObject)
                    {
                        linked.AddLast(nodeTwo);
                    }
                    else if(nodeTwo == draggingObject)
                    {
                        linked.AddLast(nodeOne);
                    }
                }

                visitedNodes = new int[linked.Count];
                Dfs(linked, draggingObject, forceGenerated);
            }
        }
        else
        {
            if (draggingObject != null)
            {
                dragObjectRigidBody = draggingObject.GetComponent<Rigidbody>();
                dragObjectRigidBody.isKinematic = false;
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
    private Vector3 CalculateMouse3DVector(Vector3 vertexPos)
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void Dfs(LinkedList<GameObject> linked, GameObject nodeObject, Vector3 forceEntered)
    {
        
        //nodeObject.transform.Translate(forceEntered);
        if(graphPairs != null)
        {
            //draggingObject.GetComponent<Rigidbody>().MovePosition(forceEntered);
            //Destroy(GetComponent<Rigidbody>());
            Debug.Log(startPosition);
            Debug.Log( forceEntered);
            //Debug.Log(CalculateMouse3DVector(cars.transform.position))
            foreach(GameObject cars in linked)
            {
                Debug.Log("May: 1");
                Debug.Log( cars.transform.position);
                //cars.GetComponent<Rigidbody>().MovePosition( forceEntered);
            }
            
        }
        else
        {
            //draggingObject.GetComponent<Rigidbody>().MovePosition(forceEntered);
        }

    }

    /*protected void applyNodeLaws(float iTimeStep)
    {
        for (int i = 0; i < graph.nodes.Count; i++)
        {
            //applies coulomb's law to each node 
            Point3D point1 = GetPoint(graph.nodes[i]);
            for (int j = 0; j < graph.nodes.Count; j++)
            {
                Point3D point2 = GetPoint(graph.nodes[j]);
                if (point1 != point2)
                {
                    Vector3 d = point1.position - point2.position;
                    float distance = d.magnitude + 0.1f;
                    Vector3 direction = d.normalized;

                    point1.ApplyForce((direction * Repulsion) / (distance * 0.5f));
                    point2.ApplyForce((direction * Repulsion) / (distance * -0.5f));

                }
            }
            //Applies centre attraction
            Vector3 centreDirection = point1.position * -1.0f;
            float displacement = centreDirection.magnitude;
            centreDirection = centreDirection.normalized;
            point1.ApplyForce(centreDirection * (Stiffness * displacement * 0.4f));

            //converts acceleration to velocity
            point1.velocity += (point1.acceleration * iTimeStep);
            point1.velocity *= Damping;
            point1.acceleration = Vector3.zero;
            //applies velocity to position
            point1.position += (point1.velocity * iTimeStep);


        }
    }*/

    /************************************************************************************************
     * @Description     applied hookes law to determine the spring force between 2 points           *
     ************************************************************************************************/
    /*protected void applyHookesLaw()
    {
        for (int i = 0; i < graph.edges.Count; i++)
        {
            Spring3D spring = GetSpring(graph.edges[i]);
            Vector3 d = spring.point2.position - spring.point1.position;
            float displacement = spring.Length - d.magnitude;
            Vector3 direction = d.normalized;

            spring.point1.ApplyForce(direction * (spring.K * displacement * -0.5f));
            spring.point2.ApplyForce(direction * (spring.K * displacement * 0.5f));
        }
    }*/
}