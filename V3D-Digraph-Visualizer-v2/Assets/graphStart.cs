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
    Camera cam;
    public GameObject nodePrefab;
    public Color nodeColour;
    public GameObject edgePrefab;
    private List<Edge> graphEdges = new List<Edge>();
    private List<EdgePairs> pairsList;
    private List<GameObject> vertexList;

    public LineRenderer line;

    /// <summary>
    ///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
    /// </summary>
    // Use this for initialization
    void Start ()
    {
        pairsList = new List<EdgePairs>();
        vertexList = new List<GameObject>();
        cam = new Camera();
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
                child.transform.parent = transform;
                vertexList.Add(child);
            }
        }

        Debug.Log(vertexList.Count);

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
            cam.transform.position = new Vector3(0, 0, -20);
        }

        //specify node color
        if(vertexList.Count > 0)
        {
            nodeColour = vertexList[0].transform.GetComponent<Renderer>().material.color;
        }
       
    }

    /// <summary>
    /// All interaction should be called in the update functin
    /// </summary>
    // Update is called once per frame
    void Update () {

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



