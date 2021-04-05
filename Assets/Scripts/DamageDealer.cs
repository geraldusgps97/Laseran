using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur damage yg diberikan
public class DamageDealer : MonoBehaviour {

    [SerializeField] int damage = 100;

	public int GetDamage() {return damage; }
    //Mengatur pada saat terkena tembakan
    public void Hit()
    {
        if(gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
