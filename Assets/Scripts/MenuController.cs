using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Mengatur UI Menu yang ada pada game (Main Menu dan Option Button)
public class MenuController : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    private void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    private void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }

    private void DisableOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    private void EnableOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void OnClickOptionsButton()
    {
        DisableMainMenu();
        EnableOptionsMenu();
    }

    public void OnClickMainMenuButton()
    {
        DisableOptionsMenu();
        EnableMainMenu();
    }
}
