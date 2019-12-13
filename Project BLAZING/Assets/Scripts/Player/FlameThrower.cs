using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    bool isP1;
    ParticleSystem particles;
    public List<ParticleCollisionEvent> collisionEvents;
    public string targetTag;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        isOn = false;
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
                collisionEvents[i].colliderComponent.GetComponent<PlayerHealth>().TakeFireDamage(1);
            }
            i++;
        }
    }

    public void FlameThrowerHeld()
    {
        if (!isOn)
        {
            particles.Play();
            isOn = true;
        }
    }

    public void Released()
    {
        if (isOn)
        {
            particles.Stop();
            isOn = false;
        }
    }
}
