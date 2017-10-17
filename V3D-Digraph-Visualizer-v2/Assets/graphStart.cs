using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
//using Leap.Unity.Interaction;
using socket.io;
using Newtonsoft.Json;

namespace vdrGraph
{
    public class graphStart : MonoBehaviour
    {
		public GameObject nodePrefab;
		public static List<EdgePairs> pairsList;
		public static List<GameObject> vertexList;
		public static List<GameObject> edges;
		private LineRenderer line;
		public GameObject arrowHead;
		public static List<ArrowInfo> arrowInfoList;
		private Dictionary<string, Color> colours;
		private Dictionary<string, Vector3> centers;
		private KeyValuePair<string, Vector3> tempCenter;
		private KeyValuePair<float, float> tempClusterCenter;
		private Queue<Vector3> clusterCentersQueue;
		private KeyValuePair<float, float> defaultClusterCenter;
		public static bool createOwnGraph = false;
		private Vector3 direction;
		/// <summary>
		///Open the file here and get the list of edges and nodes then create edges from the information then setup graph
		/// </summary>
		// Use this for initialization
		void Start ()
		{
			arrowInfoList = new List<ArrowInfo> ();
			pairsList = new List<EdgePairs> ();
			vertexList = new List<GameObject> ();
			edges = new List<GameObject> ();
			colours = new Dictionary<string, Color>()
			{
				{"red", Color.red},
				{"green", Color.green},
				{"yellow", Color.yellow},
				{"black", Color.black},
				{"orange", new Color(0.2f, 0.3f, 0.4f)},
				{"brown", new Color(0.64f, 0.16f, 0.16f)},
				{"pink", new Color(0.1f, 0.42f, 0.7f)},
				{"purple", new Color(0.5f, 0, 0.5f)},	
			};

			centers = new Dictionary<string, Vector3>()
			{
				// {"red", new Vector3(-15f, 0f, Random.Range(-5f, 5f))},
				// {"green", new Vector3(-15f, 0f, Random.Range(5f, 5f))},
				// {"yellow", new Vector3(-15f, 0f, Random.Range(-5f, -5f))},
				// {"black", new Vector3(-15f, 0f, Random.Range(5f, -5f))},
				// {"orange", new Vector3(-15f, 0f, Random.Range(0f, 5f))},
				// {"brown", new Vector3(-15f, 0f, Random.Range(0f, -5f))},
				// {"pink", new Vector3(-15f, 0f, Random.Range(-5f, 0f))},
				// {"purple", new Vector3(-15f, 0f, Random.Range(5f, 0f))}	
			};

			clusterCentersQueue = new Queue<Vector3>();
			clusterCentersQueue.Enqueue(new Vector3(-5f, 5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(5f, 5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(-5f, -5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(5f, -5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(0f, 5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(0f, -5f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(-5f, 0f, Random.Range(5f, 10f)));
			clusterCentersQueue.Enqueue(new Vector3(5f, 0f, Random.Range(5f, 10f)));

			defaultClusterCenter = new KeyValuePair<float, float>(0f, 0f);

			//Screen.SetResolution(Screen.width, Screen.height, true);
			Debug.Log (Screen.width + " " + Screen.height);

			/*************************************************************************Begin Test Code*******************************************************/

			//GameObject node1 = Instantiate(nodePrefab) as GameObject;
			//node1.name = "node1";
			//node1.transform.position = new Vector3(Random.Range(-1.0f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node1.AddComponent<LineRenderer>();
			//node1.AddComponent<Dragable>();
			//node1.AddComponent<InteractionBehaviour>();
			//node1.AddComponent<SphereCollider>();
			//node1.GetComponent<Rigidbody>().useGravity = false;
			//node1.GetComponent<Rigidbody>().drag = 3;
			//node1.transform.parent = transform;
			////node1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node1);

			//GameObject node2 = Instantiate(nodePrefab) as GameObject;
			//node2.name = "node2";
			//node2.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node2.AddComponent<LineRenderer>();
			//node2.AddComponent<Dragable>();
			//node2.AddComponent<InteractionBehaviour>();
			//node2.AddComponent<SphereCollider>();
			//node2.GetComponent<Rigidbody>().useGravity = false;
			//node2.GetComponent<Rigidbody>().drag = 3;
			//node2.transform.parent = transform;
			//// node2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node2);

			//GameObject node3 = Instantiate(nodePrefab) as GameObject;
			//node3.name = "node3";
			//node3.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node3.AddComponent<LineRenderer>();
			//node3.AddComponent<Dragable>();
			//node3.AddComponent<InteractionBehaviour>();
			//node3.AddComponent<SphereCollider>();
			//node3.GetComponent<Rigidbody>().useGravity = false;
			//node3.GetComponent<Rigidbody>().drag = 3;
			//node3.transform.parent = transform;
			////node3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node3);

			//GameObject node4 = Instantiate(nodePrefab) as GameObject;
			//node4.name = "node4";
			//node4.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node4.AddComponent<LineRenderer>();
			//node4.AddComponent<Dragable>();
			//node4.AddComponent<SphereCollider>();
			//node4.AddComponent<InteractionBehaviour>();
			//node4.GetComponent<Rigidbody>().useGravity = false;
			//node4.GetComponent<Rigidbody>().drag = 3;
			//node4.transform.parent = transform;
			////node4.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node4);

			//GameObject node5 = Instantiate(nodePrefab) as GameObject;
			//node5.name = "node5";
			//node5.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node5.AddComponent<LineRenderer>();
			//node5.AddComponent<Dragable>();
			//node5.AddComponent<SphereCollider>();
			//node5.AddComponent<InteractionBehaviour>();
			//node5.GetComponent<Rigidbody>().useGravity = false;
			//node5.GetComponent<Rigidbody>().drag = 3;
			//node5.transform.parent = transform;
			////node5.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node5);

			//GameObject node6 = Instantiate(nodePrefab) as GameObject;
			//node6.name = "node6";
			//node6.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node6.AddComponent<LineRenderer>();
			//node6.AddComponent<Dragable>();
			//node6.AddComponent<SphereCollider>();
			//node6.AddComponent<InteractionBehaviour>();
			//node6.GetComponent<Rigidbody>().useGravity = false;
			//node6.GetComponent<Rigidbody>().drag = 3;
			//node6.transform.parent = transform;
			////node6.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node6);

			//GameObject node7 = Instantiate(nodePrefab) as GameObject;
			//node7.name = "node7";
			//node7.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			////node7.AddComponent<LineRenderer>();
			//node7.AddComponent<Dragable>();
			//node7.AddComponent<SphereCollider>();
			//node7.AddComponent<InteractionBehaviour>();
			//node7.GetComponent<Rigidbody>().useGravity = false;
			//node7.GetComponent<Rigidbody>().drag = 3;
			//node7.transform.parent = transform;
			////node7.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			//vertexList.Add(node7);

			//pairsList.Add(new EdgePairs(node1, node2, 22));
			//pairsList.Add(new EdgePairs(node2, node3, 22));
			//pairsList.Add(new EdgePairs(node2, node4, 22));
			//pairsList.Add(new EdgePairs(node2, node5, 22));
			//pairsList.Add(new EdgePairs(node3, node5, 22));
			//pairsList.Add(new EdgePairs(node4, node5, 22));
			//pairsList.Add(new EdgePairs(node5, node6, 22));
			//pairsList.Add(new EdgePairs(node7, node4, 22));

			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());
			//edges.Add(new GameObject());

			//for (int i = 0; i < edges.Count(); i++)
			//{
			//    edges[i].AddComponent<LineRenderer>();
			//    line = edges[i].GetComponent<LineRenderer>();
			//    line.startWidth = 0.1f;
			//    line.endWidth = 0.1f;
			//    line.positionCount = 2;
			//    line.GetComponent<Renderer>().enabled = true;
			//    line.SetPosition(0, pairsList[i].parent.transform.position);
			//    line.SetPosition(1, pairsList[i].child.transform.position);
			//    line.material = new Material(Shader.Find("Sprites/Default"));
			//    line.startColor = Color.white;
			//    line.endColor = Color.white;
			//}

			//0.7 to get to edge of node
			//rotate arrowhead to face child
	//		GameObject arrowhead = Instantiate (arrowHead) as GameObject;
	//
	//		if (arrowHead.GetComponent<LineRenderer> () == null) 
	//		{
	//			arrowHead.AddComponent<LineRenderer>();
	//		}
	//
	//		Vector3 dir = (pairsList [i].child.transform.position - pairsList [i].parent.transform.position).normalized;
	//		arrowHead.transform.rotation = Quaternion.LookRotation (dir);
	//
	//		//move arrowhead to edge of node
	//		Vector3 displace = new Vector3 (dir.x * 0.6f, dir.y * 0.6f, dir.z * 0.6f);
	//		arrowHead.transform.position = pairsList[i].child.transform.position - displace;
	//
	//		arrowHead.name = "Arrow" + i;
	//
	//		ArrowInfo infoObj = new ArrowInfo (arrowHead.name, pairsList[i].parent.name, pairsList[i].child.name);
	//		arrowInfoList.Add (infoObj);
	//
	//		Debug.Log (arrowHead.name);
			

			//Debug.Log(edges.Count() + "Edges");

			/*************************************************************************End Test Code*******************************************************/

			//socket = Socket.Connect("http://192.168.56.1:8000");
			//socket = Socket.Connect("http://localhost:3000");

			//socket.On(SystemEvents.connect, () =>
			//{
			//    socket.Emit("status", "Graph Viz connected...");
			//    socket.Emit("getGraphData", "graph1");
			//});

			////receive graph data
			//socket.On("receiveGraphData", (string data) =>
			//{

			//string path = "/storage/emulated/0/Graphs/graph.txt";
			//Edge[] tempEdgeList = JsonHelper.FromJson<Edge> (File.ReadAllText (path));

			//string json = GameObject.Find("InformationObject").GetComponent<InformationScript>().jsonToSend;
			string json = "";

			if(json != "")
			{
				List<Edge>items = JsonConvert.DeserializeObject<List<Edge>>(json);

				foreach (Edge edge in items) 
				{
					if (!vertexList.Any (x => x.name == edge.parent.name)) 
					{
						GameObject parent = Instantiate (nodePrefab) as GameObject;
						parent.name = edge.parent.name;
						//parent.transform.position = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f));
						//parent.AddComponent<InteractionBehaviour>();
						parent.GetComponent<Rigidbody> ().useGravity = false;
						parent.GetComponent<Rigidbody> ().drag = 3;
						parent.transform.parent = transform;
						parent.GetComponent<Rigidbody>().freezeRotation = true;
						parent.GetComponent<Renderer>().material.color = colours[edge.parent.colour.ToLower()];
						if(!centers.ContainsKey(edge.parent.colour.ToLower()))
						{
							centers.Add(edge.parent.colour.ToLower(), clusterCentersQueue.Dequeue());
						}
						parent.transform.position = centers.Where(x => x.Key == edge.parent.colour.ToLower()).FirstOrDefault().Value + Random.onUnitSphere * 2f;						
						parent.transform.GetChild(0).GetComponent<TextMesh>().text = parent.name;
						vertexList.Add(parent);
					}

					if (!vertexList.Any (x => x.name == edge.child.name)) 
					{
						GameObject child = Instantiate (nodePrefab) as GameObject;
						child.name = edge.child.name;
						//child.transform.position = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f));
						//child.AddComponent<InteractionBehaviour>();
						child.GetComponent<Rigidbody> ().useGravity = false;
						child.GetComponent<Rigidbody> ().drag = 3;
						child.transform.parent = transform;
						child.GetComponent<Rigidbody>().freezeRotation = true;
						child.GetComponent<Renderer>().material.color = colours[edge.child.colour.ToLower()];
						if(!centers.ContainsKey(edge.parent.colour.ToLower()))
						{
							centers.Add(edge.child.colour.ToLower(), clusterCentersQueue.Dequeue());
						}
						child.transform.position = centers.Where(x => x.Key == edge.child.colour.ToLower()).FirstOrDefault().Value + Random.onUnitSphere * 2f;
						child.transform.GetChild(0).GetComponent<TextMesh>().text = child.name;						
						vertexList.Add(child);
					}
				}

				for (int i = 0; i < items.Count (); i++)
				{
					//render edge line
					EdgePairs tempEdge = new EdgePairs (vertexList.Where(U => U.name == items[i].parent.name).FirstOrDefault (), vertexList.Where (U => U.name == items [i].child.name).FirstOrDefault (), items[i].relationship);
					pairsList.Add (tempEdge);
					edges.Add (new GameObject ());
					edges [i].AddComponent<LineRenderer> ();
					line = edges [i].GetComponent<LineRenderer> ();
					line.startWidth = 0.1f;
					line.endWidth = 0.1f;
					line.positionCount = 2;
					line.GetComponent<Renderer> ().enabled = true;
					line.SetPosition (0, pairsList [i].parent.transform.position);
					line.SetPosition (1, pairsList [i].child.transform.position);
					line.material = new Material (Shader.Find ("Sprites/Default"));
					line.startColor = Color.white;
					line.endColor = Color.white;

					//instantiate arrow heads
					GameObject arrow = Instantiate (arrowHead) as GameObject;
					//get node rotation
					direction = (pairsList [i].child.transform.position - pairsList [i].parent.transform.position).normalized;
					arrow.transform.rotation = Quaternion.LookRotation (direction);
					//move arrowhead to edge of node
					Vector3 displace = new Vector3 (direction.x * 0.6f, direction.y * 0.6f, direction.z * 0.6f);
					arrow.transform.position = pairsList[i].child.transform.position - displace;
					arrow.name = "Arrow" + i;
					arrowInfoList.Add (new ArrowInfo (arrow, pairsList[i].parent.name, pairsList[i].child.name));
					Debug.Log (arrow.name);
				}
			}
			else
			{
				//create own graph
				createOwnGraph = true;
			}

			Debug.Log("Field of view: " + Camera.main.fieldOfView);
			GameObject.FindWithTag("VRMain").transform.position = new Vector3(0, 0, -10);
			Vector3 pos = GameObject.Find("Graph").transform.position;
			GameObject.Find("Graph").transform.position = new Vector3(pos.x, pos.y, (pos.z + 10f));
		}

		/// <summary>
		/// All interaction should be called in the update functin
		/// </summary>
		// Update is called once per frame
		void Update () 
		{
			Debug.Log("Number of lines: " + edges.Count());

			//redraw the edge
			for(int i = 0; i < edges.Count(); i++)
			{
				//update edge lines
				line = edges[i].GetComponent<LineRenderer>();
				line.SetPosition(0, pairsList[i].parent.transform.position);
				line.SetPosition(1, pairsList[i].child.transform.position);
				//Position arrows
				//rotate arrowhead to face child
				Vector3 dir = (pairsList[i].child.transform.position - pairsList[i].parent.transform.position).normalized;
				arrowInfoList[i].myArrow.transform.rotation = Quaternion.LookRotation(dir);
				//move arrowhead to edge of node
				Vector3 displace = new Vector3(dir.x * 0.6f, dir.y * 0.6f, dir.z * 0.6f);
				arrowInfoList[i].myArrow.transform.position = pairsList[i].child.transform.position - displace;

				//this is for the hand gesture mode;
				//pair.parent.transform.position = new Vector3(pair.parent.transform.position.x, pair.parent.transform.position.y, -8.5f);   
				//pair.child.transform.position = new Vector3(pair.child.transform.position.x, pair.child.transform.position.y, -8.5f);
			}

			if (Input.GetKey("left"))
				transform.RotateAround(transform.position, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * 1f);
			if (Input.GetKey("right"))
				transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * 1f);
			if (Input.GetKey("up"))
				transform.RotateAround(transform.position, new Vector3(1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * 1f);
			if (Input.GetKey("down"))
				transform.RotateAround(transform.position, new Vector3(-1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * 1f);

			if (Input.GetKey("[+]"))
			{
				//Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * 10);
				transform.Translate(Vector3.forward * Time.deltaTime);
			}
			if(Input.GetKey("[-]"))
			{
				transform.Translate(Vector3.back * Time.deltaTime);
			}
		}
		
        public static List<EdgePairs> getNodePairList()
        {
            return pairsList;
        }

<<<<<<< HEAD
		public static List<GameObject> getVertexList()
		{
			return vertexList;
		}

		public static List<GameObject> getEdges()
		{
			return edges;
		}

		public static bool getCreateOwnGraph()
		{
			return createOwnGraph;
		}

		public static List<ArrowInfo> getArrowList()
		{
			return arrowInfoList;
		}

		private void saveGraph()
		{
			//send json back to file selector
		}
	}
}
	
=======
        string json = JsonHelper.ToJson<Edge>(edges.ToArray<Edge>());
        File.WriteAllText("./Assets/Graphs/graph.json", json);
    }
>>>>>>> 20d11c18c52b8d4f5ebe1c5649b400f6e044e112

    public static List<EdgePairs> getNodePairList()
    {
        
        return pairsList;
    }
}

}

