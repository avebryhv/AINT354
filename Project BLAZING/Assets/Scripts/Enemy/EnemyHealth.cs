using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    CoreFinder finder;
    EnemyHealthBar healthBar;
    EnemyList enemyList;

    public int maxHealth;
    int currentHealth;


    public int stunThreshold;
    int stunBuildup;



    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        enemyList = finder.enemyList;
        currentHealth = maxHealth;
        
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

    
}
