using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public CoreFinder finder;
    LockOn lockOn;
    public GameObject bulletPrefab;
    public float fireTime;
    bool firing;

    public GameObject fastBullet;
    public GameObject normalBullet;
    public GameObject slowBullet;

    //Missile Data for testing
    public GameObject missilePrefab;
    public int missileBarrageCount;
    public int maxMissiles;
    int currentMissiles;

    //Shield Data
    public GameObject shieldObject;
    bool shieldUp;

    

    // Use this for initialization
    void Start()
    {
        firing = false;
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        lockOn = finder.lockOn;
        currentMissiles = maxMissiles;
        finder.mainUI.UpdateMissileCounter(currentMissiles);
        shieldUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && !firing && !finder.playerMovement.inSpecialMovement)
        {
            Fire();
        }

        if (Input.GetButtonDown("Missile"))
        {
            if (lockOn.isLockedOn)
            {
                //MissileBarrage(lockOn.lockedTarget, missileBarrageCount);
                Missile(lockOn.lockedTarget);
            }
        }


    }

    void Fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
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

    void Missile(GameObject target)
    {
        if (currentMissiles > 0)
        {
            GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            newMissile.GetComponent<Missile>().FireMissile(target, transform.forward);
            currentMissiles--;
            finder.mainUI.UpdateMissileCounter(currentMissiles);
        }
        
    }

    public void MissileBarragePressed()
    {
        if (lockOn.isLockedOn)
        {
            MissileBarrage(lockOn.lockedTarget, 9);
        }
    }

    void MissileBarrage(GameObject target, int amount)
    {
        Vector3 dir = new Vector3(); ;
        GameObject[] missiles = new GameObject[amount];
        float deltaDir = 1.0f / (float)amount;
        for (int i = 0; i < missiles.Length; i++)
        {
            dir = new Vector3((-0.5f + deltaDir * i), 0, 2);
            missiles[i] = Instantiate(missilePrefab, transform.position, transform.rotation);
            Vector3 toLookAt = new Vector3();
            toLookAt += dir.x * transform.right;
            toLookAt += dir.y * transform.up;
            toLookAt += dir.z * transform.forward;
            Debug.Log(toLookAt);
            missiles[i].GetComponent<Missile>().FireMissile(target, toLookAt, 1f);
        }
    }

    void SetMaxMissiles(int amount)
    {
        maxMissiles = amount;
        currentMissiles = maxMissiles;
        finder.mainUI.UpdateMissileCounter(currentMissiles);
    }

    public void SetStats(PlayerMovement.mechType t)
    {
        switch (t)
        {
            case PlayerMovement.mechType.Normal:
                bulletPrefab = normalBullet;
                fireTime = 0.2f;
                SetMaxMissiles(5);
                break;
            case PlayerMovement.mechType.Fast:
                bulletPrefab = fastBullet;
                fireTime = 0.15f;
                SetMaxMissiles(2);
                break;
            case PlayerMovement.mechType.Slow:
                bulletPrefab = slowBullet;
                fireTime = 0.25f;
                SetMaxMissiles(10);
                break;
            default:
                break;
        }
    }

    public void ShieldButtonPressed()
    {
        if (shieldUp)
        {
            HideShield();
        }
        else
        {
            CreateShield();
        }
    }

    void CreateShield()
    {
        shieldObject.SetActive(true);
        shieldUp = true;
    }

    void HideShield()
    {
        shieldObject.SetActive(false);
        shieldUp = false;
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
