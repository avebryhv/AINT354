using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SSSStageButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public enum stageList { arena2, arenaLong};
    public stageList stage;


    public bool isStartingSelected;

    Image image;
    Sprite spr;
    RectTransform rectT;

    public Image largeImage;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        spr = image.sprite;
        rectT = GetComponent<RectTransform>();
        if (isStartingSelected)
        {
            GetComponent<Selectable>().Select();
        }
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        SendImage();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }

    void SendImage()
    {
        largeImage.sprite = spr;
    }

    public void Pressed()
    {
        PlayerPrefs.SetString("Stage", stage.ToString());
        SceneManager.LoadScene("CharSelect");
    }

    public void Hover()
    {
        GetComponent<Selectable>().Select();
    }
}
