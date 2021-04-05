using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur Volume dan Music yang ada pada game
public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer Instance { get; private set; }

    [SerializeField] [Range(0, 1)] float musicVolume = 0.5f;
    [SerializeField] AudioSource audioSource;

    // Use this for initialization
    void Awake () 
	{
        SetUpSingleton();
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

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        audioSource.volume = musicVolume;
    }


}
