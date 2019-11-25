using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    CoreFinder finder;
    public GameObject bulletPrefab;
    public float fireTime;
    bool firing;


    // Use this for initialization
    void Start()
    {
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        firing = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (!firing && finder.playerMovement.targetable)
        {
            Fire();
        }
    }


    void Fire()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        firing = true;
        Invoke("CoolDown", fireTime);
    }



    void CoolDown()
    {
        firing = false;
    }
}
