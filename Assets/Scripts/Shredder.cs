using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Digunakan sebagai batas akhir untuk menghancurkan object yg menyentuh batas paling bawah
public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
