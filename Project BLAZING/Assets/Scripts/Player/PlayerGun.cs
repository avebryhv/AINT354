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
        GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
        newMissile.GetComponent<Missile>().FireMissile(target);
    }
}
