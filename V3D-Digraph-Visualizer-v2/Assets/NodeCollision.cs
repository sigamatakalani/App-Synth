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
		CrudGraph.createEdge(gameObject, col.gameObject);
    }
}
