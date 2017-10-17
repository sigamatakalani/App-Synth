using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetFiles : MonoBehaviour {

	public List<string> fileNames;
    public GameObject InterfaceButton;

    private int xPos = 4;
    private int yPos = 0;

    private int rowWidth = 12;
    private int elementWidth = 3;

    // Use this for initialization
    void Start () {

        //Make server call, gets a list of json file names
        //fileNames = new string[] {"Interesting Graph", "Thoughtful Graph", "Wonderful Graph", "Breakthrough Graph", "Graphic Graph", "Connected Graph", "Di-Graph", "Speculative Graph", "Intriguing Graph", "Curious Graph", "Benign Graph", "Benevolent Graph", "Unique Graph", "Strange Graph", "Chronograph"};

        //foreach (string file in System.IO.Directory.GetFiles("./Assets/Graphs/"))
        //{
        //    Debug.Log(Path.GetFileName(file));
        //    fileNames.Add(Path.GetFileName(file));
        //}

        //on pc
        string path = "./Assets/Graphs/"; // TODO

        //on phone
        //string path = "/storage/emulated/0/"; // TODO
        ApplyAllFiles(path, ProcessFile);
        
        
        
        //foreach (string file in System.IO.Directory.GetFiles("/storage/emulated/0/Graphs/"))
        //{
        //    Debug.Log(Path.GetFileName(file));
        //    fileNames.Add(Path.GetFileName(file));
        //}


        int pos = 0;

        
        foreach(string file in fileNames)
        {
            Debug.Log(file);

            GameObject fileButton = Instantiate(InterfaceButton) as GameObject;
            fileButton.name = file;
            GameObject fileTitle = fileButton.transform.Find("FileTitle").gameObject;
            fileTitle.GetComponent<TextMesh>().text = Path.GetFileName(file);
            fileButton.transform.position = placeButtonFlat(pos);
            pos++;
        }
	}

     void ProcessFile(string path) {/* ... */
        fileNames.Add(path);
    }
    static void ApplyAllFiles(string folder, Action<string> fileAction)
    {
        foreach (string file in Directory.GetFiles(folder))
        {
            string ext = Path.GetExtension(file);
            Debug.Log("extension: "+ext);
            if (ext == ".json" || ext == ".txt") {
                string st = Path.GetFileName(file).Substring(0, 4);
                Debug.Log("beg: " + st);
                if (st == "v3d-") {
                    fileAction(file);
                }
            }
        }
        foreach (string subDir in Directory.GetDirectories(folder))
        {
            try
            {
                ApplyAllFiles(subDir, fileAction);
            }
            catch
            {
                // swallow, log, whatever
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector3 placeButtonSpiral(double pos)
    {

        if(pos > 0)
        {

            float x = (float)(Math.Pow(1.5, 0.3 * pos) * Math.Cos(4 * pos));
            float y = (float)(Math.Pow(1.5, 0.3 * pos) * Math.Sin(4 * pos));
            float z = (float)(-(Math.Pow(1.5, 0.3 * pos)) + 10);
            return new Vector3(x, y, z);
        }

        return new Vector3(0f, 0f, 9f);
    }

    public Vector3 placeButtonFlat(int pos)
    {

        int rowElements = rowWidth/elementWidth;
        float colPos = (float)Math.Ceiling((decimal)(pos / rowElements)) * -3 + 4;
        float rowPos = (float)(Math.Floor((double)(pos % rowElements)) * 3 - 4.5);

        return new Vector3(rowPos, colPos, 9f);
    }
}
