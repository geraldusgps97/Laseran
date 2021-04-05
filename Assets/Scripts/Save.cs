using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public InputField textBox;

    public void clickSaveButton()
    {
        PlayerPrefs.SetString("name", textBox.text);
    }
}
