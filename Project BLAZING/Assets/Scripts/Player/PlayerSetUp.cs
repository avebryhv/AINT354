using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    //Input Variables from player select
    public PlayerMovement.mechType p1Type;
    public Vector3 p1StartPosition;


    public enum Player { Player1, Player2};
    public Player player;
    public CoreFinder p1Finder;
    public CoreFinder p2Finder;

    void Awake()
    {
        SetUpPlayer1();
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
        p1Finder = GetComponent<CoreFinder>();
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

    }

    void SetPlayerType(CoreFinder f, PlayerMovement.mechType type)
    {
        f.playerMovement.type = type;
    }
}
