using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    //Input Variables from player select
    public PlayerMovement.mechType p1Type;
    public Vector3 p1StartPosition;

    public PlayerMovement.mechType p2Type;
    public Vector3 p2StartPosition;


    
    public CoreFinder p1Finder;
    public CoreFinder p2Finder;

    void Awake()
    {
        SetUpPlayer1();
        SetUpPlayer2();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpPlayer1()
    {
        p1Finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
        if (p1Finder != null)
        {
            p1Finder.Player1Pointers();
        }
        else
        {
            p1Finder = GetComponentInChildren<CoreFinder>();
            p1Finder.Player1Pointers();
        }
        SetPlayerType(p1Finder, p1Type);
    }

    void SetUpPlayer2()
    {
        p2Finder = GameObject.FindGameObjectWithTag("P2CoreFinder").GetComponent<CoreFinder>();
        if (p2Finder != null)
        {
            p2Finder.Player2Pointers();
        }
        else
        {
            p2Finder = GetComponentInChildren<CoreFinder>();
            p2Finder.Player2Pointers();
        }
        SetPlayerType(p2Finder, p2Type);
    }

    void SetPlayerType(CoreFinder f, PlayerMovement.mechType type)
    {
        //f.playerMovement.type = type;
        f.playerMovement.SetTypeStats(type);
    }
}
