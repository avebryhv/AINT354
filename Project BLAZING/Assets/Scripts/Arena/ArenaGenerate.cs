using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGenerate : MonoBehaviour
{
    public GameObject arena2;
    public GameObject arenaLong;

    public SSSStageButton.stageList stage;

    void Awake()
    {
        string selection = PlayerPrefs.GetString("Stage");
        switch (selection)
        {
            case "arena2":
                arena2.SetActive(true);
                break;
            case "arenaLong":
                arenaLong.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
