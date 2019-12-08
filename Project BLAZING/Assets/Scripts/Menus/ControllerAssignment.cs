using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerAssignment : MonoBehaviour
{
    public GameObject p1Panel;
    public GameObject p2Panel;
    public GameObject nullPanel;

    public GameObject c1Image;
    public GameObject c2Image;
    public GameObject keyboardImage;

    public GameObject readyBanner;

    Gamepad firstGamepad;
    Gamepad secondGamepad;
    bool isTwoGamepads;

    bool p1Set;
    bool p2Set;
    bool readyToStart;

    int player1ControllerIndex;
    int player2ControllerIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        p1Set = false;
        p2Set = false;
        readyToStart = false;
        readyBanner.SetActive(false);
        CheckControllers();   
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CheckPositions();
    }

    void CheckControllers()
    {
        int count = Gamepad.all.Count;
        if (count == 0)
        {
            Debug.Log("Add controllers missing message");
            c1Image.SetActive(false);
            c2Image.SetActive(false);
        }
        else if (count == 1)
        {
            c2Image.SetActive(false);
            firstGamepad = Gamepad.all[0];
            isTwoGamepads = false;
        }
        else if (count >= 2)
        {
            firstGamepad = Gamepad.all[0];
            secondGamepad = Gamepad.all[1];
            isTwoGamepads = true;
        }
    }

    void GetInputs()
    {
        if (isTwoGamepads)
        {
            //--First GAMEPAD Input--

            Gamepad currentGamePad = firstGamepad;       

            float yInput = currentGamePad.leftStick.ReadValue().y;

            if (yInput >= 0.9f && !p1Set)
            {
                c1Image.transform.SetParent(p1Panel.transform);
            }
            else if (yInput <= -0.9f && !p2Set)
            {
                c1Image.transform.SetParent(p2Panel.transform);
            }
            else if (currentGamePad.buttonEast.wasPressedThisFrame)
            {
                c1Image.transform.SetParent(nullPanel.transform);
            }

            if (readyToStart && currentGamePad.startButton.wasPressedThisFrame)
            {
                StartPressed();
            }


            //--Second GAMEPAD Input--

            currentGamePad = secondGamepad;

            yInput = currentGamePad.leftStick.ReadValue().y;

            if (yInput >= 0.9f && !p1Set)
            {
                c2Image.transform.SetParent(p1Panel.transform);
            }
            else if (yInput <= -0.9f && !p2Set)
            {
                c2Image.transform.SetParent(p2Panel.transform);
            }
            else if (currentGamePad.buttonEast.wasPressedThisFrame)
            {
                c2Image.transform.SetParent(nullPanel.transform);
            }

            if (readyToStart && currentGamePad.startButton.wasPressedThisFrame)
            {
                StartPressed();
            }

        }
        else
        {
            //--First GAMEPAD Input--

            Gamepad currentGamePad = firstGamepad;

            float yInput = currentGamePad.leftStick.ReadValue().y;

            if (yInput >= 0.9f && !p1Set)
            {
                c1Image.transform.SetParent(p1Panel.transform);
            }
            else if (yInput <= -0.9f && !p2Set)
            {
                c1Image.transform.SetParent(p2Panel.transform);
            }
            else if (currentGamePad.buttonEast.wasPressedThisFrame)
            {
                c1Image.transform.SetParent(nullPanel.transform);
            }

            if (readyToStart && currentGamePad.startButton.wasPressedThisFrame)
            {
                StartPressed();
            }
        }

        //--KEYBOARD Input--
        if (Input.GetKeyDown(KeyCode.W) && !p1Set)
        {
            keyboardImage.transform.SetParent(p1Panel.transform);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !p2Set)
        {
            keyboardImage.transform.SetParent(p2Panel.transform);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            keyboardImage.transform.SetParent(nullPanel.transform);
        }

        if (readyToStart && Input.GetKeyDown(KeyCode.E))
        {
            StartPressed();
        }
    }

    void CheckPositions()
    {
        if (p1Panel.transform.childCount > 0)
        {
            p1Set = true;
        }
        else
        {
            p1Set = false;
        }

        if (p2Panel.transform.childCount > 0)
        {
            p2Set = true;
        }
        else
        {
            p2Set = false;
        }

        if (p1Set && p2Set)
        {
            readyToStart = true;
            readyBanner.SetActive(true);
        }
        else
        {
            readyToStart = false;
            readyBanner.SetActive(false);
        }
    }

    void StartPressed()
    {
        string p1String = p1Panel.transform.GetChild(0).name;
        string p2String = p2Panel.transform.GetChild(0).name;
        Debug.Log(p1String);
        Debug.Log(p2String);
        PlayerPrefs.SetString("Player1Controller", p1String);
        PlayerPrefs.SetString("Player2Controller", p2String);
        SceneManager.LoadScene("CharSelect");
    }
}
