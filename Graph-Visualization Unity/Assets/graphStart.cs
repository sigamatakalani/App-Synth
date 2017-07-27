using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class graphStart : MonoBehaviour {

    //Material used for connecting lines
    public Material lineMat;
    public float radius = 0.05f;

    //default cylinder mesh
    public Mesh cylinderMesh;

    public GameObject[] points;

    Transform nodeTransform;
    
    public GameObject nodePrefab;
    public GameObject edgePrefab;
    private List<Vector3> usedPositions = new List<Vector3>();
    private List<Edge> graphEdges = new List<Edge>();
    private List<EdgePairs> pairsList;
    private List<GameObject> vertexList;

    /// <summary>
    ///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
    /// </summary>
    // Use this for initialization
    void Start ()
    {
        pairsList = new List<EdgePairs>();
        vertexList = new List<GameObject>();

        string json = File.ReadAllText("Assets/Graphs/graph.json");

        Edge[] tempEdgeList = JsonHelper.FromJson<Edge>(json);

        foreach (Edge edge in tempEdgeList)
        {
            
            GameObject parent = Instantiate(nodePrefab) as GameObject;
            parent.name = edge.parent;
            parent.transform.position = Vector3();

            GameObject child = Instantiate(nodePrefab) as GameObject;
            child.name = edge.child;

            //Add new edge to graph
            EdgePairs tempEdge = new EdgePairs(parent, child, edge.value);
            pairsList.Add(tempEdge);

            //add edge connections;
            connectTwoNodesDraw(parent, child);

            bool insertParent = true, insertChild = true;

            //add to vertex list if not exists
            foreach (GameObject vertex in vertexList)
            {
                if(vertex.name == edge.parent)
                {
                    insertParent = false;
                }

                if(vertex.name == edge.child)
                {
                    insertChild = false;
                }
            }

            if(insertParent)
            {
                vertexList.Add(parent);
            }

            if(insertChild)
            {
                vertexList.Add(child);
            }
        }

        //GameObject node1 = Instantiate(nodePrefab) as GameObject;
        //node1.transform.position = new Vector3(1, 1, 1);
        //node1.name = "node1";
        ////node1.tag = "1";

        //GameObject node2 = Instantiate(nodePrefab) as GameObject;
        //node2.transform.position = new Vector3(4, 1, 1);
        //node2.name = "node2";
        //// node2.tag = "2";

        //GameObject node3 = Instantiate(nodePrefab) as GameObject;
        //node3.transform.position = new Vector3(2, 4, 3);
        //node3.name = "node3";


        //GameObject node4 = Instantiate(nodePrefab) as GameObject;
        //node4.transform.position = new Vector3(7, 6, 3);
        //node4.name = "node4";

        //GameObject node5 = Instantiate(nodePrefab) as GameObject;
        //node5.transform.position = new Vector3(-7, 1, 2);
        //node5.name = "node5";

        //connectTwoNodesDraw(node1,node2);
        //EdgePairs tmp = new  EdgePairs(node1, node2, 10);
        //pairsList.Add(tmp);
        
        //connectTwoNodesDraw(node2, node3);
        //EdgePairs tmp2 = new EdgePairs(node2, node3, 20);
        //pairsList.Add(tmp2);

        //connectTwoNodesDraw(node3, node1);
        //EdgePairs tmp3 = new EdgePairs(node3, node1, 30);
        //pairsList.Add(tmp3);


        //connectTwoNodesDraw(node3, node4);
        //EdgePairs tmp4 = new EdgePairs(node3, node4, 40);
        //pairsList.Add(tmp4);

        //connectTwoNodesDraw(node3, node5);
        //EdgePairs tmp5 = new EdgePairs(node3, node5, 50);
        //pairsList.Add(tmp5);
        
        ////hello

        //connectTwoNodesDraw(node5, node2);
        //EdgePairs tmp6 = new EdgePairs(node5, node2, 60);
        //pairsList.Add(tmp6);
    }

    /// <summary>
    /// Connects two nodes by creating a new edge between the two nodes
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <returns></returns>

    //Edge connectTwoNodes(GameObject node1, GameObject node2) {
    //    Edge newEdge = new Edge();
    //    newEdge.fromNodeTag = node1.tag;
    //    newEdge.toNodeTag = node2.tag;

    //    return newEdge;
    //}

    void connectTwoNodesDraw(GameObject node1, GameObject node2)
    {
        float cylinderDistance = 0.5f * Vector3.Distance(node1.transform.position, node2.transform.position);
        Debug.Log(" here " + cylinderDistance);
         node2.transform.parent = this.gameObject.transform;

        // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
        GameObject ringOffsetCylinderMeshObject = new GameObject();
        ringOffsetCylinderMeshObject.transform.parent = node2.transform;

        // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.

        ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, cylinderDistance, 0f);

        // Set the radius
        ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, cylinderDistance, radius);
        // Create the the Mesh and renderer to show the connecting ring
        MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();

        //ringMesh.mesh = this.cylinderMesh;
        Mesh tempMesh = Instantiate(cylinderMesh);
        ringMesh.mesh = tempMesh;

        MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
        ringRenderer.material = lineMat;
        
    }

    /// <summary>
    /// All interaction should be called in the update functino
    /// </summary>
    // Update is called once per frame
    void Update () {

        int count = 0;
        foreach (EdgePairs pair in this.pairsList) {
            // Move the ring to the point
          //  pair.child.transform.position = this.points[count].transform.position;

            // Match the scale to the distance
           // float cylinderDistance = 0.5f * Vector3.Distance(pair.child.transform.position, pair.parent.transform.position);
           // pair.child.transform.localScale = new Vector3(pair.child.transform.localScale.x, cylinderDistance, pair.child.transform.localScale.z);

            // Make the cylinder look at the main point.
            // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
            pair.child.transform.LookAt(pair.parent.transform, Vector3.up);
            pair.child.transform.rotation *= Quaternion.Euler(90, 0, 0);
            count++;
        }

    }

    public int nodeCounter = 0;
    public Vector3 PositionAlgorithm()
    {
        Vector3 nodePosi = new Vector3 (0, 0, 0);
        nodeCounter++;
        int x = 0;
        int y = 0;
        int z = 0;
        if(nodeCounter < 10)
        {

        }
        return nodePosi;
    }
}

[System.Serializable]
public class Edge
{
    public string parent;
    public int value;
    public string child;

    public Edge() {
        parent = "";
        value = 0;
        child = "";
    }
}

public class EdgePairs
{
    public GameObject parent;
    public GameObject child;
    public int value;

    public EdgePairs()
    {
        parent = new GameObject();
        child = new GameObject();
    }

    public EdgePairs(GameObject parent1, GameObject child1, int value1)
    {

        parent = parent1;
        child = child1;
        value = value1;
    }
}

//json helper class to create list of edges
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}



