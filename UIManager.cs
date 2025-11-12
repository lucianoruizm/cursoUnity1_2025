using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public TextMeshProUGUI TimeCounterGameplay;
    public TextMeshProUGUI TimeCounterWin;
    public TextMeshProUGUI CurrentTimeCounter;
    private float TimeSeconds;
    private int TimeMinutes;
    public Button RetryButton;
    public Button ContinueButton;
    public Button MainMenuButton;

    private bool Win;
    public bool Pause;

    public GameObject WinScreen;
    public GameObject PauseScreen;

    void Awake()
    {
        inst = this;

        RetryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        ContinueButton.onClick.AddListener(() =>
        {
            Pause = false;
            PauseScreen.SetActive(false);
            TimeCounterGameplay.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        });
    }

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        Win = true;
        TimeCounterWin.text = "Tiempo: " + TimeMinutes + ":" + Mathf.Ceil(TimeSeconds);
        TimeCounterGameplay.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowPauseScreen()
    {
        PauseScreen.SetActive(true);
        Pause = true;
        CurrentTimeCounter.text = "Tiempo actual: " + TimeMinutes + ":" + Mathf.Ceil(TimeSeconds);
        TimeCounterGameplay.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!Win && !Pause)
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
