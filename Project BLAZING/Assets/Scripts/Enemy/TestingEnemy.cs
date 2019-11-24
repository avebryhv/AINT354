using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemy : EnemyBehaviour
{


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.LookAt(player.transform);
    }
}
