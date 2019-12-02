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

    public Vector3 targetViewPos;

    //--Soft LockOn Variables--
    public bool isSoftLocked;
    public GameObject softLockTarget;
    public float timeSoftLocked;
    public float softLockActiveTime;
    public LayerMask cancelLockMask;

    // Start is called before the first frame update
    void Start()
    {
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        playerMovement = finder.playerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSoftLock();
        if (isSoftLocked)
        {
            timeSoftLocked += Time.deltaTime;
            if (timeSoftLocked >= softLockActiveTime)
            {
                finder.lockOnSquare.UpdateColour(true);
                isLockedOn = true;
            }
            CalculateLockOnBoxSize();
        }
        //CheckLockOn();        
        //if (isLockedOn)
        //{
        //    finder.mainUI.UpdateLockDistance(GetLockedDistance(lockedTarget.transform.position));
        //}
    }

    //public void LockButtonPressed()
    //{
    //    if (testCanLockOn)
    //    {
    //        if (isLockedOn)
    //        {
    //            EndLock();
    //        }
    //        else
    //        {
    //            CheckIfCanLockOn();
    //        }
    //    }
        
    //}

    //void CheckIfCanLockOn()
    //{
    //    //GameObject closest;
    //    //closest = finder.enemyList.ReturnClosest(finder.player.transform.position);
    //    if (GetLockedDistance(otherPlayer.transform.position) <= maxLockDistance)
    //    {
    //        targetViewPos = finder.playerCam.WorldToViewportPoint(otherPlayer.transform.position);
    //        if (targetViewPos.x > 0.2f && targetViewPos.x < 0.8f)
    //        {
    //            if (otherPlayer.GetComponent<PlayerMovement>().targetable)
    //            {
    //                isLockedOn = true;
    //                lockedTarget = otherPlayer;
    //                StartLock(lockedTarget);
    //            }
    //        }
            
            
    //    }
    //}

    void CheckSoftLock()
    {
        if (otherPlayer != null)
        {
            if (GetLockedDistance(otherPlayer.transform.position) <= maxLockDistance)
            {
                targetViewPos = finder.playerCam.WorldToViewportPoint(otherPlayer.transform.position);
                if (targetViewPos.x >= 0.2f && targetViewPos.x <= 0.8f)
                {
                    if (otherPlayer.GetComponent<PlayerMovement>().targetable)
                    {
                        Vector3 dirtoTarget = otherPlayer.transform.position - transform.position;
                        if (Vector3.Dot(dirtoTarget, transform.forward) > 0)
                        {
                            RaycastHit hit;
                            if (!Physics.Raycast(transform.position, dirtoTarget, out hit, GetLockedDistance(otherPlayer.transform.position), cancelLockMask))
                            {
                                if (!isSoftLocked)
                                {
                                    StartSoftLock();
                                }
                            }
                            else
                            {
                                EndSoftLock();
                            }
                            
                        }

                    }
                    else if (isSoftLocked)
                    {
                        EndSoftLock();
                    }
                }
                else if (isSoftLocked)
                {
                    EndSoftLock();
                }


            }
            else if (isSoftLocked)
            {
                EndSoftLock();
            }
        }
        else
        {
            EndSoftLock();
        }
        
    }

    void StartSoftLock()
    {
        isSoftLocked = true;
        softLockTarget = otherPlayer;
        finder.lockOnSquare.CreateNewLockOn(otherPlayer, finder.playerCam);
    }

    void EndSoftLock()
    {
        isSoftLocked = false;
        softLockTarget = null;
        finder.lockOnSquare.RemoveLockOnSquare();
        timeSoftLocked = 0;
        finder.lockOnSquare.UpdateColour(false);
        isLockedOn = false;
    }

    void CalculateLockOnBoxSize()
    {
        float otherDist = GetLockedDistance(softLockTarget.transform.position);
        float distPercent = otherDist / maxLockDistance;
        float widthP = 80.0f * (1 -distPercent);
        finder.lockOnSquare.UpdateSquareSize(widthP + 20);
    }

    //void CheckLockOn()
    //{
    //    if (lockedTarget == null || GetLockedDistance(lockedTarget.transform.position) > maxLockDistance || !lockedTarget.GetComponent<PlayerMovement>().targetable)
    //    {
    //        EndLock();
    //    }
    //}

    float GetLockedDistance(Vector3 other)
    {
        float dist;
        dist = Mathf.Abs(Vector3.Distance(other, finder.player.transform.position));
        return dist;
    }

    //public void StartLock(GameObject toLock)
    //{
    //    isLockedOn = true;
    //    lockedTarget = toLock;
    //    finder.player.GetComponent<PlayerMovement>().SetLockOn(lockedTarget);
    //    finder.lockOnSquare.CreateNewLockOn(lockedTarget, finder.playerCam);
    //    finder.mainUI.SetLockOn(lockedTarget);
    //}

    //public void EndLock()
    //{
    //    lockedTarget = null;
    //    isLockedOn = false;
    //    finder.player.GetComponent<PlayerMovement>().RemoveLock();
    //    finder.lockOnSquare.RemoveLockOnSquare();
    //    finder.mainUI.EndLockOn();
    //}

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
