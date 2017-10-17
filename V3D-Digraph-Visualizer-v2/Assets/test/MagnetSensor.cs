/*using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MagnetSensor : MonoBehaviour
{
	public delegate void CardboardTrigger();
	public static event CardboardTrigger OnCardboardTrigger;

	private const int WINDOW_SIZE = 40;
	private const int NUM_SEGMENTS = 2;
	private const int SEGMENT_SIZE = WINDOW_SIZE / NUM_SEGMENTS;
	private const int T1 = 30, T2 = 130;

	private List<Vector3> _sensorData;
	private float[] _offsets;

    //public GameObject nodePrefab;
    //public GameObject node1 = Instantiate(nodePrefab) as GameObject;
    //node1.name = "node1";
    /*node1.transform.position = new Vector3(Random.Range(-1.0f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
    //node1.AddComponent<LineRenderer>();
    //node1.AddComponent<Dragable>();
    node1.AddComponent<InteractionBehaviour>();
    //node1.AddComponent<SphereCollider>();
    //node1.GetComponent<Rigidbody>().useGravity = false;
    //node1.GetComponent<Rigidbody>().drag = 3;
    //node1.transform.parent = transform;
    node1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    public GameObject node2 = Instantiate(nodePrefab) as GameObject;
    //node2.name = "node2";
    node2.transform.position = new Vector3(Random.Range(-1.0f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
    //node1.AddComponent<LineRenderer>();
    //node1.AddComponent<Dragable>();
    //node2.AddComponent<InteractionBehaviour>();
    //node1.AddComponent<SphereCollider>();
    //node2.GetComponent<Rigidbody>().useGravity = false;
    //node2.GetComponent<Rigidbody>().drag = 3;
    //node2.transform.parent = transform;
    node2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);*/

	/*void Awake()
	{
		_sensorData = new List<Vector3>(WINDOW_SIZE);
		_offsets = new float[SEGMENT_SIZE];
	}

	void OnEnable()
	{
		_sensorData.Clear();
		Input.compass.enabled = true;
        //draggingObject.GetComponent<Renderer>().material.color = Color.blue;
	}

	void OnDisable()
	{
		Input.compass.enabled = false;
	}

	void Update ()
	{
		Vector3 currentVector = Input.compass.rawVector;
		if(currentVector.x == 0 && currentVector.y == 0 && currentVector.z == 0) return;

		if(_sensorData.Count >= WINDOW_SIZE) _sensorData.RemoveAt(0);
		_sensorData.Add(currentVector);

		EvaluateModel();
	}

	private void EvaluateModel()
	{
		if(_sensorData.Count < WINDOW_SIZE) return;

		float[] means = new float[2];
		float[] maximums = new float[2];
		float[] minimums = new float[2];

		Vector3 baseline = _sensorData[_sensorData.Count - 1];

		for(int i = 0; i < NUM_SEGMENTS; i++)
		{
			int segmentStart = 20 * i;
			_offsets = ComputeOffsets(segmentStart, baseline);

			means[i] = ComputeMean(_offsets);
			maximums[i] = ComputeMaximum(_offsets);
			minimums[i] = ComputeMinimum(_offsets);
		}

		float min1 = minimums[0];
		float max2 = maximums[1];

		if(min1 < T1 && max2 > T2)
		{
			_sensorData.Clear();
			OnCardboardTrigger();
		}
	}

	private float[] ComputeOffsets(int start, Vector3 baseline)
	{
		for(int i = 0; i < SEGMENT_SIZE; i++)
		{
			Vector3 point = _sensorData[start + i];
			Vector3 o = new Vector3(point.x - baseline.x, point.y - baseline.y, point.z - baseline.z);
			_offsets[i] = o.magnitude;
		}

		return _offsets;
	}

	private float ComputeMean(float[] offsets)
	{
		float sum = 0;
		foreach(float o in offsets)
		{
			sum += o;
		}
		return sum / offsets.Length;
	}

	private float ComputeMaximum(float[] offsets)
	{
		float max = float.MinValue;
		foreach(float o in offsets)
		{
			max = Mathf.Max(o, max);
		}
		return max;
	}

	private float ComputeMinimum(float[] offsets)
	{
		float min = float.MaxValue;
		foreach(float o in offsets)
		{
			min = Mathf.Min(o, min);
		}
		return min;
	}
}*/