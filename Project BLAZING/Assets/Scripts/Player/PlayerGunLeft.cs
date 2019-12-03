using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLeft : MonoBehaviour
{
    public CoreFinder finder;
    LockOn lockOn;
    public GameObject bulletPrefab;
    public float fireTime;
    bool firing;

    public float accuracy;

    public GameObject fastBullet;
    public GameObject normalBullet;
    public GameObject slowBullet;

    

    // Use this for initialization
    void Start()
    {
        firing = false;
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        lockOn = finder.lockOn;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButton("Shoot") && !firing && !finder.playerMovement.inSpecialMovement)
        //{
        //    Fire();
        //}

        //if (Input.GetButtonDown("Missile"))
        //{
        //    if (lockOn.isLockedOn)
        //    {
        //        //MissileBarrage(lockOn.lockedTarget, missileBarrageCount);
        //        Missile(lockOn.lockedTarget);
        //    }
        //}


    }

    public void FireButtonPressed()
    {
        if (!firing && !finder.playerMovement.inSpecialMovement)
        {
            Fire();
        }
    }

   

    void Fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        float randomX = Random.Range(-accuracy, accuracy);
        float randomY = Random.Range(-accuracy, accuracy);
        float randomZ = Random.Range(-accuracy, accuracy);
        newBullet.transform.Rotate(new Vector3(randomX, randomY, randomZ));
        newBullet.GetComponent<PlayerGunBullet>().SetParent(finder.playerMovement.isPlayer1);
        if (finder.lockOn.isLockedOn)
        {
            newBullet.GetComponent<PlayerGunBullet>().SetCurving(finder.lockOn.softLockTarget);
        }
        firing = true;
        Invoke("CoolDown", fireTime);
    }

    void CoolDown()
    {
        firing = false;
    }


    public void SetStats(PlayerMovement.mechType t)
    {
        switch (t)
        {
            case PlayerMovement.mechType.Normal:
                //bulletPrefab = normalBullet;
                fireTime = 0.4f;
                accuracy = 1;
                break;
            case PlayerMovement.mechType.Fast:
                //bulletPrefab = fastBullet;
                fireTime = 0.1f;
                accuracy = 3;
                break;
            case PlayerMovement.mechType.Slow:
                //bulletPrefab = slowBullet;
                fireTime = 0.25f;
                accuracy = 0.1f;
                break;
            default:
                break;
        }
    }



    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
