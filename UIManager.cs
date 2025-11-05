using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public TextMeshProUGUI TimeCounterGameplay;
    public TextMeshProUGUI TimeCounterWin;
    private float TimeSeconds;
    private int TimeMinutes;
    private bool Win;

    public GameObject WinScreen;

    void Awake()
    {
        inst = this;
    }

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        Win = true;
        TimeCounterWin.text = "Tiempo: " + TimeMinutes + ":" + Mathf.Ceil(TimeSeconds);
        TimeCounterGameplay.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Win)
        {
            TimeSeconds += Time.deltaTime;

            if (TimeSeconds >= 59)
            {
                TimeMinutes++;
                TimeSeconds = 0;
            }

            TimeCounterGameplay.text = "Tiempo: " + TimeMinutes + ":" + Mathf.Ceil(TimeSeconds);
        }
    }
}
