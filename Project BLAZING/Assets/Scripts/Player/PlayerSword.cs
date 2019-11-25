using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public int damage;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            TestSwing();
        }
    }

    public void TestSwing()
    {
        anim.Play("swing", 0, 0);
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Damageable")
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);            
        }
        else if (col.tag == "Wall")
        {
            
        }
    }
}
