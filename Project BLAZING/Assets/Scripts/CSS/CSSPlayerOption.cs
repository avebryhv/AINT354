using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSSPlayerOption : CSSElement
{
    public int characterIndex;
    public Image model;
    public float turnSpeed;
    public CSSItem statObject;
    CSS css;

    // Start is called before the first frame update
    void Start()
    {
        css = FindObjectOfType<CSS>();
        //model.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
        //model.transform.Translate(new Vector3(0, 0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
        //model.transform.Rotate(new Vector3(0, turnSpeed, 0));    
    }

    public override void OnPress(int player)
    {
        base.OnPress(player);
        if (player == 1)
        {
            css.SetP1Selection(characterIndex, model, statObject);
            css.SetPlayer1Ready();
        }
        else if (player == 2)
        {
            css.SetPlayer2Ready();
            css.SetP2Selection(characterIndex, model, statObject);
        }
        
    }

    public override void Select(int player)
    {
        base.Select(player);
        if (player == 1)
        {
            css.P1HoverOver(model, statObject);
        }
        else if (player == 2)
        {
            css.P2HoverOver(model, statObject);
        }
        
    }

    public override void Deselect(int player)
    {
        base.Deselect(player);
        if (player == 1)
        {
            css.P1HoverOff();
        }
        else if (player == 2)
        {
            css.P2HoverOff();
        }
        
    }
}
