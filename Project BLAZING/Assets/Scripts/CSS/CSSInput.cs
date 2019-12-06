using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CSSInput : MonoBehaviour
{

    Gamepad gamePad1;
    Gamepad gamePad2;

    public CSSCursor cursor1;
    public CSSCursor cursor2;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Gamepad.all.Count < 2)
        {
            Debug.Log("Connect Two Controllers");
        }
        else
        {
            gamePad1 = Gamepad.all[0];
            gamePad2 = Gamepad.all[1];
        }
    }

    void GetPlayer1Input()
    {
        Gamepad currentGamePad = gamePad1;
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

    }

    void GetPlayer2Input()
    {
        Gamepad currentGamePad = gamePad2;
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

    }
}
