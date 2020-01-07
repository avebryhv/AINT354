using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;


public class GameUI : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas startCanvas;
    public TextMeshProUGUI countdownNumber;
    public Button firstButton;
    public PostProcessVolume volume;
    ColorGrading grading;

    // Start is called before the first frame update
    void Start()
    {        
        volume.profile.TryGetSettings(out grading);
        pauseCanvas.enabled = false;
        grading.enabled.value = false;
        grading.active = false;
        StartCountdown();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && Input.GetKeyDown(KeyCode.P))
        {
            ColourShift();
        }
    }

    public void TogglePauseCanvas()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;
        if (pauseCanvas.enabled)
        {
            firstButton.Select();
        }
    }

    public void SetUp()
    {

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

    public void Resume()
    {
        GameFunctions.PauseButtonPressed();
    }

    public void ColourShift()
    {
        if (grading.active)
        {
            grading.enabled.value = false;
            grading.active = false;
        }
        else
        {
            grading.enabled.value = true;
            grading.active = true;
        }
        
    }
}
