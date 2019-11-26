using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CoreFinder finder;


    // Start is called before the first frame update
    void Start()
    {
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LockOn"))
        {
            finder.lockOn.LockButtonPressed();
        }

        if (Input.GetButtonDown("Evade"))
        {
            finder.playerMovement.EvadePressed();
        }

        if (Input.GetButtonDown("Special"))
        {
            switch (finder.playerMovement.type)
            {
                case PlayerMovement.mechType.Normal:
                    break;
                case PlayerMovement.mechType.Fast:
                    finder.playerMovement.SpeedCamoTest();
                    break;
                case PlayerMovement.mechType.Slow:
                    finder.playerGun.MissileBarragePressed();
                    break;
                default:
                    break;
            }
        }
    }
}
