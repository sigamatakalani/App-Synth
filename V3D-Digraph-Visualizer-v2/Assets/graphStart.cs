using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Leap.Unity.Interaction;

public class graphStart : MonoBehaviour {

    //Material used for connecting lines
    public Material lineMat;
    public float radius = 0.05f;

    //default cylinder mesh
    public Mesh cylinderMesh;
    public GameObject graphObject;
    public GameObject nodePrefab;
    public Color nodeColour;
    public GameObject edgePrefab;
    private List<Edge> graphEdges = new List<Edge>();
    private List<EdgePairs> pairsList;
    private List<GameObject> vertexList;
    public LineRenderer line;
    //public GameObject rotateUp;
    //public GameObject rotateDown;
    //public GameObject rotateLeft;
    //public GameObject rotateRight; 

    //used to add opposing force to a moving object
    Vector3 opposingForce;
    Rigidbody movingObjectRigidBody;

    public Texture2D rotateLeft;
    public Texture2D rotateRight;
    public Texture2D rotateUp;
    public Texture2D rotateDown;
    public Texture2D zoomIn;
    public Texture2D zoomOut;

    private void OnGUI()
    {
        ////rotate up
        //if (GUI.RepeatButton(new Rect(Screen.width / 2 - 25, 0, 50, 50), rotateUp))
        //{
        //    this.transform.Rotate(1f, 0f, 0f);
        //}

        ////rotate right
        //if (GUI.RepeatButton(new Rect(Screen.width - 50, Screen.height / 2 - 25, 50, 50), rotateRight))
        //{
        //    this.transform.Rotate(0f, 1f, 0f);
        //}

        ////rotate left
        //if (GUI.RepeatButton(new Rect(0, Screen.height / 2 - 25, 50, 50), rotateLeft))
        //{
        //    this.transform.Rotate(0f, -1f, 0f);
        //}

        ////rotate down
        //if (GUI.RepeatButton(new Rect(Screen.width / 2 - 25, Screen.height - 50, 50, 50), rotateDown))
        //{
        //    this.transform.Rotate(-1f, 0f, 0f);
        //}

        ////zoom in
        //if (GUI.RepeatButton(new Rect(Screen.width / 2 - 100, Screen.height - 100, 50, 50), zoomIn))
        //{
        //    Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * 10);
        //}

        //if (GUI.RepeatButton(new Rect(Screen.width / 2 + 50, Screen.height - 100, 50, 50), zoomOut))
        //{
        //    Camera.main.transform.Translate(Vector3.back * Time.deltaTime * 10);
        //}
    }
    /// <summary>
    ///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
    /// </summary>
    // Use this for initialization
    void Start ()
    {
        pairsList = new List<EdgePairs>();
        vertexList = new List<GameObject>();
        Screen.SetResolution(Screen.width, Screen.height, true);
        //Vector3 pos = GameObject.FindWithTag("RotateRight").transform.position;
        //GameObject.FindWithTag("RotateRight").transform.position = new Vector3(pos.x + 10, pos.y + 10, pos.z);

        Debug.Log(Screen.width + " " + Screen.height);

        /*************************************************************************Begin Test Code*******************************************************/

        GameObject node1 = Instantiate(nodePrefab) as GameObject;
        node1.name = "node1";
        node1.transform.position = new Vector3(Random.Range(-1.0f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node1.AddComponent<LineRenderer>();
        node1.AddComponent<Dragable>();
        node1.AddComponent<InteractionBehaviour>();
        node1.AddComponent<SphereCollider>();
        node1.GetComponent<Rigidbody>().useGravity = false;
        node1.GetComponent<Rigidbody>().drag = 3;
        node1.transform.parent = transform;
        node1.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node1);

        GameObject node2 = Instantiate(nodePrefab) as GameObject;
        node2.name = "node2";
        node2.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node2.AddComponent<LineRenderer>();
        node2.AddComponent<Dragable>();
        node2.AddComponent<InteractionBehaviour>();
        node2.AddComponent<SphereCollider>();
        node2.GetComponent<Rigidbody>().useGravity = false;
        node2.GetComponent<Rigidbody>().drag = 3;
        node2.transform.parent = transform;
        node2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node2);

        GameObject node3 = Instantiate(nodePrefab) as GameObject;
        node3.name = "node3";
        node3.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node3.AddComponent<LineRenderer>();
        node3.AddComponent<Dragable>();
        node3.AddComponent<InteractionBehaviour>();
        node3.AddComponent<SphereCollider>();
        node3.GetComponent<Rigidbody>().useGravity = false;
        node3.GetComponent<Rigidbody>().drag = 3;
        node3.transform.parent = transform;
        node3.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node3);

        GameObject node4 = Instantiate(nodePrefab) as GameObject;
        node4.name = "node4";
        node4.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node4.AddComponent<LineRenderer>();
        node4.AddComponent<Dragable>();
        node4.AddComponent<SphereCollider>();
        node4.AddComponent<InteractionBehaviour>();
        node4.GetComponent<Rigidbody>().useGravity = false;
        node4.GetComponent<Rigidbody>().drag = 3;
        node4.transform.parent = transform;
        node4.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node4);

        GameObject node5 = Instantiate(nodePrefab) as GameObject;
        node5.name = "node5";
        node5.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node5.AddComponent<LineRenderer>();
        node5.AddComponent<Dragable>();
        node5.AddComponent<SphereCollider>();
        node5.AddComponent<InteractionBehaviour>();
        node5.GetComponent<Rigidbody>().useGravity = false;
        node5.GetComponent<Rigidbody>().drag = 3;
        node5.transform.parent = transform;
        node5.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node5);

        GameObject node6 = Instantiate(nodePrefab) as GameObject;
        node6.name = "node6";
        node6.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node6.AddComponent<LineRenderer>();
        node6.AddComponent<Dragable>();
        node6.AddComponent<SphereCollider>();
        node6.AddComponent<InteractionBehaviour>();
        node6.GetComponent<Rigidbody>().useGravity = false;
        node6.GetComponent<Rigidbody>().drag = 3;
        node6.transform.parent = transform;
        node6.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node6);

        GameObject node7 = Instantiate(nodePrefab) as GameObject;
        node7.name = "node7";
        node7.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -8.5f);
        node7.AddComponent<LineRenderer>();
        node7.AddComponent<Dragable>();
        node7.AddComponent<SphereCollider>();
        node7.AddComponent<InteractionBehaviour>();
        node7.GetComponent<Rigidbody>().useGravity = false;
        node7.GetComponent<Rigidbody>().drag = 3;
        node7.transform.parent = transform;
        node7.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        vertexList.Add(node7);

        pairsList.Add(new EdgePairs(node1, node2, 22));
        pairsList.Add(new EdgePairs(node2, node3, 22));
        pairsList.Add(new EdgePairs(node2, node4, 22));
        pairsList.Add(new EdgePairs(node2, node5, 22));
        pairsList.Add(new EdgePairs(node3, node5, 22));
        pairsList.Add(new EdgePairs(node4, node5, 22));
        pairsList.Add(new EdgePairs(node5, node6, 22));
        pairsList.Add(new EdgePairs(node7, node4, 22));

        //SpringJoint joint = node1.AddComponent<SpringJoint>();
        //joint.minDistance = Vector3.Distance(node1.transform.position, node2.transform.position);
        //joint.maxDistance = Vector3.Distance(node1.transform.position, node2.transform.position);
        //node1.GetComponent<SpringJoint>().connectedBody = node2.GetComponent<Rigidbody>();

        foreach (EdgePairs edge in pairsList)
        {
            line = edge.parent.GetComponent<LineRenderer>();
            line.startWidth = 0.02f;
            line.endWidth = 0.02f;
            line.positionCount = 2;
            line.GetComponent<Renderer>().enabled = true;
            line.SetPosition(0, edge.parent.transform.position);
            line.SetPosition(1, edge.child.transform.position);
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.white;
            line.endColor = Color.white;
        }

        /*************************************************************************End Test Code*******************************************************/

        //string json = File.ReadAllText("./Assets/Graphs/graph.json");

        ////string json = GameObject.Find("InformationObject").GetComponent<InformationScript>().jsonToSend;
        //Debug.Log(json);

        //Edge[] tempEdgeList = JsonHelper.FromJson<Edge>(json);

        //foreach (Edge edge in tempEdgeList)
        //{
        //    if (!vertexList.Any(x => x.name == edge.parent))
        //    {
        //        GameObject parent = Instantiate(nodePrefab) as GameObject;
        //        parent.name = edge.parent;
        //        parent.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
        //        parent.AddComponent<LineRenderer>();
        //        parent.AddComponent<Dragable>();
        //        parent.GetComponent<Rigidbody>().useGravity = false;
        //        parent.GetComponent<Rigidbody>().drag = 3;
        //        parent.transform.parent = transform;
        //        vertexList.Add(parent);
        //    }

        //    if (!vertexList.Any(x => x.name == edge.child))
        //    {
        //        GameObject child = Instantiate(nodePrefab) as GameObject;
        //        child.name = edge.child;
        //        child.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
        //        child.AddComponent<LineRenderer>();
        //        child.AddComponent<Dragable>();
        //        child.GetComponent<Rigidbody>().useGravity = false;
        //        child.GetComponent<Rigidbody>().drag = 3;
        //        child.transform.parent = transform;
        //        vertexList.Add(child);
        //    }
        //}

        //foreach (Edge edge in tempEdgeList)
        //{
        //    EdgePairs tempEdge = new EdgePairs(vertexList.Where(U => U.name == edge.parent).FirstOrDefault(), vertexList.Where(U => U.name == edge.child).FirstOrDefault(), edge.value);
        //    //tempEdge.parent.AddComponent<Rigidbody>();
        //    //tempEdge.child.AddComponent<Rigidbody>();
        //    //SpringJoint joint = tempEdge.parent.AddComponent<SpringJoint>();
        //    //joint.connectedBody = tempEdge.child.GetComponent<Rigidbody>();

        //    LineRenderer line = tempEdge.parent.GetComponent<LineRenderer>();
        //    line.startWidth = 0.05f;
        //    line.endWidth = 0.05f;
        //    line.positionCount = 2;
        //    line.GetComponent<Renderer>().enabled = true;
        //    line.SetPosition(0, tempEdge.parent.transform.position);
        //    line.SetPosition(1, tempEdge.child.transform.position);
        //    line.material = new Material(Shader.Find("Particles/Additive"));
        //    line.startColor = Color.white;
        //    line.endColor = Color.white;
        //    pairsList.Add(tempEdge);
        //}

        Debug.Log("Field of view: " + Camera.main.fieldOfView);

        //foreach (Camera cam in Camera.allCameras)
        //{
        //    cam.transform.position = new Vector3(0, 0, -10);
        //}
        GameObject.FindWithTag("VRMain").transform.position = new Vector3(0, 0, -10);
    }

    /// <summary>
    /// All interaction should be called in the update functin
    /// </summary>
    // Update is called once per frame
    void Update () {
        
        //redraw the edge
        foreach(EdgePairs pair in pairsList)
        {
            line = pair.parent.GetComponent<LineRenderer>();
            line.SetPosition(0, pair.parent.transform.position);
            line.SetPosition(1, pair.child.transform.position);
            pair.parent.transform.position = new Vector3(pair.parent.transform.position.x, pair.parent.transform.position.y, -8.5f);   
            pair.child.transform.position = new Vector3(pair.child.transform.position.x, pair.child.transform.position.y, -8.5f);
        }

        if (Input.GetKey("left"))
            this.transform.Rotate(0f, -1f, 0f);
        if (Input.GetKey("right"))
            this.transform.Rotate(0f, 1f, 0f);
        if (Input.GetKey("up"))
            this.transform.Rotate(1f, 0f, 0f);
        if (Input.GetKey("down"))
            this.transform.Rotate(-1f, 0f, 0f);

        if (Input.GetKey("[+]"))
        {
            Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * 10);
        }
        if(Input.GetKey("[-]"))
        {
            Camera.main.transform.Translate(Vector3.back * Time.deltaTime * 10);
        }

        //graphObject.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
    }

    //save graph back to graph spec
    void saveGraph()
    {
        List<Edge> edges = new List<Edge>();

        foreach (EdgePairs pair in pairsList)
        {
            Edge edge = new Edge();
            edge.parent = pair.parent.name;
            edge.child = pair.child.name;
            edge.value = pair.value;
            edges.Add(edge);
        }

        string json = JsonHelper.ToJson<Edge>(edges.ToArray<Edge>());
        File.WriteAllText("./Assets/Graphs/graph.json", json);
    }
}



