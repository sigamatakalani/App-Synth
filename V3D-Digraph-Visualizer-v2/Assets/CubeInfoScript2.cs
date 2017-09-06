using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeInfoScript2 : NetworkBehaviour
{

    public string objJson = "{'items': [ {'child': 'Node2','parent': 'Node1','value': 11},{'parent': 'Node2','value': 22,'child': 'Node3'},{'parent': 'Node2','value': 33,'child': 'Node4'},{'parent': 'Node2','value': 44,'child': 'Node5'}]}";

}
