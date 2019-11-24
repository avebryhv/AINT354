using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFinder : MonoBehaviour
{
    public GameObject player;
    public EnemyList enemyList;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyList = GetComponent<EnemyList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
