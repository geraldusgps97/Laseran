using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur spawn yang akan mengeluarkan wave musuh
public class Spawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

	// Use this for initialization
	IEnumerator Start () 
	{
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
	}
    //Mengatur spawn dari musuh dan power up
    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];

            //Debug.Log("Spawning Wave: " + waveConfigs[waveIndex].name);

            // Untuk Delay Spawn selanjutnya
            yield return new WaitForSeconds(currentWave.GetDelayNextSpawn());

            StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    //Mengatur lebih detail spawn musuh dan power up
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            

            if(enemyCount == waveConfig.GetEnemyToSpawnBoost())
            {
                newEnemy.GetComponent<Enemy>().SetBoostItem(waveConfig.GetBoostToSpawn());
            }

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

        
    }
}

