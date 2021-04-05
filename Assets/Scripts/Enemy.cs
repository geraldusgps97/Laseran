using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//Mengatur musuh yang ada pada game
public class Enemy : MonoBehaviour {
    //mengatur statistik musuh
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue;
    [SerializeField] GameObject boostItem = null;
    //mengatur tembakan musuh
    [Header("Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject weapon;
    [SerializeField] float projectileSpeed = 10f;
    //mengatur visual efek dan suara pada saat musuh mati
    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    private bool onScreen = false;


    // Use this for initialization
    void Start () 
	{
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	 
	// Update is called once per frame
	void Update () 
	{
        CountDownAndShoot();
	}
    //Mengatur seberapa sering musuh menembak
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f && onScreen)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        // mengatur tembakan musuh
        GameObject projectile = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;

        // Mengambil arah tembakan musuh
        List<float> projectileDirectionList = projectile.GetComponent<WeaponConfig>().GetProjectileDirections();

        // Mengambil children tembakan dari GameObject
        List<Rigidbody2D> projectileRigidBodies = projectile.transform.GetComponentsInChildren<Rigidbody2D>().ToList();

        // mengatur rBodyCounter untuk iterasi melalui  projectDirectionList
        int rBodyCounter = 0;

        // mengatur setiap child dari tembakan
        foreach (var rBody in projectileRigidBodies)
        {
            // memasukan arah tembakan dari velocity
            rBody.velocity = new Vector2(projectileDirectionList[rBodyCounter], -projectileSpeed);

            // Iterasi rBodyCounter
            rBodyCounter++;
        }
        SoundManager.Instance.TriggerEnemyShotSFX();

    }
    //Mengatur damage collider 2d
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onScreen)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer)
            {
                return;
            }
            else
            {
                if (other.tag == "Projectile")
                {
                    Destroy(other);
                }
            }
            ProcessHit(damageDealer);
        }
    }
    //Mengatur proses terkena tembakan dari player
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    //Mengatur pada saat musuh mati akan mendapat visual efek,sound efek dan spawn power up
    private void Die()
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        GameSession.Instance.AddToScore(scoreValue);
        SoundManager.Instance.TriggerEnemyDeadSFX();
        Destroy(gameObject);

        if(boostItem != null)
        {
            Instantiate(boostItem, transform.position, Quaternion.identity);
            Debug.Log("Spawning boost: " + boostItem.name);
        }

        if(gameObject.name.StartsWith("Boss"))
        {
            SceneLoader.Instance.LoadGameOver();
        }
    }
    // mengatur power up
    public void SetBoostItem(GameObject boost)
    {
        boostItem = boost;
    }

    private void OnBecameVisible()
    {
        onScreen = true;
    }

    private void OnBecameInvisible()
    {
        onScreen = false;
    }
}
