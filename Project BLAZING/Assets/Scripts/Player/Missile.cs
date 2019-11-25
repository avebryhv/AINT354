using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int damage;
    public float timeBeforeLockOn;
    float floatTimer;
    public Vector3 initialDirection;
    public GameObject lockedTarget;
    public float floatingSpeed;
    public float lockedSpeed;
    public float turnSpeed;
    public enum State { Floating, Locked, NoTarget };
    public State state;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Floating;
        floatTimer = 0;
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(initialDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Floating)
        {
            if (lockedTarget == null)
            {
                state = State.NoTarget;
                
            }
            floatTimer += Time.deltaTime;
            //LookAtTarget(turnSpeed);
            transform.rotation = Quaternion.LookRotation(initialDirection);
            rb.velocity = transform.forward * floatingSpeed;
            if (floatTimer >= timeBeforeLockOn)
            {
                state = State.Locked;
            }
        }
        else if (state == State.Locked)
        {
            if (lockedTarget == null)
            {
                state = State.NoTarget;
            }
            else
            {
                turnSpeed += Time.deltaTime;
                LookAtTarget(turnSpeed);
                rb.velocity = transform.forward * lockedSpeed;
            }
        }
        else
        {
            rb.velocity = transform.forward * lockedSpeed;
        }
    }    

    public void FireMissile(GameObject target)
    {
        lockedTarget = target;
    }

    public void FireMissile(GameObject target, Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        initialDirection = dir;
        lockedTarget = target;
    }

    void LookAtTarget(float speed)
    {
        Vector3 targetDir = lockedTarget.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Damageable")
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
