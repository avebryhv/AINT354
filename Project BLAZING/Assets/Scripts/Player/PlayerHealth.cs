using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    CoreFinder finder;
    MainUI ui;
    public bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        ui = finder.mainUI;
        canTakeDamage = true;
        ui.UpdateHealthBar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        currentHealth = maxHealth;
        //ui.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ui.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
