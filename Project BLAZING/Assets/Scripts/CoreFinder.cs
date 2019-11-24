using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFinder : MonoBehaviour
{
    public GameObject player;
    public LockOn lockOn;
    public EnemyList enemyList;
    public MainUI mainUI;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lockOn = player.GetComponent<LockOn>();
        enemyList = GetComponent<EnemyList>();
        mainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
