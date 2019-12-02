using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject parent;
    bool hasDamaged;
    public float killTime;
    float killCounter;
    bool P1Source;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        killCounter = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameFunctions.isPaused)
        {
            killCounter += Time.deltaTime;
            if (killCounter >= killTime)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetParent(GameObject g)
    {
        parent = g;
    }

    public void SetParent(bool b)
    {
        P1Source = b;
    }

    public void SetTarget(GameObject g)
    {
        target = g;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasDamaged)
        {
            if (parent != null)
            {
                if (other.gameObject != parent)
                {
                    hasDamaged = true;
                    other.GetComponent<PlayerHealth>().TakeDamage(10);
                }

            }
            else if (target != null)
            {
                if (other.gameObject == target)
                {
                    hasDamaged = true;
                    other.GetComponent<PlayerHealth>().TakeDamage(10);
                }
            }
        }
        


    }
}
