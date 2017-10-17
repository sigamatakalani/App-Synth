using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vdrGraph;
using System.Linq;

namespace CrudGraphNamespace
{
	public class CrudGraph : MonoBehaviour 
	{
		public GameObject nodePrefab;
		public GameObject arrowHead;
		private Vector3 center = new Vector3(0f,0f,7.5f);
		private float timer;
		private float holdTimer;
		private float gazeTime = 1f;
		private bool gazedAt;
		private bool gazeDown;
		private static LineRenderer line;
		private static Vector3 direction;

		private static EdgePairs tempEdge;
		// Use this for initialization
		void Start ()
		{
			
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
			node.name = "Node" + graphStart.getVertexList().Count;
			node.transform.GetChild(0).GetComponent<TextMesh>().text = node.name; //add name to label
			node.GetComponent<Rigidbody>().useGravity = false;
			node.GetComponent<Rigidbody> ().drag = 3;
			node.transform.parent = GameObject.Find("Graph").transform;
			node.transform.position = center + Random.onUnitSphere * 2f;
			graphStart.getVertexList().Add(node);
		}
		public static void createEdge(GameObject node1, GameObject node2)
		{
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
			}
		}

		public void onMouseOver()
		{
			gazedAt = true;
		}

		public void onMouseLeave()
		{
			gazedAt = false;
			timer = 0f;
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
				// Create node
				createNode();
				timer = 0f;
			}
		}
	}
}

