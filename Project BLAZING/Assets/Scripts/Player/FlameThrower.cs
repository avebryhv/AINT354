using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    bool isP1;
    ParticleSystem particles;
    public List<ParticleCollisionEvent> collisionEvents;
    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);

        
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (other.tag == targetTag)
            {
                other.GetComponent<PlayerHealth>().TakeFireDamage(1);
            }
            i++;
        }
    }
}
