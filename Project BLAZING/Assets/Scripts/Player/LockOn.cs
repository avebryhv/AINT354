using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public CoreFinder finder;

    public bool isLockedOn;
    public GameObject lockedTarget;
    public float maxLockDistance;
    PlayerMovement playerMovement;

    public bool testCanLockOn;
    public GameObject otherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        playerMovement = finder.playerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLockOn();        
        if (isLockedOn)
        {
            finder.mainUI.UpdateLockDistance(GetLockedDistance(lockedTarget.transform.position));
        }
    }

    public void LockButtonPressed()
    {
        if (testCanLockOn)
        {
            if (isLockedOn)
            {
                EndLock();
            }
            else
            {
                GetClosest();
            }
        }
        
    }

    void GetClosest()
    {
        //GameObject closest;
        //closest = finder.enemyList.ReturnClosest(finder.player.transform.position);
        if (GetLockedDistance(otherPlayer.transform.position) <= maxLockDistance)
        {
            if (otherPlayer.GetComponent<PlayerMovement>().targetable)
            {
                isLockedOn = true;
                lockedTarget = otherPlayer;
                StartLock(lockedTarget);
            }
            
        }
    }

    void CheckLockOn()
    {
        if (lockedTarget == null || GetLockedDistance(lockedTarget.transform.position) > maxLockDistance || !lockedTarget.GetComponent<PlayerMovement>().targetable)
        {
            EndLock();
        }
    }

    float GetLockedDistance(Vector3 other)
    {
        float dist;
        dist = Mathf.Abs(Vector3.Distance(other, finder.player.transform.position));
        return dist;
    }

    public void StartLock(GameObject toLock)
    {
        isLockedOn = true;
        lockedTarget = toLock;
        finder.player.GetComponent<PlayerMovement>().SetLockOn(lockedTarget);
        finder.lockOnSquare.CreateNewLockOn(lockedTarget, finder.playerCam);
        finder.mainUI.SetLockOn(lockedTarget);
    }

    public void EndLock()
    {
        lockedTarget = null;
        isLockedOn = false;
        finder.player.GetComponent<PlayerMovement>().RemoveLock();
        finder.lockOnSquare.RemoveLockOnSquare();
        finder.mainUI.EndLockOn();
    }

    public Vector3 ReturnTargetBehind()
    {
        float playerWidth = playerMovement.playerWidth;
        float dist = playerWidth * 3;
        if (lockedTarget != null)
        {
            Vector3 toReturn = lockedTarget.transform.position;
            toReturn -= lockedTarget.transform.forward * dist;
            return toReturn;
        }
        else
        {
            return new Vector3();
        }
    }

    public Vector3 ReturnTargetHorizontalPoint(int location)
    {
        float dist = 1;
        if (lockedTarget != null)
        {
            Vector3 toReturn = lockedTarget.transform.position;
            switch (location)
            {
                case 0: //Return Middle                  
                    toReturn += lockedTarget.transform.forward * dist;
                    break;
                case 1: //Return Left
                    toReturn += -lockedTarget.transform.right * dist + lockedTarget.transform.forward * dist * 2;
                    break;
                case -1: //Return Right
                    toReturn += lockedTarget.transform.right * dist + lockedTarget.transform.forward * dist * 2;
                    break;
                default:
                    break;
            }
            
            return toReturn;
        }
        else
        {
            return finder.player.transform.position;
        }
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
