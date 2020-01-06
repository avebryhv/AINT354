using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public CoreFinder finder;
    PlayerMovement playerMovement;

    public float cameraSensitivity;
    public bool usingController;

    public Gamepad gamepad;

    public void SetGamePadNum(int num)
    {
        if (num == 1)
        {            
            string controllerString = PlayerPrefs.GetString("Player1Controller");
            switch (controllerString)
            {
                case "Controller1":
                    gamepad = Gamepad.all[0];
                    usingController = true;
                    break;
                case "Controller2":
                    gamepad = Gamepad.all[1];
                    usingController = true;
                    break;
                case "Keyboard":
                    usingController = false;
                    break;
                default:
                    break;
            }
        }
        else
        {
            string controllerString = PlayerPrefs.GetString("Player2Controller");
            switch (controllerString)
            {
                case "Controller1":
                    gamepad = Gamepad.all[0];
                    usingController = true;
                    break;
                case "Controller2":
                    gamepad = Gamepad.all[1];
                    usingController = true;
                    break;
                case "Keyboard":
                    usingController = false;
                    break;
                default:
                    break;
            }
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
            //Debug.Log("Connected Gamepads");
            //Debug.Log(Gamepad.all.Count);
            List<string> gamePadNames = new List<string>();
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                gamePadNames.Add(Gamepad.all[i].name);
                //Debug.Log(Gamepad.all[i].name);
            }
            //Debug.Log("Unsupported Gamepads");
            
           //Debug.Log(InputSystem.GetUnsupportedDevices().Count);
        }


        if (playerMovement.isPlayer1)
        {
            if (PlayerPrefs.HasKey("P1Sensitive"))
            {
                cameraSensitivity = PlayerPrefs.GetFloat("P1Sensitive");
                //Debug.Log("p1 sens loaded " + cameraSensitivity);
            }
            else
            {
                cameraSensitivity = 1;
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("P2Sensitive"))
            {
                cameraSensitivity = PlayerPrefs.GetFloat("P2Sensitive");
            }
            else
            {
                cameraSensitivity = 1;
            }
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

                if (gamepad.rightTrigger.isPressed)
                {
                    finder.playerGun.FireButtonPressed();
                    finder.playerGun2.FireButtonPressed();
                }

                if (gamepad.rightShoulder.wasPressedThisFrame)
                {
                    finder.shoulderWeapon.ShoulderWeaponPressed();
                }

                //if (gamepad.rightShoulder.ReadValue() == 1)
                //{
                //    finder.shoulderWeapon.ShoulderWeaponHeld();
                //}
                //else
                //{
                //    finder.shoulderWeapon.ShoulderWeaponReleased();
                //}

                if (gamepad.rightStickButton.wasPressedThisFrame)
                {
                    finder.shoulderWeapon.ShoulderWeaponPressed();
                }

                if (gamepad.buttonNorth.wasPressedThisFrame)
                {
                    finder.shoulderWeapon.ShoulderWeaponPressed();
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

                if (Input.GetMouseButton(1))
                {
                    
                }

                if (Input.GetMouseButton(0))
                {
                    finder.playerGun2.FireButtonPressed();
                    finder.playerGun.FireButtonPressed();
                }

                if (Input.GetMouseButtonDown(2))
                {
                    finder.shoulderWeapon.ShoulderWeaponPressed();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameFunctions.PauseButtonPressed();
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
        float camAxis = Mathf.Pow(gamepad.rightStick.ReadValue().x, 2) * cameraSensitivity * Mathf.Sign(gamepad.rightStick.ReadValue().x);
        finder.playerMovement.RecieveAxisInput(hori, verti, camAxis);
    }

    public void SetCameraSensitivity(Slider sl)
    {
        if (playerMovement != null)
        {
            cameraSensitivity = sl.value;
            if (playerMovement.isPlayer1)
            {
                PlayerPrefs.SetFloat("P1Sensitive", sl.value);
            }
            else
            {
                PlayerPrefs.SetFloat("P2Sensitive", sl.value);
            }
        }
        
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

    public void Rumble(float t)
    {
        if (usingController)
        {
            gamepad.SetMotorSpeeds(0.25f, 0.75f);
            Invoke("RumbleOff", t);
        }
        
    }

    public void RumbleOff()
    {
        gamepad.SetMotorSpeeds(0, 0);
    }
}
