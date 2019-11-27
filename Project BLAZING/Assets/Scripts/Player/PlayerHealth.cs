using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public CoreFinder finder;
    MainUI ui;
    public bool canTakeDamage;

    //Special Data
    public float maxSpecialCharge; //Charge required for special move
    public float specialCharge;
    public bool canSpecialCharge;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        ui = finder.mainUI;
        canTakeDamage = true;
        ui.UpdateHealthBar(currentHealth, maxHealth);
        canSpecialCharge = true;
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpecialCharge && specialCharge < maxSpecialCharge)
        {
            specialCharge += Time.deltaTime;
            specialCharge = Mathf.Clamp(specialCharge, 0, maxSpecialCharge);
        }
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        currentHealth = maxHealth;
        //ui.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            currentHealth -= damage;
            ui.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        Invoke("ResetCanTakeDamage", 2f);
        
    }

    public void ResetSpecialCharge()
    {
        specialCharge = 0;
    }

    public bool ReturnCanUseSpecial()
    {
        if (specialCharge == maxSpecialCharge)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }
    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }
}
