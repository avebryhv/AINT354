using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI lockStatus;
    public TextMeshProUGUI lockDistance;


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
}
