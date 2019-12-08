using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CSSSelected : CSSElement
{
    public bool locked;
    Image backgroundColour;
    public Color lockedColour;
    public Color baseColour;

    public Image selected;
    public Sprite OGImage;
    public CSSItem currentItem;

    public Image healthImage;
    public Image speedImage;
    public TextMeshProUGUI leftWepText;
    public TextMeshProUGUI rightWepText;
    public TextMeshProUGUI backWepText;
    CSS css;

    // Start is called before the first frame update
    void Start()
    {
        css = FindObjectOfType<CSS>();
        backgroundColour = GetComponent<Image>();
        OGImage = selected.sprite;
        Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSelectionImage(Image im)
    {
        selected.sprite = im.sprite;
    }

    public void SetStatBars(CSSItem item)
    {
        int maxFill = 5;
        healthImage.fillAmount = ((float)item.health / (float)maxFill);
        speedImage.fillAmount = ((float)item.moveSpeed / (float)maxFill);
        leftWepText.text = item.leftWeapon;
        rightWepText.text = item.rightWeapon;
        backWepText.text = item.backWeapon;
    }

    public void LockSelection(CSSItem item)
    {
        currentItem = item;
        SetStatBars(item);
        locked = true;
        backgroundColour.color = lockedColour;

    }

    public void Unlock()
    {
        locked = false;
        backgroundColour.color = baseColour;
    }

    public void SetHover(CSSItem item)
    {
        if (!locked)
        {
            int maxFill = 5;
            healthImage.fillAmount = ((float)item.health / (float)maxFill);
            speedImage.fillAmount = ((float)item.moveSpeed / (float)maxFill);
            leftWepText.text = item.leftWeapon;
            rightWepText.text = item.rightWeapon;
            backWepText.text = item.backWeapon;
        }
    }

    public void Clear()
    {
        if (!locked)
        {
            healthImage.fillAmount = 0;
            speedImage.fillAmount = 0;
            leftWepText.text = "---";
            rightWepText.text = "---";
            backWepText.text = "---";
            selected.sprite = OGImage;
        }
    }


}
