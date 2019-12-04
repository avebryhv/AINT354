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

    public bool isMissileFollowing;
    public List<Missile> missilesFollowing;

    //Fire damage
    bool canTakeFireDamage;
    public float fireDamageCooldown;

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
        missilesFollowing = new List<Missile>();
        canTakeFireDamage = true;

        //Set charge to full for test
        specialCharge = maxSpecialCharge;

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpecialCharge && specialCharge < maxSpecialCharge)
        {
            specialCharge += Time.deltaTime;
            specialCharge = Mathf.Clamp(specialCharge, 0, maxSpecialCharge);
        }
        ui.UpdateSpecialBar(specialCharge, maxSpecialCharge);


        CheckMissiles();
        if (missilesFollowing.Count > 0)
        {
            ui.ShowMissileApproaching();
        }
        else
        {
            ui.HideMissileApproaching();
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
        if (canTakeDamage && !GameFunctions.isPaused)
        {
            canTakeDamage = false;
            currentHealth -= damage;
            ui.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                OnDeath();
            }
            GetComponentInChildren<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
            finder.lockOn.otherPlayer.GetComponent<PlayerHealth>().DisplayDamageMarker();
        }
        Invoke("ResetCanTakeDamage", 0.1f);
        
    }

    public void TakeFireDamage(int damage)
    {
        if (canTakeFireDamage && !GameFunctions.isPaused)
        {
            canTakeFireDamage = false;
            currentHealth -= damage;
            ui.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                OnDeath();
            }
            GetComponentInChildren<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
            finder.lockOn.otherPlayer.GetComponent<PlayerHealth>().DisplayDamageMarker();
        }
        Invoke("ResetCanTakeFireDamage", 0.5f);

    }

    void ResetCanTakeFireDamage()
    {
        canTakeFireDamage = true;
    }

    void OnDeath()
    {
        //GameFunctions.ReloadScene();
        if (finder.playerMovement.isPlayer1)
        {
            FindObjectOfType<WinScreen>().Player2Win();
        }
        else
        {
            FindObjectOfType<WinScreen>().Player1Win();
        }
        
        Destroy(gameObject);
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

    public void DisplayDamageMarker()
    {
        finder.crosshair.ShowHitMarker();
    }

    public void SetFinder(CoreFinder f)
    {
        finder = f;
    }

    public void AddMissile(Missile m)
    {
        missilesFollowing.Add(m);
    }

    public void RemoveMissile(Missile m)
    {
        missilesFollowing.Remove(m);
    }

    void CheckMissiles()
    {
        for (int i = 0; i < missilesFollowing.Count; i++)
        {
            if (missilesFollowing[i] == null)
            {
                missilesFollowing.RemoveAt(i);
            }
            else if (missilesFollowing[i].state != Missile.State.Locked)
            {
                missilesFollowing.RemoveAt(i);
            }
        }
    }
}
