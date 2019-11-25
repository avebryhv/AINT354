using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    CoreFinder finder;
    LockOn lockOn;
    public GameObject bulletPrefab;
    public float fireTime;
    bool firing;

    //Missile Data for testing
    public GameObject missilePrefab;
    public int missileCount;

    // Use this for initialization
    void Start()
    {
        firing = false;
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        lockOn = finder.lockOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !firing && !finder.playerMovement.inSpecialMovement)
        {
            Fire();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (lockOn.isLockedOn)
            {
                MissileBarrage(lockOn.lockedTarget, missileCount);
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
        GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
        newMissile.GetComponent<Missile>().FireMissile(target);
    }

    void MissileBarrage(GameObject target, int amount)
    {
        Vector3 dir = new Vector3(); ;
        GameObject[] missiles = new GameObject[amount];
        float deltaDir = 1.0f / (float)amount;
        for (int i = 0; i < missiles.Length; i++)
        {
            dir = new Vector3((-0.5f + deltaDir * i), 1, 1);
            missiles[i] = Instantiate(missilePrefab, transform.position, transform.rotation);
            Vector3 toLookAt = new Vector3();
            toLookAt += dir.x * transform.right;
            toLookAt += dir.y * transform.up;
            toLookAt += dir.z * transform.forward;
            Debug.Log(toLookAt);
            missiles[i].GetComponent<Missile>().FireMissile(target, toLookAt);
        }
    }
}
