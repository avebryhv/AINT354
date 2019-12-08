using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CSS : MonoBehaviour
{
    public CSSSelected p1SelectionBox;
    public CSSSelected p2SelectionBox;


    int p1Selection;
    int p2Selection;

    public GameObject readyPanel;

    bool p1Ready;
    bool p2Ready;    

    // Start is called before the first frame update
    void Start()
    {
        p1Selection = 0;
        p2Selection = 0;
        readyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetPlayer1Ready()
    //{
    //    p1Ready = !p1Ready;
    //    //p1ReadyTick.enabled = !p1ReadyTick.enabled;
    //    if (p1Ready && p2Ready)
    //    {
    //        LoadArena();
    //    }
    //}

    //public void SetPlayer2Ready()
    //{
    //    p2Ready = !p2Ready;
    //    //p2ReadyTick.enabled = !p2ReadyTick.enabled;
    //    if (p1Ready && p2Ready)
    //    {
    //        LoadArena();
    //    }
    //}

    public void CheckReady()
    {
        if (p1SelectionBox.locked && p2SelectionBox.locked)
        {
            readyPanel.SetActive(true);
            //LoadArena();
        }
        else
        {
            readyPanel.SetActive(false);
        }
    }

    public void LoadArena()
    {
        PlayerPrefs.SetInt("Player1Character", p1Selection);
        PlayerPrefs.SetInt("Player2Character", p2Selection);
        SceneManager.LoadScene("DemoArena");
    }

    public void SetP1Selection(int num, Image im, CSSItem item)
    {
        p1SelectionBox.LockSelection(item);
        p1SelectionBox.SetSelectionImage(im);
        p1Selection = num;
    }

    public void P1HoverOver(Image im, CSSItem item)
    {
        p1SelectionBox.SetSelectionImage(im);
        p1SelectionBox.SetHover(item);
    }

    public void P1HoverOff()
    {
        p1SelectionBox.Clear();
    }

    public void RemoveP1Lock()
    {
        p1SelectionBox.Unlock();
        CheckReady();
    }

    public void SetP2Selection(int num, Image im, CSSItem item)
    {
        p2SelectionBox.LockSelection(item);
        p2SelectionBox.SetSelectionImage(im);
        p2Selection = num;
    }

    public void P2HoverOver(Image im, CSSItem item)
    {
        p2SelectionBox.SetSelectionImage(im);
        p2SelectionBox.SetHover(item);
    }

    public void P2HoverOff()
    {
        p2SelectionBox.Clear();
    }

    public void SetP2Selection(int num)
    {
        p2Selection = num;
    }

    public void RemoveP2Lock()
    {
        p2SelectionBox.Unlock();
        CheckReady();
    }

    public void StartPressed()
    {
        if (p1SelectionBox.locked && p2SelectionBox.locked)
        {
            LoadArena();
        }
        
    }


}
