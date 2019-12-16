using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    int state;
    public Mesh[] meshes;
    public int[] healthBoundary;

    MeshRenderer meshR;
    // Start is called before the first frame update
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        state = 0;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (state > 0)
        {
            if (currentHealth <= healthBoundary[state - 1])
            {
                state -= 1;
                
            }
        }
    }
}
