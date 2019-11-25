using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyHealthBar healthBar;
    public Rigidbody rb;

    public float moveSpeed;
    public float turnSpeed;

    float xMove;
    float zMove;

    public CoreFinder finder;
    public GameObject player;
    public EnemyList enemyList;

    public bool isVisible; //Check if enemy can be seen on camera

    // Start is called before the first frame update
    public virtual void Start()
    {
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        player = finder.player;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        enemyList = finder.enemyList;
        enemyList.AddToList(gameObject);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    public Vector3 ReturnClosestPointTo(Vector3 position)
    {
        Collider col = GetComponent<Collider>();
        Vector3 toReturn = col.ClosestPoint(position);
        return toReturn;
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);        
    }

    public void Move(Vector3 dir)
    {
        dir.y = 0;

        //xMove = dir.x * moveSpeed;
        //zMove = dir.z * moveSpeed;

        //Vector3 diagonal = new Vector3(dir.x, 0, dir.y);
        //if (diagonal.magnitude > 1)
        //{
        //    zMove = zMove / diagonal.magnitude;
        //    xMove = xMove / diagonal.magnitude;
        //}

        //Vector3 xVec = xMove * transform.right;
        //Vector3 zVec = zMove * transform.forward;

        //rb.velocity = (xVec + zVec);

        rb.velocity = dir * moveSpeed;
    }

    public void Move(float horizontal, float vertical)
    {
        Debug.Log(horizontal + " " + vertical);
        xMove = horizontal * moveSpeed;
        zMove = vertical * moveSpeed;

        
        Vector3 diagonal = new Vector3(horizontal, 0, vertical);
        if (diagonal.magnitude > 1)
        {
            zMove = zMove / diagonal.magnitude;
            xMove = xMove / diagonal.magnitude;
        }

        Vector3 xVec = xMove * transform.right;
        Vector3 zVec = zMove * transform.forward;
        Debug.Log("Mvoing " + xMove + " " + zMove);
        rb.velocity = (xVec + zVec);
        

    }
}
