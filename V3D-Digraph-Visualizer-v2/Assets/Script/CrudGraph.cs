using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vdrGraph;
using System.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;

namespace CrudGraphNamespace
{
	public class CrudGraph : MonoBehaviour 
	{
		public GameObject nodePrefab;
		public GameObject arrowHead;
		public GameObject createNodeButton;
		public GameObject removeNodeButton;
		public static GameObject editModeToggle;
		private Vector3 center = new Vector3(0f,0f,7.5f);
		private float timer;
		private float holdTimer;
		private float gazeTime = 1f;
		private bool gazedAtCreateNode;
		private bool gazedAtDeleteNode;
		private bool gazedAtToggleEditMode;
		private bool gazedAtSaveGraph;
		private bool gazeDown;
		private static LineRenderer line;
		private static Vector3 direction;
		private static EdgePairs tempEdge;
		private static GameObject tempNode;
		private static GameObject tempLine;
		private static ArrowInfo tempArrow;
		private static List<EdgePairs> tempEdgeList;
		private static List<Edge> edgeListToSerialize;
		public static bool deleteMode = false;

		// Use this for initialization
		void Start ()
		{
			editModeToggle = GameObject.Find("Toggle");
		}
		
		// Update is called once per frame
		void Update () 
		{
			updateTimer();
			checkTimeMouseDown();
		}

		public void createNode()
		{
			GameObject node = Instantiate(nodePrefab) as GameObject;
			node.name = "Node" + (graphStart.getVertexList().Count + 1);
			node.transform.GetChild(0).GetComponent<TextMesh>().text = node.name; //add name to label
			node.GetComponent<Rigidbody>().useGravity = false;
			node.GetComponent<Rigidbody> ().drag = 3;
			node.transform.parent = GameObject.Find("Graph").transform;
			node.transform.position = center + Random.onUnitSphere * 2f;
			graphStart.getVertexList().Add(node);
		}

		public static void createEdge(GameObject node1, GameObject node2)
		{
			if(editModeToggle.GetComponent<Toggle>().isOn)
			{
				Node parent;
				Node child;
				Relationship relation;

				//add edge to pairslist
				tempEdge = graphStart.getNodePairList().Where(x => x.parent.name == node1.name && x.child.name == node2.name).FirstOrDefault();
				if(tempEdge == null)
				{
					tempEdge = new EdgePairs(node1, node2, new Relationship());
					graphStart.getNodePairList().Add(tempEdge);
					//add line renderer
					graphStart.getEdges().Add(new GameObject());
					graphStart.getEdges().Last().AddComponent<LineRenderer>();
					line = graphStart.getEdges().Last().GetComponent<LineRenderer>();
					line.startWidth = 0.1f;
					line.endWidth = 0.1f;
					line.positionCount = 2;

					line.GetComponent<Renderer> ().enabled = true;
					line.SetPosition (0, node1.transform.position);
					line.SetPosition (1, node2.transform.position);
					line.material = new Material (Shader.Find ("Sprites/Default"));
					line.startColor = Color.white;
					line.endColor = Color.white;

					//add arrows
					GameObject arrow = Instantiate(GameObject.Find("Graph").GetComponent<graphStart>().arrowHead) as GameObject;
					//get node rotation
					direction = (node1.transform.position - node2.transform.position).normalized;
					arrow.transform.rotation = Quaternion.LookRotation (direction);
					//move arrowhead to edge of node
					Vector3 displace = new Vector3 (direction.x * 0.6f, direction.y * 0.6f, direction.z * 0.6f);
					arrow.transform.position = node2.transform.position - displace;
					arrow.name = "Arrow" + graphStart.getArrowList().Count();
					graphStart.getArrowList().Add (new ArrowInfo (arrow, node1.name, node2.name));
					//add to serializable edges
					parent = new Node(node1.name, "", "red", new List<Dictionary<string, string>>());
					child = new Node(node2.name, "", "red", new List<Dictionary<string, string>>());
					relation = new Relationship();
					graphStart.getSerializableEdges().Add(new Edge(parent, child, relation));
				}
			}
		}

		public void deleteNode()
		{
			tempNode = graphStart.getVertexList().Where(x => x.GetComponent<Renderer>().material.color == Color.blue).FirstOrDefault();
			if(tempNode != null)
			{
				tempEdgeList = graphStart.getNodePairList().Where(x => x.parent.name == tempNode.name || x.child.name == tempNode.name).ToList();
				if(tempEdgeList != null)
				{
					foreach(EdgePairs edgePair in tempEdgeList)
					{
						tempLine = graphStart.getEdges().Where(x => x.GetComponent<LineRenderer>().GetPosition(0) == edgePair.parent.transform.position && x.GetComponent<LineRenderer>().GetPosition(1) == edgePair.child.transform.position).FirstOrDefault();
						if(tempLine != null)
						{
							//delete line
							graphStart.getEdges().Remove(tempLine);
							Destroy(tempLine);
						}

						tempArrow = graphStart.getArrowList().Where(x => x.myParentNode == edgePair.parent.name && x.myChildNode == edgePair.child.name).FirstOrDefault();
						if(tempArrow != null)
						{
							//delete arrow
							graphStart.getArrowList().Remove(tempArrow);
							Destroy(tempArrow.myArrow);
						}
						//delete edge
						graphStart.getNodePairList().Remove(edgePair);
					}
				}
				//delete node
				graphStart.getVertexList().Remove(tempNode);
				Destroy(tempNode);
			}
		}

		public void toggleEditMode()
		{
			print("enter toggle function...");
			if(editModeToggle.GetComponent<Toggle>().isOn)
			{
				editModeToggle.GetComponent<Toggle>().isOn = false;
				createNodeButton.SetActive(false);
				removeNodeButton.SetActive(false);
			}
			else
			{
				editModeToggle.GetComponent<Toggle>().isOn = true;
				createNodeButton.SetActive(true);
				removeNodeButton.SetActive(true);
			}
		}

		public void saveGraph()
		{
			if(graphStart.getSerializableEdges().Count != 0)
			{
				string json = JsonConvert.SerializeObject(graphStart.getSerializableEdges());
				string filePath = GameObject.Find("InformationObject").GetComponent<InformationScript>().fileName;
				//string filePath = "./Assets/Graphs/v3d-graphFile0.txt";

				if(File.Exists(filePath))
				{
					File.WriteAllText(filePath, json);
				}
				else
				{
					int count = 0;
					string path = "/storage/emulated/0/Downloads/";
					//string path = "./Assets/Graphs/";
					while(File.Exists(path + "v3d-graphFile" + count + ".txt"))
					{
						count++;
					}
					File.WriteAllText(path + "v3d-graphFile" + count + ".txt", json);
				}
			}
			SceneManager.LoadScene("startScreen");
		}

		//mouseover and mouseout functions
		
		public void saveGraphMouseOver()
		{
			gazedAtSaveGraph = true;
		}

		public void saveGraphMouseLeave()
		{
			gazedAtSaveGraph = false;
			timer = 0f;
		}

		public void toggleEditModeMouseOver()
		{
			gazedAtToggleEditMode = true;
		}

		public void toggleEditModeMouseLeave()
		{
			gazedAtToggleEditMode = false;
			timer = 0f;
		}

		public void deleteNodeMouseOver()
		{
			gazedAtDeleteNode = true;
		}

		public void deleteNodeMouseLeave()
		{
			gazedAtDeleteNode = false;
			timer = 0f;
		}

		public void onMouseOver()
		{
			gazedAtCreateNode = true;
		}

		public void onMouseLeave()
		{
			gazedAtCreateNode = false;
			timer = 0f;
		}

		private void updateTimer()
		{
			if (gazedAtCreateNode || gazedAtDeleteNode || gazedAtToggleEditMode || gazedAtSaveGraph)
			{
				timer += Time.deltaTime;
			}
		}

		private void checkTimeMouseDown()
		{
			if (timer >= gazeTime && gazedAtCreateNode)
			{
				// Create node
				createNode();
				timer = 0f;
			}
			else if(timer >= gazeTime && gazedAtDeleteNode)
			{
				deleteNode();
				timer = 0f;
			}
			else if(timer >= gazeTime && gazedAtToggleEditMode)
			{
				toggleEditMode();
				timer = 0f;
			}
			else if(timer >= gazeTime && gazedAtSaveGraph)
			{
				saveGraph();
				timer = 0f;
			}
		}
	}
}

