using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemy : EnemyBehaviour
{
    public float retreatRange;
    public float OrbitRange;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        if (GetPlayerDistance() < retreatRange)
        {
            Debug.Log("Player In Range");
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (GetPlayerDistance() < OrbitRange)
        {
            transform.position += transform.right * moveSpeed* Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void Rotate()
    {
        if (finder.playerMovement.targetable)
        {
            transform.LookAt(player.transform);
        }
    }
}
