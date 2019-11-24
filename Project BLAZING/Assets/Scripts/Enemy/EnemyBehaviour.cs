using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public EnemyHealthBar healthBar;

    public float moveSpeed;
    public float turnSpeed;

    float xMove;
    float zMove;

    public Vector2 direction;
    CoreFinder finder;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.Damage(currentHealth, maxHealth, damage);
        if (currentHealth <= 0)
        {
            enemyList.RemoveFromList(gameObject);
            Destroy(gameObject);
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    //void Move(Vector2 dir)
    //{
    //    xMove = dir.x * moveSpeed;
    //    zMove = dir.y * moveSpeed;

    //    Vector3 diagonal = new Vector3(dir.x, 0, dir.y);
    //    if (diagonal.magnitude > 1)
    //    {
    //        zMove = zMove / diagonal.magnitude;
    //        xMove = xMove / diagonal.magnitude;
    //    }
    //}
}
