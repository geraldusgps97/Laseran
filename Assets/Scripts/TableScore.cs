using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TableScore : MonoBehaviour
{
    public Text Playeran; 
    public Text Playeran2;
    public Text Playeran3;
    public Text Playeran4;
    public Text Playerskor;
    public Text Playerskor2;
    public Text Playerskor3;
    public Text Playerskor4;

    string player_an;
    string player_an2;
    string player_an3;
    string player_an4;

    int playerSkor;
    int playerSkor2;
    int playerSkor3;
    int playerSkor4;

    // Start is called before the first frame update
    void Start()
    {
        player_an = PlayerPrefs.GetString("name");
        player_an2 = PlayerPrefs.GetString("Name2", "Dapid");
        player_an3 = PlayerPrefs.GetString("Name3", "Clipod");
        player_an4 = PlayerPrefs.GetString("Name4", "Betrek");

        playerSkor = PlayerPrefs.GetInt("score");
        playerSkor2 = PlayerPrefs.GetInt("score2", 12000);
        playerSkor3 = PlayerPrefs.GetInt("score3", 9000);
        playerSkor4 = PlayerPrefs.GetInt("score4", 4500);

        if (playerSkor > playerSkor2)
        {
            Playeran.text = player_an;
            Playeran2.text = player_an2;
            Playeran3.text = player_an3;
            Playeran4.text = player_an4;
            Playerskor.text = playerSkor.ToString();
            Playerskor2.text = playerSkor2.ToString();
            Playerskor3.text = playerSkor3.ToString();
            Playerskor4.text = playerSkor4.ToString();
        }
        if (playerSkor2 > playerSkor && playerSkor > playerSkor3 && playerSkor > playerSkor4)
        {
            Playeran.text = player_an2;
            Playeran2.text = player_an;
            Playeran3.text = player_an3;
            Playeran4.text = player_an4;
            Playerskor.text = playerSkor2.ToString();
            Playerskor2.text = playerSkor.ToString();
            Playerskor3.text = playerSkor3.ToString();
            Playerskor4.text = playerSkor4.ToString();
        }
        if (playerSkor3 > playerSkor && playerSkor2 > playerSkor && playerSkor > playerSkor4)
        {
            Playeran.text = player_an2;
            Playeran2.text = player_an3;
            Playeran3.text = player_an;
            Playeran4.text = player_an4;
            Playerskor.text = playerSkor2.ToString();
            Playerskor2.text = playerSkor3.ToString();
            Playerskor3.text = playerSkor.ToString();
            Playerskor4.text = playerSkor4.ToString();
        }
        if (playerSkor4 > playerSkor)
        {
            Playeran.text = player_an2;
            Playeran2.text = player_an3;
            Playeran3.text = player_an4;
            Playeran4.text = player_an;
            Playerskor.text = playerSkor2.ToString();
            Playerskor2.text = playerSkor3.ToString();
            Playerskor3.text = playerSkor4.ToString();
            Playerskor4.text = playerSkor.ToString();
        }

    }

}