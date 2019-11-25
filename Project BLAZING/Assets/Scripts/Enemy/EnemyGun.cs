using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireTime;
    bool firing;


    // Use this for initialization
    void Start()
    {
        firing = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (!firing)
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
