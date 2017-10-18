using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using vdrGraph;
using UnityEngine.VR;

[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{
    public float catchingDistance = 3f;
    public bool isDragging = false;
    GameObject draggingObject;
    Rigidbody dragObjectRigidBody;
    List<EdgePairs> graphPairs;
    Vector3 startPosition;
    Vector3 finalPosition = new Vector3(0, 0, 0);
    Color prevColor;
    float stiffness = 81.76f;
    float repulsion = 40000.0f;
    float damping = 0.5f;

    float forceGenerated;
    int [] visitedNodes;

    // Use this for initialization
    void Start()
    {
        //startPosition = draggingObject.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //VR DRAG
        updateTimer();
        checkTimeMouseDown();
        //


        if (mouseDown)//Input.GetMouseButton(0)
        {
            checkTimeStandStill();
            if (!isDragging)
            {
                draggingObject = GetObjectFromMouseRaycast();
                startPosition = draggingObject.transform.position;
                if (draggingObject)
                {
                    draggingObject.GetComponent<Rigidbody>().isKinematic = true;
                    prevColor = draggingObject.GetComponent<Renderer>().material.color;
                    draggingObject.GetComponent<Renderer>().material.color = Color.blue;
                    draggingObject.AddComponent<NodeCollision>();
                    isDragging = true;
                }
            }
            else if (draggingObject != null)
            {
                //draggingObject.GetComponent<Rigidbody>().MovePosition(CalculateMouse3DVector(draggingObject.transform.position));
                finalPosition = CalculateMouse3DVector(draggingObject.transform.position);
                //visitedNodes = null;
                draggingObject.GetComponent<Rigidbody>().MovePosition(finalPosition);


                forceGenerated = 1;
                LinkedList<GameObject> linked = new LinkedList<GameObject>();
                LinkedList<GameObject> visitedNodes = new LinkedList<GameObject>();
                visitedNodes.AddFirst(draggingObject);
                graphPairs = graphStart.getNodePairList();
                foreach (EdgePairs pairing in graphPairs)
                {
                    GameObject nodeOne = pairing.parent;
                    GameObject nodeTwo = pairing.child;
                    if (nodeOne == draggingObject)
                    {
                        linked.AddLast(nodeTwo);
                        //visitedNodes.AddFirst(nodeTwo);

                    }
                    else if (nodeTwo == draggingObject)
                    {
                        linked.AddLast(nodeOne);
                        //visitedNodes.AddLast(nodeOne);
                    }
                }

                //visitedNodes = new int[linked.Count];
                Dfs(linked, visitedNodes, draggingObject, forceGenerated);
            }
        }
        else
        {
            if (draggingObject != null)
            {
                dragObjectRigidBody = draggingObject.GetComponent<Rigidbody>();
                dragObjectRigidBody.isKinematic = false;
                draggingObject.transform.GetComponent<Renderer>().material.color = prevColor;
                Destroy(draggingObject.GetComponent<NodeCollision>());

                /*LinkedList<GameObject> linked = new LinkedList<GameObject>();
                graphPairs = graphStart.getNodePairList();
                foreach (EdgePairs pairing in graphPairs)
                {
                    GameObject nodeOne = pairing.parent;
                    GameObject nodeTwo = pairing.child;
                    if (nodeOne == draggingObject)
                    {
                        linked.AddLast(nodeTwo);
                    }
                    else if (nodeTwo == draggingObject)
                    {
                        linked.AddLast(nodeOne);
                    }
                }

                visitedNodes = new int[linked.Count];
                Dfs(linked, draggingObject, forceGenerated);*/
            }
            isDragging = false;
        }
    }

    //private GameObject GetObjectFromMouseRaycast()
    //{
    //    GameObject gmObj = null;
    //    RaycastHit hitInfo = new RaycastHit();
    //    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
    //    if (hit)
    //    {
    //        if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() &&
    //            Vector3.Distance(hitInfo.collider.gameObject.transform.position,
    //            transform.position) <= catchingDistance)
    //        {
    //            gmObj = hitInfo.collider.gameObject;
    //        }
    //    }
    //    return gmObj;
    //}

    private GameObject GetObjectFromMouseRaycast()
    {
        return gameObject;
    }
    private Vector3 CalculateMouse3DVector(Vector3 vertexPos)
    {
        
        Vector3 headRotation = GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward); //InputTracking.GetLocalRotation(VRNode.Head);
        Vector3 camPosition = GameObject.Find("Main Camera").transform.position;
        Vector3 v3 = camPosition + (headRotation) * 5f;
        return v3;
    }

    private void Dfs(LinkedList<GameObject> linked, LinkedList<GameObject> visit, GameObject nodeObject, float forceEntered)
    {

        //nodeObject.transform.Translate(forceEntered);
        if (visit.Count != 0)
        {
            foreach (GameObject siblingNodes in linked)
            {
                
                Vector3 d = nodeObject.transform.position - siblingNodes.transform.position;
                var mag = d.magnitude;
                float distance = d.magnitude + 0.1f;
                Vector3 direction = d.normalized;
                //Debug.Log("Counter: " + r);
                repulsion = 6.0f;
                //Debug.Log((direction * repulsion) / (distance * -0.5f));
                //siblingNodes.GetComponent<Rigidbody>().AddForce((direction * repulsion) / (distance * 0.5f));
                var forceM = (direction * repulsion) * forceEntered;
                if(mag <= 5f)
                {
                    siblingNodes.GetComponent<Rigidbody>().velocity = -forceM;//new Vector3(0f, 0f, 0f);

                }
                else
                {
                    siblingNodes.GetComponent<Rigidbody>().velocity = forceM;
                }

                forceEntered = forceEntered /  2;
                LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
                //LinkedList<GameObject> visitedNodes = new LinkedList<GameObject>();
                visit.AddFirst(siblingNodes);
               // visitedNodes.AddFirst(nodeTwo);
                graphPairs = graphStart.getNodePairList();
                foreach (EdgePairs pairing in graphPairs)
                {
                    GameObject nodeOne = pairing.parent;
                    GameObject nodeTwo = pairing.child;
                    if (nodeOne == siblingNodes && !visit.Contains(nodeOne))
                    {
                        linkedList.AddLast(nodeTwo);
                        visit.AddFirst(nodeTwo);

                    }
                    else if (nodeTwo == siblingNodes && !visit.Contains(nodeTwo))
                    {
                        linkedList.AddLast(nodeOne);
                        visit.AddLast(nodeOne);
                    }
                }

                Dfs(linkedList, visit, siblingNodes, forceEntered);
                

            }

        }
        else
        {
            //draggingObject.GetComponent<Rigidbody>().MovePosition(forceEntered);
            Debug.Log("No OBJ Position:");
        }

    }

    /// <summary>
    /// This is the VR Draggable updates
    /// </summary>
    private float timer;
    private float holdTimer;
    public float gazeTime = 4f;
    private bool gazedAt;
    private bool gazeDown;
    private bool mouseDown;
    
    public void onMouseOver()
    {
        gazedAt = true;
    }

    public void onMouseLeave()
    {
        gazedAt = false;
        //mouseDown = false;
        timer = 0f;
        holdTimer = 0f;
    }

    private void updateTimer()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
        }
    }

    private void checkTimeMouseDown()
    {
        if (timer >= gazeTime)
        {
            // Generate Mouse Down
            mouseDown = true;
        }
    }

    private void checkTimeStandStill()
    {
        if (mouseDown)
        {
            Vector3 nodePosition = gameObject.transform.position;
            Vector3 headRotation = GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward); //InputTracking.GetLocalRotation(VRNode.Head);
            Vector3 camPosition = GameObject.Find("Main Camera").transform.position;
            Vector3 cameraPosition = camPosition + (headRotation) * 5f;

            Vector3 diffInPos = nodePosition - cameraPosition;
            Debug.Log(diffInPos.magnitude);
            if (diffInPos.magnitude < 0.01f)
            {

                holdTimer += Time.deltaTime;
                if(holdTimer >= gazeTime)
                {
                    mouseDown = false;
                    timer = 0f;
                    holdTimer = 0f;
                }
            }
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