using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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

    //used to add opposing force to a moving object
    Vector3 opposingForce;
    Rigidbody movingObjectRigidBody;
    /// <summary>
    ///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
    /// </summary>
    // Use this for initialization
    void Start ()
    {
        pairsList = new List<EdgePairs>();
        vertexList = new List<GameObject>();

        /*************************************************************************Begin Test Code*******************************************************/

        //GameObject node1 = Instantiate(nodePrefab) as GameObject;
        //node1.name = "node1";
        //node1.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
        //node1.AddComponent<LineRenderer>();
        //node1.AddComponent<Dragable>();
        //node1.GetComponent<Rigidbody>().useGravity = false;
        //node1.GetComponent<Rigidbody>().drag = 3;
        //node1.transform.parent = transform;
        //vertexList.Add(node1);

        //GameObject node2 = Instantiate(nodePrefab) as GameObject;
        //node2.name = "node2";
        //node2.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
        //node2.AddComponent<LineRenderer>();
        //node2.AddComponent<Dragable>();
        //node2.GetComponent<Rigidbody>().useGravity = false;
        //node2.GetComponent<Rigidbody>().drag = 3;
        //node2.transform.parent = transform;
        //vertexList.Add(node2);

        //pairsList.Add(new EdgePairs(node1, node2, 22));

        //SpringJoint joint = node1.AddComponent<SpringJoint>();
        //joint.minDistance = Vector3.Distance(node1.transform.position, node2.transform.position);
        //joint.maxDistance = Vector3.Distance(node1.transform.position, node2.transform.position);
        //node1.GetComponent<SpringJoint>().connectedBody = node2.GetComponent<Rigidbody>();

        //LineRenderer line = node1.GetComponent<LineRenderer>();
        //line.startWidth = 0.05f;
        //line.endWidth = 0.05f;
        //line.positionCount = 2;
        //line.GetComponent<Renderer>().enabled = true;
        //line.SetPosition(0, node1.transform.position);
        //line.SetPosition(1, node2.transform.position);
        //line.material = new Material(Shader.Find("Particles/Additive"));
        //line.startColor = Color.white;
        //line.endColor = Color.white;

        /*************************************************************************End Test Code*******************************************************/

        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);

        string json = File.ReadAllText("./Assets/Graphs/graph.json");

        Edge[] tempEdgeList = JsonHelper.FromJson<Edge>(json);

        foreach (Edge edge in tempEdgeList)
        {
            if (!vertexList.Any(x => x.name == edge.parent))
            {
                GameObject parent = Instantiate(nodePrefab) as GameObject;
                parent.name = edge.parent;
                parent.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
                parent.AddComponent<LineRenderer>();
                parent.AddComponent<Dragable>();
                parent.GetComponent<Rigidbody>().useGravity = false;
                parent.GetComponent<Rigidbody>().drag = 3;
                parent.transform.parent = transform;
                vertexList.Add(parent);
            }

            if (!vertexList.Any(x => x.name == edge.child))
            {
                GameObject child = Instantiate(nodePrefab) as GameObject;
                child.name = edge.child;
                child.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
                child.AddComponent<LineRenderer>();
                child.AddComponent<Dragable>();
                child.GetComponent<Rigidbody>().useGravity = false;
                child.GetComponent<Rigidbody>().drag = 3;
                child.transform.parent = transform;
                vertexList.Add(child);
            }
        }

        foreach (Edge edge in tempEdgeList)
        {
            EdgePairs tempEdge = new EdgePairs(vertexList.Where(U => U.name == edge.parent).FirstOrDefault(), vertexList.Where(U => U.name == edge.child).FirstOrDefault(), edge.value);
            //tempEdge.parent.AddComponent<Rigidbody>();
            //tempEdge.child.AddComponent<Rigidbody>();
            //SpringJoint joint = tempEdge.parent.AddComponent<SpringJoint>();
            //joint.connectedBody = tempEdge.child.GetComponent<Rigidbody>();

            LineRenderer line = tempEdge.parent.GetComponent<LineRenderer>();
            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
            line.positionCount = 2;
            line.GetComponent<Renderer>().enabled = true;
            line.SetPosition(0, tempEdge.parent.transform.position);
            line.SetPosition(1, tempEdge.child.transform.position);
            line.material = new Material(Shader.Find("Particles/Additive"));
            line.startColor = Color.white;
            line.endColor = Color.white;
            pairsList.Add(tempEdge);
        }

        Debug.Log("Field of view: " + Camera.main.fieldOfView);

        foreach (Camera cam in Camera.allCameras)
        {
            cam.transform.position = new Vector3(0, 0, -10);
        }
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



