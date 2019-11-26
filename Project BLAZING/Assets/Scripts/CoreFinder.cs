using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFinder : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public LockOn lockOn;
    public EnemyList enemyList;
    public MainUI mainUI;
    public DisplayLockOnSquares lockOnSquare;
    public PlayerGun playerGun;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        lockOn = player.GetComponent<LockOn>();
        enemyList = GetComponent<EnemyList>();
        mainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
        lockOnSquare = GameObject.FindGameObjectWithTag("MainUI").GetComponentInChildren<DisplayLockOnSquares>();
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
