using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFireBox : MonoBehaviour
{
    public ParticleSystem particles;
    public BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        col = GetComponent<BoxCollider>();
        particles.Stop();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        particles.Play();
        col.enabled = true;
    }

    void OnTriggerStay(Collider col)
    {

        if (col.tag == "Player" || col.tag == "Player2")
        {
            col.GetComponent<PlayerHealth>().TakeFireDamage(1);
        }
    }
}
