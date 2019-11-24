using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Camera cam;

    public Image healthBar;
    public Image healthBarBG;
    //public Text healthText;
    //public Image debuffIcon;

    public float lingerTime;
    float lingerCounter;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        active = false;
        healthBarBG.color = new Color(0, 0, 0, 0);
        healthBar.color = new Color(0, 1, 0, 0);
        //healthText.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        if (active)
        {
            lingerCounter -= Time.deltaTime;

            if (lingerCounter <= 0)
            {
                active = false;
                StartCoroutine(FadeUI(false));
            }
        }
    }

    public void CreateHealthBar(int maxHealth)
    {
        //healthText.text = (maxHealth + "/" + maxHealth);
    }

    public void Damage(int health, int maxHealth, int damage)
    {
        healthBar.fillAmount = (float)health / (float)maxHealth;
        //healthText.text = (health + "/" + maxHealth);
        healthBarBG.color = new Color(0, 0, 0, 0.7f);
        healthBar.color = new Color(0, 1, 0, 0.7f);
        //healthText.color = new Color(1, 1, 1, 0.7f);
        active = true;
        lingerCounter = lingerTime;
        //GameObject d = Instantiate(damageNumber);
        //d.transform.parent = transform;
        //d.GetComponent<DamageNumber>().Create(damage, 1.5f);
    }

    IEnumerator FadeUI(bool fadeIn)
    {
        if (fadeIn)
        {
            active = true;
            for (float i = 0; i <= 0.7f; i += Time.deltaTime)
            {
                healthBarBG.color = new Color(0, 0, 0, i);
                healthBar.color = new Color(0, 1, 0, i);
                //healthText.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0.7f; i >= 0; i -= Time.deltaTime)
            {
                healthBarBG.color = new Color(0, 0, 0, i);
                healthBar.color = new Color(0, 1, 0, i);
                //healthText.color = new Color(1, 1, 1, i);
                yield return null;
            }
            active = false;
        }
    }
}
