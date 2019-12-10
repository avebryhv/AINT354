using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public Image fillImage;
    public float holdTime;
    float holdCounter;
    public int sceneToGoIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckForInput())
        {
            holdCounter += Time.deltaTime;
            if (holdCounter >= holdTime)
            {
                SceneManager.LoadScene(sceneToGoIndex);
            }
        }
        else
        {
            holdCounter = 0;
        }
        UpdateFill();
    }

    bool CheckForInput()
    {
        bool anyHeld = false;
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i].buttonEast.ReadValue() == 1)
            {
                anyHeld = true;
            }
            
        }
        return anyHeld;
    }

    void UpdateFill()
    {
        float amount = holdCounter / holdTime;
        fillImage.fillAmount = amount;
    }
}
