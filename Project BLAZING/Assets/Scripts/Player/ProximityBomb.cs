using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBomb : MonoBehaviour
{
    public ParticleSystem radiusParticle;
    public GameObject explosionPrefab;
    MeshRenderer objectMesh;

    public Color p1Colour;
    public Color p2Colour;

    public float armTime;
    public bool isArmed;
    float armCounter;
    public int damage;
    bool isP1Bomb;
    bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        objectMesh = GetComponent<MeshRenderer>();
        hasDamaged = false;
        isArmed = false;
    }

    // Update is called once per frame
    void Update()
    {
        armCounter += Time.deltaTime;
        if (armCounter >= armTime)
        {
            isArmed = true;
        }
    }

    public void SetParent(bool isP1)
    {
        isP1Bomb = isP1;
        if (isP1Bomb)
        {
            radiusParticle.startColor = p1Colour;
        }
        else
        {
            radiusParticle.startColor = p2Colour;
        }
        radiusParticle.Play();
    }

    void OnTriggerEnter(Collider col)
    {
        if (!hasDamaged && isArmed)
        {
            if (col.tag == "Player" && !isP1Bomb)
            {
                Debug.Log("hit");
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                Explode();
            }
            else if (col.tag == "Player2" && isP1Bomb)
            {
                Debug.Log("hit");
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                Explode();
            }
        }
                
    }

    void Explode()
    {
        hasDamaged = true;
        radiusParticle.Stop();
        objectMesh.enabled = false;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
