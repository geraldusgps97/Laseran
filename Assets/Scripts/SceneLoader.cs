using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Untuk Memuat scene
public class SceneLoader : MonoBehaviour {

    public static SceneLoader Instance { get; private set; }

    [SerializeField] float sceneLoadDelay = 3f;

    private void Awake()
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

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadLeve01()
    {
        GameSession.Instance.ResetGame();
        SceneManager.LoadScene("Level01");
    }

    public void LoadLeve02()
    {
        GameSession.Instance.ResetGame();
        SceneManager.LoadScene("Level02");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
