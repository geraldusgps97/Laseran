using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config")]

//untuk mengatur setiap musuh yg keluar setiap wave
public class WaveConfig : ScriptableObject {

    //mengatur musuh yang akan di spawn
    [Header("Enemy")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0f;
    //[SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 0;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float delaySpawn = 0f;
    //Mengatur power up yg dikeluarkan setiap wave
    [Header("Boost")]
    [SerializeField] float boostSpawnChance = 0.5f;
    [SerializeField] GameObject boostItem = null;
    [SerializeField] int enemyToSpawnBoost = -1;
    //Mengambil gameobject prefab enemy
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    //Mengatur arah dan jalur dari wave
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    //Mengambil Spawn time setiap wave
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    //public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    
    //Mengambil jumlah musuh 
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    //Mengambil kecepatan wave
    public float GetMoveSpeed() { return moveSpeed; }
    //Mengambil delayspawn wave
    public float GetDelayNextSpawn()
    {
        return delaySpawn;
    }
    //Mengatur spawn rate dari boost item/power up
    private void OnEnable()
    {
        if(boostSpawnChance > 0)
        {
            float boostSpawnRoll = UnityEngine.Random.Range(0f, 1f);
            if(boostSpawnRoll <= boostSpawnChance)
            {
                SetBoostToSpawn();
                SetEnemyToSpawnBoost();
            }
        }
    }
    //Mengatur spawn boost item per musuh dikalahkan
    private void SetEnemyToSpawnBoost()
    {
        if(enemyToSpawnBoost == -1)
        {
            enemyToSpawnBoost = UnityEngine.Random.Range(0, numberOfEnemies);
        }
    }
    //Mengatur item boost spawn per wave
    private void SetBoostToSpawn()
    {
        if(boostItem == null)
        {
            Debug.Log(name + "called SetBoostToSpawn");
            List<GameObject> boostItemList = GameSession.Instance.GetboostList();
            boostItem = boostItemList[UnityEngine.Random.Range(0, boostItemList.Count)];
        }
    }

    public int GetEnemyToSpawnBoost()
    {
        return enemyToSpawnBoost;
    }

    public GameObject GetBoostToSpawn()
    {
        return boostItem;
    }
}

