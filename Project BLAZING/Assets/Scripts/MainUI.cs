using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI lockStatus;
    public TextMeshProUGUI lockDistance;

    public Image healthBar;
    public Image evadeBar;

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
        float amount = ((float)current / (float)max);
        healthBar.fillAmount = amount;
    }

    public void UpdateEvadeBar(float current, float max)
    {
        float amount = (current / max);
        evadeBar.fillAmount = amount;
    }
}
