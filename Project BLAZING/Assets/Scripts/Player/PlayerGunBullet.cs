using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunBullet : MonoBehaviour
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
    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Damageable")
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            this.Destroy();
        }
        else if (col.tag == "Wall")
        {
            this.Destroy();
        }
    }
}
