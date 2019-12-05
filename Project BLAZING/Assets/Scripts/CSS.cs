using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CSS : MonoBehaviour
{
    public TMP_Dropdown p1Selection;
    public TMP_Dropdown p2Selection;

    public Image p1ReadyTick;
    public Image p2ReadyTick;

    bool p1Ready;
    bool p2Ready;

    // Start is called before the first frame update
    void Start()
    {
        p1ReadyTick.enabled = false;
        p2ReadyTick.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer1Ready()
    {
        p1Ready = !p1Ready;
        p1ReadyTick.enabled = !p1ReadyTick.enabled;
        if (p1Ready && p2Ready)
        {
            LoadArena();
        }
    }

    public void SetPlayer2Ready()
    {
        p2Ready = !p2Ready;
        p2ReadyTick.enabled = !p2ReadyTick.enabled;
        if (p1Ready && p2Ready)
        {
            LoadArena();
        }
    }

    public void LoadArena()
    {
        PlayerPrefs.SetInt("Player1Character", p1Selection.value);
        PlayerPrefs.SetInt("Player2Character", p2Selection.value);
        SceneManager.LoadScene("DemoArena");
    }
}
