using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBehaviour : MonoBehaviour {

    private Renderer myRenderer;
    public Material HighLightMaterial;
    private Material normalMaterial;
	// Use this for initialization
	void Start () {
        this.myRenderer = GetComponent<Renderer>();
        this.normalMaterial = this.myRenderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HighLight() {
        myRenderer.material = HighLightMaterial;
    }

    public void NormalRendering() {
        myRenderer.material = normalMaterial;
    }
}
