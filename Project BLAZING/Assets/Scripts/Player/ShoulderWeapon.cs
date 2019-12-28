using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderWeapon : MonoBehaviour
{
    CoreFinder finder;
    LockOn lockOn;

    public enum shoulderType { HomingMissile, Bomb, Railgun, FlameThrower};
    public shoulderType type;
    


    public float chargeUpTime;
    float chargeTimer;
    public int maxCharges;
    int currentCharges;

    //Missile Data for testing
    public GameObject missilePrefab;
    public int missileBarrageCount;

    //Railgun Data
    public LayerMask railgunCollisionLayer;
    public ParticleSystem railgunChargeEmber;
    public ParticleSystem railgunExplosion;
    public GameObject railgunBulletLine;

    //Bomb Data
    public GameObject bombPrefab;

    //Flamethrower Data
    FlameThrower flame;

    // Start is called before the first frame update
    void Start()
    {
        lockOn = finder.lockOn;
        UpdateUI();
        chargeTimer = 0;
        //currentCharges = 0;
        flame = GetComponentInChildren<FlameThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameFunctions.isPaused)
        {
            if (currentCharges < maxCharges)
            {
                chargeTimer += Time.deltaTime;
                if (chargeTimer >= chargeUpTime)
                {
                    chargeTimer = 0;
                    currentCharges++;
                    if (currentCharges > maxCharges)
                    {
                        currentCharges = maxCharges;
                    }
                    UpdateUI();
                }
                finder.mainUI.UpdateSpecialBar(chargeTimer, chargeUpTime);
            }
        }
        
    }

    void UpdateUI()
    {
        switch (type)
        {
            case shoulderType.HomingMissile:
                finder.mainUI.UpdateMissileCounter("Missile Stock: " + currentCharges);
                break;
            case shoulderType.Bomb:
                if (currentCharges >= 1)
                {
                    finder.mainUI.UpdateMissileCounter("Bomb: READY");
                }
                else
                {
                    finder.mainUI.UpdateMissileCounter("Bomb: CHARGING");
                }
                break;
            case shoulderType.Railgun:
                if (currentCharges >= 1)
                {
                    finder.mainUI.UpdateMissileCounter("Railgun: READY");
                }
                else
                {
                    finder.mainUI.UpdateMissileCounter("Railgun: CHARGING");
                }
                break;
            default:
                break;
        }
    }

    public void ShoulderWeaponPressed()
    {
        if (currentCharges > 0)
        {
            switch (type)
            {
                case shoulderType.HomingMissile:
                    MissileButtonPressed();
                    break;
                case shoulderType.Bomb:
                    DropProximityBomb();
                    break;
                case shoulderType.Railgun:
                    StartCoroutine("FireRailgunCo");
                    break;
                default:
                    break;
            }
        }
        
        UpdateUI();
    }

    public void ShoulderWeaponHeld()
    {
        if (chargeTimer > 0)
        {
            chargeTimer -= Time.deltaTime;
            flame.FlameThrowerHeld();
        }
    }

    public void ShoulderWeaponReleased()
    {
        flame.Released();
    }

    public void MissileButtonPressed()
    {
        if (lockOn.isLockedOn)
        {
            //MissileBarrage(lockOn.lockedTarget, missileBarrageCount);
            Missile(lockOn.softLockTarget);
        }
    }

    void Missile(GameObject target)
    {
        if (currentCharges > 0)
        {
            GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            newMissile.GetComponent<Missile>().FireMissile(target, transform.forward, finder.playerMovement.isPlayer1);
            currentCharges--;
            finder.sfx.PlayMissileLaunch();
        }

    }    

    void SetMaxCharges(int amount, bool startCharged)
    {
        maxCharges = amount;
        if (startCharged)
        {
            currentCharges = maxCharges;
        }
        else
        {
            currentCharges = 0;
        }
        UpdateUI();
    }

    float FireRailgun()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.15f, transform.forward, out hit, Mathf.Infinity, railgunCollisionLayer))
        {
            //Debug.Log("hit wall");
            //Destroy(gameObject);
            if (hit.collider.gameObject.tag == "Player" && !finder.playerMovement.isPlayer1)
            {
                Debug.Log("hit");
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(8);                
            }
            else if (hit.collider.gameObject.tag == "Player2" && finder.playerMovement.isPlayer1)
            {
                Debug.Log("hit");
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(8);                
            }
            else if (hit.collider.gameObject.tag == "Wall")
            {
                
            }

        }
        return hit.distance;
    }

    IEnumerator FireRailgunCo()
    {
        currentCharges--;
        UpdateUI();
        railgunChargeEmber.Play();
        finder.sfx.PlayRailGunCharge();
        yield return new WaitForSecondsRealtime(1.5f);
        finder.sfx.PlayRailGunFire();
        railgunExplosion.Play();
        float dist = FireRailgun();
        RailgunLine newLine = Instantiate(railgunBulletLine, transform.position, transform.rotation).GetComponent<RailgunLine>();
        newLine.SetLocations(new Vector3(0,0,0), new Vector3(0, 0, dist));      
        
    }

    void DropProximityBomb()
    {
        ProximityBomb newBomb = Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation).GetComponent<ProximityBomb>();
        newBomb.SetParent(finder.playerMovement.isPlayer1);
        currentCharges--;
    }

    public void SetStats(PlayerMovement.mechType t)
    {
        switch (t)
        {
            case PlayerMovement.mechType.Normal:
                SetMaxCharges(5, true);
                chargeUpTime = 10f;
                type = shoulderType.HomingMissile;
                break;
            case PlayerMovement.mechType.Fast:
                type = shoulderType.Bomb;
                SetMaxCharges(1, true);
                chargeUpTime = 20f;
                break;
            case PlayerMovement.mechType.Slow:
                SetMaxCharges(1, false);
                chargeUpTime = 8f;
                type = shoulderType.Railgun;
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
