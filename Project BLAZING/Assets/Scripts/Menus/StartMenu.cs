using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public GameObject creditsImage;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Player1Character", 0);
        PlayerPrefs.SetInt("Player2Character", 0);
        startButton.Select();
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        creditsImage.SetActive(false);
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
        if (creditsImage.active)
        {
            creditsImage.SetActive(false);
        }
        else
        {
            creditsImage.SetActive(true);
        }
        
    }
}
