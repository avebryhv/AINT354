using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunBullet : MonoBehaviour
{
    public float moveSpeed;
    public float killTime;
    public int damage = 1;
    //Curving Bullet Variables
    public GameObject target;
    public bool isP1Bullet;
    public LayerMask hitLayer;

    // Use this for initialization
    void Start()
    {
        Invoke("Destroy", killTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        Vector3 toMove = transform.forward * moveSpeed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, toMove.magnitude, hitLayer))
        {
            Debug.Log("hit wall");
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }    

    public void SetParent(bool b)
    {
        isP1Bullet = b;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player" && !isP1Bullet)
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            this.Destroy();
        }
        else if (col.tag == "Player2" && isP1Bullet)
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            this.Destroy();
        }
        else if (col.tag == "Wall")
        {
            this.Destroy();
        }
    }

    //void LookAtTarget(float speed)
    //{
    //    Vector3 targetDir = target.transform.position - transform.position;

    //    // The step size is equal to speed times frame time.
    //    float step = speed * Time.deltaTime;

    //    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
    //    Debug.DrawRay(transform.position, newDir, Color.red);

    //    // Move our position a step closer to the target.
    //    transform.rotation = Quaternion.LookRotation(newDir);
    //}
}
