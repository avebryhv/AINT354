using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Canvas pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePauseCanvas()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;
    }
}
