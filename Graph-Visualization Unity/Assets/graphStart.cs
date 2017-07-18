using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphStart : MonoBehaviour {

    Transform nodeTransform;
    
    public GameObject nodePrefab;
    public GameObject edgePrefab;
    private List<Vector3> usedPositions = new List<Vector3>();
    private List<Edge> graphEdges = new List<Edge>();

    /// <summary>
    ///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
    /// </summary>
    // Use this for initialization
    void Start () {
        


       GameObject node1 = Instantiate(nodePrefab) as GameObject;
        node1.transform.position = new Vector3(1, 1, 1);
        node1.name = "node1";
        node1.tag = "1";



        GameObject node2 = Instantiate(nodePrefab) as GameObject;
        node2.transform.position = new Vector3(4, 1, 1);
        node2.name = "node2";
        node2.tag = "2";

        //conect one and two
        //GameObject edge1 = Instantiate(nodePrefab);
        //edge1.transform.position = new Vector3(2,1,1);
        //edge1.transform.rotation = Vector3();
        
        GameObject node3 = Instantiate(nodePrefab) as GameObject;
        node3.transform.position = new Vector3(2, 4, 3);
        node3.name = "node3";
        node3.tag = "3";

        //connect the nodes 

    }

    /// <summary>
    /// Connects two edges by creating a new edge between the two nodes
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <returns></returns>

    Edge connectTwoNodes(GameObject node1, GameObject node2) {
        Edge newEdge = new Edge();
        newEdge.fromNodeTag = node1.tag;
        newEdge.toNodeTag = node2.tag;
        return newEdge;
    }


	
    /// <summary>
    /// All interaction should be called in the update functino
    /// </summary>
	// Update is called once per frame
	void Update () {
		
	}
}

public class Edge {
    GameObject edgePrefab;
    public string fromNodeTag;
    public string toNodeTag;

    public Edge() {
      edgePrefab = new GameObject(); 
      fromNodeTag = "";
    toNodeTag = "";
}

    public Edge(GameObject prefab, string fromTg, string totg) {
        edgePrefab = prefab;
        fromNodeTag = fromTg;
        toNodeTag = totg;
    }

}
