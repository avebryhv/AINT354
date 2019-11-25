using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float moveSpeed;
    public float killTime;
    public int damage = 1;

    // Use this for initialization

    void Start()
    {
        Invoke("Destroy", killTime);
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }



    void Destroy()
    {
        Destroy(gameObject);
    }



    void OnTriggerEnter(Collider col)
    {        

        if (col.tag == "Player")
        {
            Debug.Log("hit");
            if (col.gameObject.GetComponent<PlayerHealth>().canTakeDamage)
            {
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                this.Destroy();
            }
            
            
        }

        else if (col.tag == "Wall")
        {
            this.Destroy();
        }

    }
}
