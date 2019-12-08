using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI lockStatus;
    public TextMeshProUGUI lockDistance;
    public TextMeshProUGUI missileCount;
    public TextMeshProUGUI missileApproaching;

    public Image healthBar;
    public Image evadeBar;
    public Image specialBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLockOn(GameObject target)
    {
        lockStatus.text = "Locked: " + target.name;
    }

    public void EndLockOn()
    {
        lockStatus.text = "Locked: ";
    }

    public void UpdateLockDistance(float distance)
    {
        lockDistance.text = "Range: " + distance.ToString();
    }

    public void UpdateHealthBar(int current, int max)
    {
        //max = 5;
        float amount = ((float)current / (float)max);
        healthBar.fillAmount = amount;
    }

    public void UpdateEvadeBar(float current, float max)
    {
        float amount = (current / max);
        evadeBar.fillAmount = amount;
    }

    public void UpdateMissileCounter(string text)
    {
        missileCount.text = text;
    }

    public void UpdateSpecialBar(float current, float max)
    {
        float amount = current / max;
        specialBar.fillAmount = amount;
    }

    public void ShowMissileApproaching()
    {
        missileApproaching.enabled = true;
    }

    public void HideMissileApproaching()
    {
        missileApproaching.enabled = false;
    }

    
}
