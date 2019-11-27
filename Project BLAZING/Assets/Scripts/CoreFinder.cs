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
    public Camera playerCam;

    // Start is called before the first frame update
    void Awake()
    {
        //Player1Pointers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1Pointers()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.SetFinder(this);
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.SetFinder(this);
        lockOn = player.GetComponent<LockOn>();
        lockOn.SetFinder(this);
        enemyList = GetComponent<EnemyList>();
        mainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
        lockOnSquare = GameObject.FindGameObjectWithTag("MainUI").GetComponentInChildren<DisplayLockOnSquares>();
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>();
        playerGun.SetFinder(this);
        playerCam = GameObject.FindGameObjectWithTag("P1Camera").GetComponent<Camera>();
    }

    public void Player2Pointers()
    {

    }
}
