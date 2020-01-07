using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button creditsBackButton;
    public GameObject creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Player1Character", 0);
        PlayerPrefs.SetInt("Player2Character", 0);
        startButton.Select();
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        creditsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ControllerSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsScreen.SetActive(true);
        creditsBackButton.Select();
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
        startButton.Select();
    }
}
