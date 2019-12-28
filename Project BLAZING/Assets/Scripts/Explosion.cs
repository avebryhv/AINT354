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
        GetComponent<AudioSource>().Play();
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
            if (other.tag == "Player" && !P1Source)
            {
                other.GetComponent<PlayerHealth>().TakeDamage(5);
                hasDamaged = true;
            }
            else if (other.tag == "Player2" && P1Source)
            {
                other.GetComponent<PlayerHealth>().TakeDamage(5);
                hasDamaged = true;
            }
            
        }
        


    }
}
