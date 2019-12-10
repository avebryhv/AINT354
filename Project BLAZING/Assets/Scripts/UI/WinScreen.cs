using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI winText;
    Canvas canvas;
    public Button firstButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowCanvas()
    {
        canvas.enabled = true;
        GameFunctions.ForcePause();
        firstButton.Select();
    }

    public void Player1Win()
    {
        ShowCanvas();
        FindObjectOfType<DataToSheet>().RecieveWinner("Player 1");
        winText.text = "Player 1 Win";
        PlayerPrefs.SetInt("Player1Score", PlayerPrefs.GetInt("Player1Score") + 1);
    }

    public void Player2Win()
    {
        ShowCanvas();
        FindObjectOfType<DataToSheet>().RecieveWinner("Player 2");
        winText.text = "Player 2 Win";
        PlayerPrefs.SetInt("Player2Score", PlayerPrefs.GetInt("Player2Score") + 1);
    }


    public void Rematch()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void MechSelect()
    {
        SceneManager.LoadScene("CharSelect");
    }


}
