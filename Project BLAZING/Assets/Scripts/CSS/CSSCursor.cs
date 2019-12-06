using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSCursor : MonoBehaviour
{
    RectTransform rect;
    public float moveSpeed;
    public int player;
    CSSElement currentElement;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveMovement(Vector2 input)
    {
        rect.transform.position += new Vector3(input.x, input.y, 0) * moveSpeed;
    }

    public void ClickPressed()
    {
        if (currentElement != null)
        {
            currentElement.OnPress(player);
        }
    }

    public void BackPressed()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CSSElement")
        {
            currentElement = collision.GetComponent<CSSElement>();
            currentElement.Select(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CSSElement")
        {            
            currentElement.Deselect(player);
            currentElement = null;
        }
    }
}
