using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur sownd dari game
public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioClip playerShotSFX, playerDeadSFX, enemyDeadSFX, enemyShotSFX, buttonClickSFX;

    [SerializeField] AudioSource audioSource;

    [SerializeField] [Range(0, 1)] float sFXVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float playerShotVolume = 1f;
    [SerializeField] [Range(0, 1)] float playerDeadVolume =  1f;
    [SerializeField] [Range(0, 1)] float enemyShotVolume = 1f;
    [SerializeField] [Range(0, 1)] float enemyDeadVolume = 1f;
    [SerializeField] [Range(0, 1)] float buttonClickVolume = 1f;

    private void Awake()
    {
        SetUpSingleton();
        SetSFXVolume(sFXVolume);
    }

    private void SetUpSingleton()
    {
        // Mengecek awal ada konflik dengan instance lain/tidak
        if (Instance != null)
        {
            // Jika ada destroy instance yg lain
            Destroy(gameObject);
        }
        else
        {
            // Menge save dari inputan yg kita buat
            Instance = this;

            // Kemudian memastikan agar tidak destroy di scene yg lain
            DontDestroyOnLoad(gameObject);
        }
    }

    //Trigger suara tembakan player
    public void TriggerPlayerShotSFX()
    {
        audioSource.PlayOneShot(playerShotSFX, (sFXVolume * playerShotVolume));
    }
    //Trigger suara player mati
    public void TriggerPlayerDeadSFX()
    {
        audioSource.PlayOneShot(playerDeadSFX, (sFXVolume * playerDeadVolume));
    }
    //Trigger suara tembakan musuh
    public void TriggerEnemyShotSFX()
    {
        audioSource.PlayOneShot(enemyShotSFX, (sFXVolume * enemyShotVolume));
    }
    //Trigger suara musuh mati
    public void TriggerEnemyDeadSFX()
    {
        audioSource.PlayOneShot(enemyDeadSFX, (sFXVolume * enemyShotVolume));
    }
    //Trigger suara klik tombol
    public void TriggerButtonClickSFX()
    {
        audioSource.PlayOneShot(enemyDeadSFX, (sFXVolume * enemyShotVolume));
    }
    //Mengambil suara efek khusus
    public float GetSFXVolume()
    {
        return sFXVolume;
    }
    //Mengeset suara efek khusus
    public void SetSFXVolume(float newVolume)
    {
        sFXVolume = newVolume;
        audioSource.volume = sFXVolume;
    }
}
