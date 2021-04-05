using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur sesi dan fitur dari game
public class GameSession : MonoBehaviour {

    public static GameSession Instance { get; private set; }
    //Mengatur nyawa dan shield
    [Header("Shield")]
    [SerializeField] int health = 100;
    int healthMax = 100;
    [SerializeField] int shieldHealth = 0;
    [SerializeField] public int shieldLayer01 = 100;
    [SerializeField] public int shieldLayer02 = 200;
    [SerializeField] public int shieldLayer03 = 300;
    //Mengatur tembakan
    [Header("Projectile")]
    [SerializeField] GameObject weapon;
    [SerializeField] int weaponLevel = 0;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [SerializeField] List<GameObject> weaponsList;
    [SerializeField] ParticleSystem weaponBoostVFX;
    //Mengatur Kecepatan,power up dan score
    [Header("Other")]
    [SerializeField] int playerMovementSpeed = 4;
    [SerializeField] List<GameObject> boostList;
    [SerializeField] GameObject player;
    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
        SetWeapon(0);
    }

    private void SetUpSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
    //Mengambil score yang didapat player
    public int GetScore()
    {
        return score;
    }
    //Memasukkan score pada UI score
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        PlayerPrefs.SetInt("score", score);
    }
    //Untuk memulai ulang game
    public void ResetGame()
    {
        //health = 100;
        //shieldHealth = 0;
        //weaponLevel = 0;
        //playerMovementSpeed = 4;
        //score = 0;
        Destroy(gameObject);
    }
    //Mengambil Nyawa pemain
    public int GetHealth()
    {
        return health;
    }
    //Menghitung Nyawa pemain
    public void SubtractHealth(int healthValue)
    {
        if(shieldHealth > 0)
        {
            SubtractShieldHealth(healthValue);
        }
        else
        {
            health = Mathf.Clamp((health -= healthValue), 0, healthMax);
        }
    }
    //Menambah nyawa pemain pada saat mendapat power up
    public void AddHealth(int healthValue)
    {
        health = Mathf.Clamp((health += healthValue), 0, healthMax);
    }
    //Mengambil int nyawa shield
    public int GetShieldHealth()
    {
        return shieldHealth;
    }
    //Menghitung shield pemain
    public void SubtractShieldHealth(int healthValue)
    {
        shieldHealth -= healthValue;
    }
    //Menambah nyawa shield pada saat mendapat power up
    public void AddShieldHealth(int healthValue)
    {
        shieldHealth = Mathf.Clamp((shieldHealth += healthValue), 0, shieldLayer03);
        
    }
    //Mengambil power up list
    public List<GameObject> GetboostList()
    {
        return boostList;
    }
    //Mengambil senjata yg ada pada gameobject
    public GameObject GetWeapon()
    {
        return weapon;
    }
    //Mengatur jenis senjata pada saat mendapat power up senjata
    public void SetWeapon(int weaponBoostValue)
    {
        if(player != null)
        {
            weaponLevel = Mathf.Clamp((weaponLevel + weaponBoostValue), 0, weaponsList.Count - 1);
            weapon = weaponsList[weaponLevel];
            player.GetComponent<Player>().SetWeapon();
            //PlayWeaponBoostVFX();
        }
    }
    //Memulai visual efek dari power up senjata
    private void PlayWeaponBoostVFX()
    {
        if(weaponLevel > 0)
        {
            ParticleSystem boostVFX = Instantiate(weaponBoostVFX, player.transform.position, Quaternion.identity);
            Destroy(boostVFX.gameObject, 1f);
        }
    }
    //Memanggil kecepatan player
    public int GetPlayerSpeed()
    {
        return playerMovementSpeed;
    }
    //Mengatur kecepatan pemain pada saat mendapat power up
    public void SetPlayerSpeed(int speedValue)
    {
        playerMovementSpeed = speedValue;
    }
    //Menambahkan kecepatan pada saat mendapat power up
    public void AddPlayerSpeed(int speedValue)
    {
        playerMovementSpeed += speedValue;
        player.GetComponent<Player>().SetSpeed();
    }
}
