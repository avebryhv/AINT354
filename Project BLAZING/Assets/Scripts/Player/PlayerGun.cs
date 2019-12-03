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

    public float accuracy;

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

    public void MissileButtonPressed()
    {
        if (lockOn.isLockedOn)
        {
            //MissileBarrage(lockOn.lockedTarget, missileBarrageCount);
            Missile(lockOn.softLockTarget);
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

    void CoolDown()
    {
        firing = false;
    }

    void Missile(GameObject target)
    {
        if (currentMissiles > 0)
        {
            GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            newMissile.GetComponent<Missile>().FireMissile(target, transform.forward, finder.playerMovement.isPlayer1);
            currentMissiles--;
            finder.mainUI.UpdateMissileCounter(currentMissiles);
        }
        
    }

    public void MissileBarragePressed()
    {
        if (lockOn.isLockedOn)
        {
            MissileBarrage(lockOn.softLockTarget, 9);
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
            missiles[i].GetComponent<Missile>().FireMissile(target, toLookAt, 1f, finder.playerMovement.isPlayer1);
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
                //bulletPrefab = normalBullet;
                fireTime = 0.4f;
                SetMaxMissiles(10);
                accuracy = 1;
                break;
            case PlayerMovement.mechType.Fast:
                //bulletPrefab = fastBullet;
                fireTime = 0.1f;
                SetMaxMissiles(5);
                accuracy = 3;
                break;
            case PlayerMovement.mechType.Slow:
                //bulletPrefab = slowBullet;
                fireTime = 0.25f;
                SetMaxMissiles(20);
                accuracy = 0.1f;
                break;
            default:
                break;
        }
    }

    public void ShieldButtonPressed()
    {
        CreateShield();
    }

    void CreateShield()
    {
        finder.playerHealth.canSpecialCharge = false;
        shieldObject.SetActive(true);
        shieldUp = true;
        Invoke("HideShield", 5.0f);
    }

    void HideShield()
    {
        finder.playerHealth.canSpecialCharge = true;
        shieldObject.SetActive(false);
        shieldUp = false;
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
