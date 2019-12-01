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
        if (lockOn.isLockedOn)
        {
            newBullet.GetComponent<PlayerGunBullet>().SetCurving(lockOn.lockedTarget);
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
                bulletPrefab = normalBullet;
                fireTime = 0.2f;
                break;
            case PlayerMovement.mechType.Fast:
                bulletPrefab = fastBullet;
                fireTime = 0.3f;
                break;
            case PlayerMovement.mechType.Slow:
                bulletPrefab = slowBullet;
                fireTime = 0.25f;
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
