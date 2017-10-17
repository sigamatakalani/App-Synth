using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nodeInfoView : MonoBehaviour
{
    public Text ifo_Text;
	public List<EdgePairs> pairsList;

    // Use this for initialization
    void Start()
    {
        ifo_Text = GameObject.Find("Canvas/NodeInfo/Label").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pointerEnter()
    {
		ifo_Text.text = "Name: " + gameObject.name;	
    }

    public void pointerExit()
    {
        //Debug.Log("looking out");

        ifo_Text.text = "...";
    }

}

