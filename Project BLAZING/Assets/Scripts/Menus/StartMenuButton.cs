using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    Selectable thisButton;
    float originalRight;
    RectTransform rectTransform;
    bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRight = rectTransform.offsetMax.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            Expand();
        }
        else
        {
            Contract();
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
    }

    void Expand()
    {
        float targetRight = originalRight + 100;
        Vector2 targetVector = new Vector2(targetRight, rectTransform.offsetMax.y);
        rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, targetVector, Time.deltaTime * 2);
    }

    void Contract()
    {
        float targetRight = originalRight;
        Vector2 targetVector = new Vector2(targetRight, rectTransform.offsetMax.y);
        rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, targetVector, Time.deltaTime * 2);
    }


}
