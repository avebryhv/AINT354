using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public CoreFinder finder;
    PlayerMovement playerMovement;

    public float cameraSensitivity;
    public bool usingController;

    public Gamepad gamepad;

    public void SetGamePadNum(int num)
    {
        if (Gamepad.all.Count >= num)
        {
            gamepad = Gamepad.all[num - 1];
            usingController = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            usingController = false;
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        playerMovement = finder.playerMovement;
        //gamepad = Gamepad.current;
        //string[] attachedControllers = Input.GetJoystickNames();
        //for (int i = 0; i < attachedControllers.Length; i++)
        //{
        //    Debug.Log("Controller " + i + " " + attachedControllers[i]);
        //}
        if (finder.playerMovement.isPlayer1)
        {
            Debug.Log("Connected Gamepads");
            Debug.Log(Gamepad.all.Count);
            List<string> gamePadNames = new List<string>();
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                gamePadNames.Add(Gamepad.all[i].name);
                Debug.Log(Gamepad.all[i].name);
            }
            Debug.Log("Unsupported Gamepads");
            
            Debug.Log(InputSystem.GetUnsupportedDevices().Count);
        }
        
        //gamepad = Gamepad.all[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (usingController) //---------------------------------CONTROLLER CONTROLS--------------------------------------
        {
            if (!GameFunctions.isPaused)
            {
                SendAxisInput();

                if (gamepad.leftShoulder.wasPressedThisFrame)
                {
                    finder.playerMovement.EvadePressed();                    
                }

                if (gamepad.buttonSouth.wasPressedThisFrame)
                {
                    finder.playerMovement.EvadePressed();
                }

                if (/*Input.GetButtonDown(evadeButton)*/gamepad.leftTrigger.wasPressedThisFrame)
                {
                    //finder.playerMovement.EvadePressed();
                    finder.playerMovement.BoostButtonPressed();
                }

                if (gamepad.leftTrigger.wasReleasedThisFrame)
                {
                    finder.playerMovement.BoostButtonReleased();
                }

                //if (/*Input.GetButtonDown(specialButton)*/gamepad.bButton.wasPressedThisFrame)
                //{
                //    if (finder.playerHealth.ReturnCanUseSpecial())
                //    {
                //        switch (finder.playerMovement.type)
                //        {
                //            case PlayerMovement.mechType.Normal:
                //                finder.playerGun.ShieldButtonPressed();
                //                finder.playerHealth.ResetSpecialCharge();
                //                break;
                //            case PlayerMovement.mechType.Fast:
                //                finder.playerMovement.CamoButtonPressed();
                //                finder.playerHealth.ResetSpecialCharge();
                //                break;
                //            case PlayerMovement.mechType.Slow:
                //                if (finder.lockOn.isLockedOn)
                //                {
                //                    finder.playerGun.MissileBarragePressed();
                //                    finder.playerHealth.ResetSpecialCharge();
                //                }
                //                break;
                //            default:
                //                break;
                //        }

                //    }

                //}

                if (gamepad.rightTrigger.isPressed)
                {
                    finder.playerGun.FireButtonPressed();
                    finder.playerGun2.FireButtonPressed();
                }

                if (gamepad.rightShoulder.wasPressedThisFrame)
                {
                    finder.playerGun.MissileButtonPressed();
                }                

                if (gamepad.rightStickButton.wasPressedThisFrame)
                {
                    finder.playerGun.MissileButtonPressed();
                }

                if (gamepad.buttonNorth.wasPressedThisFrame)
                {
                    finder.playerGun.MissileButtonPressed();
                }
            }
            

            if (gamepad.startButton.wasPressedThisFrame)
            {
                GameFunctions.PauseButtonPressed();
            }
        }
        else //---------------------------------------KEYBOARD CONTROLS------------------------------------------
        {
            if (!GameFunctions.isPaused)
            {
                SendAxisInputKeyboard();
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    finder.playerMovement.BoostButtonPressed();
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    finder.playerMovement.BoostButtonReleased();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    finder.playerMovement.EvadePressed();
                }

                //if (Input.GetKeyDown(KeyCode.Q))
                //{
                //    if (finder.playerHealth.ReturnCanUseSpecial())
                //    {
                //        switch (finder.playerMovement.type)
                //        {
                //            case PlayerMovement.mechType.Normal:
                //                finder.playerGun.ShieldButtonPressed();
                //                finder.playerHealth.ResetSpecialCharge();
                //                break;
                //            case PlayerMovement.mechType.Fast:
                //                finder.playerMovement.CamoButtonPressed();
                //                finder.playerHealth.ResetSpecialCharge();
                //                break;
                //            case PlayerMovement.mechType.Slow:
                //                if (finder.lockOn.isLockedOn)
                //                {
                //                    finder.playerGun.MissileBarragePressed();
                //                    finder.playerHealth.ResetSpecialCharge();
                //                }
                //                break;
                //            default:
                //                break;
                //        }

                //    }

                //}

                if (Input.GetMouseButton(1))
                {
                    finder.playerGun.FireButtonPressed();
                }

                if (Input.GetMouseButton(0))
                {
                    finder.playerGun2.FireButtonPressed();
                }

                if (Input.GetMouseButtonDown(2))
                {
                    finder.playerGun.MissileButtonPressed();
                }
            }
            
        }

    }

    void SendAxisInput()
    {
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        float hori = leftStick.x;
        float verti = leftStick.y;
        //float camAxis = gamepad.rightStick.ReadValue().x * cameraSensitivity;
        float camAxis = Mathf.Pow(gamepad.rightStick.ReadValue().x, 2) * Mathf.Sign(gamepad.rightStick.ReadValue().x);
        finder.playerMovement.RecieveAxisInput(hori, verti, camAxis);
    }

    void SendAxisInputKeyboard()
    {
        float hori;
        if (Input.GetKey(KeyCode.A))
        {
            hori = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hori = 1;
        }
        else
        {
            hori = 0;
        }

        float verti;
        if (Input.GetKey(KeyCode.S))
        {
            verti = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verti = 1;
        }
        else
        {
            verti = 0;
        }

        float cam;
        cam = Input.GetAxis("Mouse X") * 2;
        

        finder.playerMovement.RecieveAxisInput(hori, verti, cam);
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }

    void SendStickMovement()
    {

    }

    public void SetInputString(bool isP1)
    {
        if (isP1)
        {            
            SetGamePadNum(1);
        }
        else
        {            
            SetGamePadNum(2);
        }
    }
}
