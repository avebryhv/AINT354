using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CSSInput : MonoBehaviour
{

    Gamepad player1GamePad;
    Gamepad player2GamePad;

    public CSSCursor cursor1;
    public CSSCursor cursor2;

    bool p1UsingKeyboard;
    bool p2UsingKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        p1UsingKeyboard = false;
        p2UsingKeyboard = false;
        AssignGamepads();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayer1Input();
        GetPlayer2Input();
    }

    void AssignGamepads()
    {       

        string p1String = PlayerPrefs.GetString("Player1Controller");
        string p2String = PlayerPrefs.GetString("Player2Controller");

        switch (p1String)
        {
            case "Controller1":
                player1GamePad = Gamepad.all[0];
                p1UsingKeyboard = false;
                break;
            case "Controller2":
                player1GamePad = Gamepad.all[1];
                p1UsingKeyboard = false;
                break;
            case "Keyboard":
                p1UsingKeyboard = true;
                break;
            default:
                break;
        }

        switch (p2String)
        {
            case "Controller1":
                player2GamePad = Gamepad.all[0];
                p2UsingKeyboard = false;
                break;
            case "Controller2":
                player2GamePad = Gamepad.all[1];
                p2UsingKeyboard = false;
                break;
            case "Keyboard":
                p2UsingKeyboard = true;
                break;
            default:
                break;
        }
    }

    void GetPlayer1Input()
    {
        if (p1UsingKeyboard)
        {
            CSSCursor currentCursor = cursor1;
            Vector2 cursorPos = Input.mousePosition;
            currentCursor.RecieveNewPosition(cursorPos);

            if (Input.GetMouseButtonDown(0))
            {
                currentCursor.ClickPressed();
            }
            if (Input.GetMouseButtonDown(1))
            {
                currentCursor.BackPressed();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<CSS>().StartPressed();
            }
        }
        else
        {
            Gamepad currentGamePad = player1GamePad;
            CSSCursor currentCursor = cursor1;

            Vector2 cursorInput = currentGamePad.leftStick.ReadValue();
            currentCursor.RecieveMovement(cursorInput);

            if (currentGamePad.buttonSouth.wasPressedThisFrame)
            {
                currentCursor.ClickPressed();
            }

            if (currentGamePad.buttonEast.wasPressedThisFrame)
            {
                currentCursor.BackPressed();
            }

            if (currentGamePad.startButton.wasPressedThisFrame)
            {
                FindObjectOfType<CSS>().StartPressed();
            }
        }
        

    }

    void GetPlayer2Input()
    {
        if (p2UsingKeyboard)
        {
            CSSCursor currentCursor = cursor2;
            Vector2 cursorPos = Input.mousePosition;
            currentCursor.RecieveNewPosition(cursorPos);

            if (Input.GetMouseButtonDown(0))
            {
                currentCursor.ClickPressed();
            }
            if (Input.GetMouseButtonDown(1))
            {
                currentCursor.BackPressed();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<CSS>().StartPressed();
            }
        }
        else
        {
            Gamepad currentGamePad = player2GamePad;
            CSSCursor currentCursor = cursor2;

            Vector2 cursorInput = currentGamePad.leftStick.ReadValue();
            currentCursor.RecieveMovement(cursorInput);

            if (currentGamePad.buttonSouth.wasPressedThisFrame)
            {
                currentCursor.ClickPressed();
            }

            if (currentGamePad.buttonEast.wasPressedThisFrame)
            {
                currentCursor.BackPressed();
            }

            if (currentGamePad.startButton.wasPressedThisFrame)
            {
                FindObjectOfType<CSS>().StartPressed();
            }
        }
        

    }
}
