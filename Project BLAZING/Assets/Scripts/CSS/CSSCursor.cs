using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSCursor : MonoBehaviour
{
    public RectTransform canvasTransform;
    RectTransform rectTransform;
    public float moveSpeed;
    public int player;
    CSSElement currentElement;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //canvasTransform = GetComponentInParent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveMovement(Vector2 input)
    {
        Vector3 newPosition = rectTransform.transform.position + new Vector3(input.x, input.y, 0) * moveSpeed;

        if (newPosition.x > 0 && newPosition.x < canvasTransform.rect.width && newPosition.y > 0 && newPosition.y < canvasTransform.rect.height)
        {
            rectTransform.transform.position = newPosition;
        }
        //rectTransform.transform.position = newPosition;
    }

    public void RecieveNewPosition(Vector2 pos)
    {
        rectTransform.transform.position = pos;
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
        if (player == 1)
        {
            FindObjectOfType<CSS>().RemoveP1Lock();
        }
        else if (player == 2)
        {
            FindObjectOfType<CSS>().RemoveP2Lock();
        }
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
