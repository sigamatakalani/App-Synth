using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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
