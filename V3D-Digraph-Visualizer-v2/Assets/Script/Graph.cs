﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

//helper classes
[System.Serializable]
public class Edge
{
    public Node parent;
    public Relationship relationship;
    public Node child;

    public Edge()
    {
        parent = new Node();
        relationship = new Relationship();
        child = new Node();
    }
}

public class Node
{
    public string name;
    public string label;
    public string colour;
    public List<KeyValuePair<string, string>> attributes;

    public Node()
    {
        name = "";
        label = "";
        attributes = new List<KeyValuePair<string, string>>();
    }
}

public class Relationship
{
    public string label;
    public int weight;

    public Relationship()
    {   
        label = "";
        weight = 0;
    }
}

public class EdgePairs
{
    public GameObject parent;
    public GameObject child;
    public Relationship relationship;

    public EdgePairs()
    {
        parent = new GameObject();
        child = new GameObject();
        relationship = new Relationship();
    }

    public EdgePairs(GameObject parent1, GameObject child1, Relationship relationship1)
    {

        parent = parent1;
        child = child1;
        relationship = relationship1;
    }
}

public class ArrowInfo
{
    public GameObject myArrow;
    public string myParentNode;
    public string myChildNode;

    public ArrowInfo(GameObject a, string p, string c)
    {
        myArrow = a;
        myParentNode = p;
        myChildNode = c;
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
