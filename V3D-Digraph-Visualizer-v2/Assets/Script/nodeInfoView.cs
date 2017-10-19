using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vdrGraph;
using System.Linq;

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
        string attributes = "";
        Edge tempEdge = graphStart.getSerializableEdges().Where(x => x.parent.name == gameObject.name || x.child.name == gameObject.name).FirstOrDefault();
        Node tempNode;

        if(tempEdge != null)
        {
            if(tempEdge.parent.name == gameObject.name)
            {
                tempNode = tempEdge.parent;
            }
            else{
                tempNode = tempEdge.child;
            }
            foreach(Dictionary<string, string> attr in tempNode.attributes)
            {
                foreach(KeyValuePair<string, string> obj in attr)
                {
                    attributes += obj.Key + " : " + obj.Value + " ";
                }
            }
        }
        
        Debug.Log(attributes);
		ifo_Text.text = attributes;	
    }

    public void pointerExit()
    {
        //Debug.Log("looking out");

        ifo_Text.text = "...";
    }

}

