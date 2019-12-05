using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunBullet : MonoBehaviour
{
    public float moveSpeed;
    public float killTime;
    float killCounter;
    public int damage = 1;
    //Curving Bullet Variables
    public GameObject target;
    public bool isCurving;
    public float curveSpeed;

    public bool isP1Bullet;
    public LayerMask wallLayer;
    public GameObject hitEffect;

    public Material p1Mat;
    public Material p2Mat;

    MeshRenderer mesh;
    public Light light;
    TrailRenderer trail;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        light = GetComponent<Light>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        //isCurving = false;
        //Invoke("Destroy", killTime);
        killCounter = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameFunctions.isPaused)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            Vector3 toMove = transform.forward * moveSpeed * Time.deltaTime;
            if (isCurving && target != null)
            {
                LookAtTarget(curveSpeed);
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, toMove.magnitude, wallLayer))
            {
                //Debug.Log("hit wall");
                //Destroy(gameObject);
                if (hit.collider.gameObject.tag == "Player" && !isP1Bullet)
                {
                    Debug.Log("hit");
                    hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                    Destroy();
                }
                else if (hit.collider.gameObject.tag == "Player2" && isP1Bullet)
                {
                    Debug.Log("hit");
                    hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                    Destroy();
                }
                else if (hit.collider.gameObject.tag == "Wall")
                {
                    Destroy();
                }

            }

            killCounter += Time.deltaTime;
            if (killCounter >= killTime)
            {
                Destroy();
            }
        }
        
    }

    void Destroy()
    {
        ParticleSystemRenderer hitImpact = Instantiate(hitEffect, transform.position, transform.rotation).GetComponent<ParticleSystemRenderer>();
        hitImpact.material = mesh.material;
        Destroy(gameObject);
    }    

    public void SetParent(bool b)
    {
        isP1Bullet = b;
        if (isP1Bullet)
        {
            mesh.material = p1Mat;
        }
        else
        {
            mesh.material = p2Mat;
        }
        light.color = mesh.material.color;
        trail.startColor = mesh.material.color;
    }

    public void SetCurving(GameObject t)
    {
        isCurving = true;
        target = t;
    }

    //void OnTriggerEnter(Collider col)
    //{

    //    if (col.tag == "Player" && !isP1Bullet)
    //    {
    //        Debug.Log("hit");
    //        col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    //        this.Destroy();
    //    }
    //    else if (col.tag == "Player2" && isP1Bullet)
    //    {
    //        Debug.Log("hit");
    //        col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    //        this.Destroy();
    //    }
    //    else if (col.tag == "Wall")
    //    {
    //        this.Destroy();
    //    }
    //}

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
