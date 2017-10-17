using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrudGraphNamespace;

public class NodeCollision : MonoBehaviour 
{
	string node1;
	string node2;

	void OnCollisionEnter (Collision col)
    {
        //Debug.Log(gameObject.GetComponent<NodeCollision>().enabled + " " + col.gameObject.GetComponent<NodeCollision>().enabled);
		CrudGraph.createEdge(gameObject, col.gameObject);
    }
}
