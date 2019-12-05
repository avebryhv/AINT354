using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFire : MonoBehaviour
{
    public ArenaWarning[] arenaWarnings;
    public bool[] isSectionWalled;
    int sectionsRemaining;

    public float timeUntilWallAppears;
    float timer;

    public int selectedSection;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        sectionsRemaining = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameFunctions.isPaused)
        {
            if (sectionsRemaining > 1)
            {
                timer += Time.deltaTime;
                if (timer >= timeUntilWallAppears)
                {
                    DecideSection();
                    timer = 0;
                }
            }
        }
        
        
    }

    void DecideSection()
    {
        //selectedSection = -1;
        bool validSection = false;
        while (validSection == false)
        {
            selectedSection = Random.Range(0, 5);
            if (isSectionWalled[selectedSection])
            {
                validSection = false;
            }
            else
            {
                validSection = true;
            }
        }

        isSectionWalled[selectedSection] = true;
        arenaWarnings[selectedSection].SetRising();
        sectionsRemaining -= 1;
    }

    
}
