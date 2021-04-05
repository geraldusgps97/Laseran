using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Mengatur display nyawa player
public class HealthDisplay : MonoBehaviour {

    Slider healthSlider;
    GameSession gameSession;

    void Awake()
    {
        healthSlider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        healthSlider.value = GameSession.Instance.GetHealth();
    }
}
