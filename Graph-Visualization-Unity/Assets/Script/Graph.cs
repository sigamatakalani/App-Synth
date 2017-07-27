using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Graph : MonoBehaviour {

    public void createGraph(List<GameObject> vertexList, List<EdgePairs> pairsList, GameObject nodePrefab)
    {
        string json = File.ReadAllText("Assets/Graphs/graph.json");

        Edge[] tempEdgeList = JsonHelper.FromJson<Edge>(json);

        GameObject parent;
        GameObject child;


        foreach (Edge edge in tempEdgeList)
        {
            if (!vertexList.Any(x => x.name == edge.parent))
            {
                parent = Instantiate(nodePrefab) as GameObject;
                parent.name = edge.parent;
                parent.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                vertexList.Add(parent);
            }
            else
            {
                parent = vertexList.Where(x => x.name == edge.parent).FirstOrDefault();
            }

            if (!vertexList.Any(x => x.name == edge.child))
            {
                child = Instantiate(nodePrefab) as GameObject;
                child.name = edge.child;
                child.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                vertexList.Add(child);
            }
            else
            {
                child = vertexList.Where(x => x.name == edge.child).FirstOrDefault();
            }

            //Add new edge to graph
            EdgePairs tempEdge = new EdgePairs(parent, child, edge.value);
            pairsList.Add(tempEdge);
            //connectTwoNodesDraw(parent, child);
        }
    }

    public void createGraph2(List<GameObject> vertexList, List<EdgePairs> pairsList, GameObject nodePrefab)
    {
        string json = File.ReadAllText("Assets/Graphs/graph.json");

        Edge[] tempEdgeList = JsonHelper.FromJson<Edge>(json);

        //GameObject parent;
        //GameObject child;


        foreach (Edge edge in tempEdgeList)
        {
            if (!vertexList.Any(x => x.name == edge.parent))
            {
                GameObject parent = Instantiate(nodePrefab) as GameObject;
                parent.name = edge.parent;
                parent.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                vertexList.Add(parent);
              //  parent.
            }

            if (!vertexList.Any(x => x.name == edge.child))
            {
                GameObject child = Instantiate(nodePrefab) as GameObject;
                child.name = edge.child;
                child.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                vertexList.Add(child);
                Destroy(child);
            }

            //Add new edge to graph
            
        }

        foreach (Edge edge in tempEdgeList)
        {

            EdgePairs tempEdge = new EdgePairs(vertexList.Where(U => U.name == edge.parent).FirstOrDefault(), vertexList.Where(U => U.name == edge.child).FirstOrDefault(), edge.value);
            pairsList.Add(tempEdge);
            //connectTwoNodesDraw(parent, child);
        }
    }
}


//helper classes
[System.Serializable]
public class Edge
{
    public string parent;
    public int value;
    public string child;

    public Edge()
    {
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
