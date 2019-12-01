using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public CoreFinder finder;
    PlayerMovement playerMovement;

    public string horizontalAxis;
    public string verticalAxis;
    public string cameraAxis;
    public string shootButton;
    public string missileButton;
    public string specialButton;
    public string evadeButton;
    public string lockOnButton;


    public Gamepad gamepad;

    public void SetGamePadNum(int num)
    {
        if (Gamepad.all.Count >= num)
        {
            gamepad = Gamepad.all[num - 1];
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

        List<string> gamePadNames = new List<string>();
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            gamePadNames.Add(Gamepad.all[i].name);
            Debug.Log(Gamepad.all[i].name);
        }
        //gamepad = Gamepad.all[0];
    }

    // Update is called once per frame
    void Update()
    {
        SendAxisInput();

        //if (/*Input.GetButtonDown(lockOnButton)*/gamepad.rightStickButton.wasPressedThisFrame)
        //{
        //    finder.lockOn.LockButtonPressed();
        //}

        if (/*Input.GetButtonDown(evadeButton)*/gamepad.rightShoulder.wasPressedThisFrame)
        {
            finder.playerMovement.EvadePressed();
        }

        if (/*Input.GetButtonDown(specialButton)*/gamepad.bButton.wasPressedThisFrame)
        {
            if (finder.playerHealth.ReturnCanUseSpecial())
            {
                switch (finder.playerMovement.type)
                {
                    case PlayerMovement.mechType.Normal:
                        finder.playerGun.ShieldButtonPressed();
                        finder.playerHealth.ResetSpecialCharge();
                        break;
                    case PlayerMovement.mechType.Fast:
                        finder.playerMovement.CamoButtonPressed();
                        finder.playerHealth.ResetSpecialCharge();
                        break;
                    case PlayerMovement.mechType.Slow:
                        if (finder.lockOn.isLockedOn)
                        {
                            finder.playerGun.MissileBarragePressed();
                            finder.playerHealth.ResetSpecialCharge();
                        }                        
                        break;
                    default:
                        break;
                }
                
            }
            
        }

        if (gamepad.rightTrigger.isPressed)
        {
            finder.playerGun.FireButtonPressed();
        }

        if (gamepad.leftTrigger.isPressed)
        {
            finder.playerGun2.FireButtonPressed();
        }

        if (gamepad.yButton.wasPressedThisFrame)
        {
            finder.playerGun.MissileButtonPressed();
        }

    }

    void SendAxisInput()
    {
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        float hori = leftStick.x;
        float verti = leftStick.y;
        float camAxis = gamepad.rightStick.ReadValue().x;
        finder.playerMovement.RecieveAxisInput(hori, verti, camAxis);
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
            horizontalAxis = "Horizontal";
            verticalAxis = "Vertical";
            cameraAxis = "CameraX";
            shootButton = "Shoot";
            missileButton = "Missile";
            specialButton = "Special";
            evadeButton = "Evade";
            lockOnButton = "LockOn";
            SetGamePadNum(1);
        }
        else
        {
            horizontalAxis = "Horizontal" + "P2";
            verticalAxis = "Vertical" + "P2";
            cameraAxis = "CameraX" + "P2";
            shootButton = "Shoot" + "P2";
            missileButton = "Missile" + "P2";
            specialButton = "Special" + "P2";
            evadeButton = "Evade" + "P2";
            lockOnButton = "LockOn" + "P2";
            SetGamePadNum(2);
        }
    }
}
