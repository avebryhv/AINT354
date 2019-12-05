using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas startCanvas;
    public TextMeshProUGUI countdownNumber;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.enabled = false;
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePauseCanvas()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;
    }

    public void StartCountdown()
    {
        startCanvas.enabled = true;
        GameFunctions.ForcePause();
        StartCoroutine("CountdownSequence");
    }

    IEnumerator CountdownSequence()
    {
        countdownNumber.text = "3";
        yield return new WaitForSecondsRealtime(1.0f);
        countdownNumber.text = "2";
        yield return new WaitForSecondsRealtime(1.0f);
        countdownNumber.text = "1";
        yield return new WaitForSecondsRealtime(1.0f);
        countdownNumber.text = "GO";
        yield return new WaitForSecondsRealtime(1.0f);
        EndCountdown();
    }

    public void EndCountdown()
    {
        startCanvas.enabled = false;
        GameFunctions.ForceResume();
    }
}
