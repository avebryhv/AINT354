using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSElement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Select(int player)
    {
        //Debug.Log(name + " Selected by player " + player);
    }

    public virtual void Deselect(int player)
    {
        //Debug.Log(name + "Deselected by player " + player);
    }

    public virtual void OnPress(int player)
    {
        //Debug.Log(name + " Pressed by player " + player);
    }
}
