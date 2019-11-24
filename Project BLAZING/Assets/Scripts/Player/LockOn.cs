using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    CoreFinder finder;

    public bool isLockedOn;
    public GameObject lockedTarget;
    public float maxLockDistance;

    // Start is called before the first frame update
    void Start()
    {
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLockOn();
        if (Input.GetButtonDown("Fire2"))
        {
            LockButtonPressed();
        }
        if (isLockedOn)
        {
            finder.mainUI.UpdateLockDistance(GetLockedDistance(lockedTarget.transform.position));
        }
    }

    public void LockButtonPressed()
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

    void GetClosest()
    {
        GameObject closest;
        closest = finder.enemyList.ReturnClosest(finder.player.transform.position);
        if (GetLockedDistance(closest.transform.position) <= maxLockDistance)
        {
            isLockedOn = true;
            lockedTarget = closest;
            StartLock(lockedTarget);
        }
    }

    void CheckLockOn()
    {
        if (lockedTarget == null || GetLockedDistance(lockedTarget.transform.position) > maxLockDistance)
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
        finder.mainUI.SetLockOn(lockedTarget);
    }

    public void EndLock()
    {
        lockedTarget = null;
        isLockedOn = false;
        finder.player.GetComponent<PlayerMovement>().RemoveLock();
        finder.mainUI.EndLockOn();
    }
}
