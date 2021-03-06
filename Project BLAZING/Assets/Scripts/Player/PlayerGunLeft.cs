﻿using System.Collections;
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

    public enum FireType { Rifle, Shotgun, Rocket, None}
    public FireType type;
    public GameObject fastBullet;
    public GameObject normalBullet;
    public GameObject slowBullet;

    public int shotgunBulletCount;

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
            switch (type)
            {
                case FireType.Rifle:
                    ShootRifle();
                    break;
                case FireType.Shotgun:
                    ShootShotgun();
                    break;
                case FireType.Rocket:
                    ShootRocket();
                    break;
                default:
                    break;
            }
            
        }
    }

   

    void Fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        float randomX = Random.Range(-accuracy, accuracy);
        float randomY = Random.Range(-accuracy, accuracy);
        float randomZ = Random.Range(-accuracy, accuracy);
        newBullet.transform.Rotate(new Vector3(0, randomY, 0));
        newBullet.GetComponent<PlayerGunBullet>().SetParent(finder.playerMovement.isPlayer1);                
        if (finder.lockOn.isLockedOn)
        {
            newBullet.GetComponent<PlayerGunBullet>().SetCurving(finder.lockOn.softLockTarget);
        }
        firing = true;
        Invoke("CoolDown", fireTime);
    }

    void ShootRifle()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        float randomX = Random.Range(-accuracy, accuracy);
        float randomY = Random.Range(-accuracy, accuracy);
        float randomZ = Random.Range(-accuracy, accuracy);
        newBullet.transform.Rotate(new Vector3(0, randomY, 0));
        newBullet.GetComponent<PlayerGunBullet>().SetParent(finder.playerMovement.isPlayer1);
        if (finder.lockOn.isLockedOn)
        {
            newBullet.GetComponent<PlayerGunBullet>().SetCurving(finder.lockOn.softLockTarget);
        }
        firing = true;
        Invoke("CoolDown", fireTime);
    }

    void ShootShotgun()
    {
        for (int i = 0; i < shotgunBulletCount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            float randomX = Random.Range(-accuracy, accuracy);
            float randomY = Random.Range(-accuracy, accuracy);
            float randomZ = Random.Range(-accuracy, accuracy);
            newBullet.transform.Rotate(new Vector3(0, randomY, 0));
            newBullet.GetComponent<PlayerGunBullet>().SetKillTime(0.3f);
            newBullet.GetComponent<PlayerGunBullet>().SetParent(finder.playerMovement.isPlayer1);            
        }
        firing = true;
        Invoke("CoolDown", fireTime);
    }

    void ShootRocket()
    {

    }

    void CoolDown()
    {
        firing = false;
    }


    public void SetStats(PlayerMovement.mechType t)
    {
        switch (t)
        {
            case PlayerMovement.mechType.Normal: //Rifle
                bulletPrefab = normalBullet;
                fireTime = 0.4f;
                accuracy = 1;
                type = FireType.Rifle;
                break;
            case PlayerMovement.mechType.Fast: //Shotgun
                bulletPrefab = fastBullet;
                fireTime = 1f;
                accuracy = 5;
                type = FireType.Shotgun;
                shotgunBulletCount = 5;
                break;
            case PlayerMovement.mechType.Slow: //Flamethrower
                bulletPrefab = slowBullet;
                fireTime = 2f;
                accuracy = 0.5f;
                type = FireType.None;
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
