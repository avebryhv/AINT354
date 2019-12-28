using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerOptions : MonoBehaviour
{
    public Slider p1SenSlider;
    public Slider p2SenSlider;
    public Canvas optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("P1Sensitive"))
        {
            p1SenSlider.value = PlayerPrefs.GetFloat("P1Sensitive");
        }
        else
        {
            p1SenSlider.value = 1;
        }

        if (PlayerPrefs.HasKey("P2Sensitive"))
        {
            p2SenSlider.value = PlayerPrefs.GetFloat("P2Sensitive");            
        }
        else
        {
            p2SenSlider.value = 1;
        }
        
        HidePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel()
    {
        optionsPanel.enabled = true;
    }

    public void HidePanel()
    {
        optionsPanel.enabled = false;
    }
}
