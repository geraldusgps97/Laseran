﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur Power up weapon
public class BoostWeapon : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool active = true;
    [SerializeField] private int dropSpeed = 3;
    [SerializeField] private int weaponBoostValue = 1;
    [SerializeField] private int scoreValue = 1000;
    [SerializeField] private GameObject collectedVFX;
    //Trigger pada saat mengambil power up
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && active == true)
        {
            PickUp();
        }
    }
    //Mengatur pada saat mengambil power up
    private void PickUp()
    {
        // Menghindari mengambil dobel pada saat bersamaan
        active = false;

        // Spawn efek animasi pada saat mengambil power up
        Animator animator = GetComponent<Animator>();
        animator.Play("Boost Collected");
        Instantiate(collectedVFX, transform.position, Quaternion.identity);

        // Menggunakannya pada player
        GameSession.Instance.SetWeapon(weaponBoostValue);
        GameSession.Instance.AddToScore(scoreValue);

        // Setelah mengambil power up, power up akan hancur
        Destroy(gameObject, .5f);
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(active == true)
        {
            rb2d.velocity = Vector2.down * dropSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}
