using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur parallax scroller atas kebawah pada background mulai dari kecepatan sampai rendering dan material
public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = 1f;
    Material myMaterial;
    Vector2 offSet;

	// Use this for initialization
	void Start () 
	{
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
	}
}
