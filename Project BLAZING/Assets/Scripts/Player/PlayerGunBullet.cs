using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunBullet : MonoBehaviour
{
    public float moveSpeed;
    public float killTime;
    public int damage = 1;
    //Curving Bullet Variables
    public bool curving;
    public GameObject target;
    public float curveSpeed;

    // Use this for initialization
    void Start()
    {
        Invoke("Destroy", killTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (curving && target != null)
        {
            LookAtTarget(curveSpeed);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }    

    public void SetCurving(GameObject t)
    {
        target = t;
        curving = true;

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Damageable")
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            this.Destroy();
        }
        else if (col.tag == "Wall")
        {
            this.Destroy();
        }
    }

    void LookAtTarget(float speed)
    {
        Vector3 targetDir = target.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
