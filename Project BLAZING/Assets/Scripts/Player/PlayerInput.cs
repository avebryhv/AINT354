using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CoreFinder finder;


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
            if (finder.playerHealth.ReturnCanUseSpecial())
            {
                switch (finder.playerMovement.type)
                {
                    case PlayerMovement.mechType.Normal:
                        finder.playerGun.ShieldButtonPressed();
                        break;
                    case PlayerMovement.mechType.Fast:
                        finder.playerMovement.CamoButtonPressed();
                        break;
                    case PlayerMovement.mechType.Slow:
                        finder.playerGun.MissileBarragePressed();
                        break;
                    default:
                        break;
                }
                finder.playerHealth.ResetSpecialCharge();
            }
            
        }
    }
    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
