﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Player1Character", 0);
        PlayerPrefs.SetInt("Player2Character", 0);
        startButton.Select();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CharSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
