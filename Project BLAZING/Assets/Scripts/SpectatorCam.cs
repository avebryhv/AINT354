using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpectatorCam : MonoBehaviour
{
    public bool eSportsMode;
    public Canvas spectatorUI;
    public CoreFinder p1Finder;
    public CoreFinder p2Finder;
    Camera cam;

    public RectTransform p1Follower;
    public RectTransform p2Follower;
    public Image p1Health;
    public Image p2Health;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (eSportsMode)
        {
            cam = GetComponent<Camera>();
            Debug.Log("displays connected: " + Display.displays.Length);
            // Display.displays[0] is the primary, default display and is always ON.
            // Check if additional displays are available and activate each.
            if (Display.displays.Length > 1)
            {
                Display.displays[1].Activate();
            }
            if (Display.displays.Length > 2)
            {
                Display.displays[2].Activate();
            }
            p1ScoreText.text = PlayerPrefs.GetInt("Player1Score").ToString();
            p2ScoreText.text = PlayerPrefs.GetInt("Player2Score").ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eSportsMode)
        {
            UpdateTrackerPositions();
        }
        
    }

    void UpdateTrackerPositions()
    {
        Vector2 p1CamPos = cam.WorldToScreenPoint(p1Finder.player.transform.position);
        Vector2 p2CamPos = cam.WorldToScreenPoint(p2Finder.player.transform.position);
        Debug.Log(p1CamPos);
        p1Follower.localPosition = new Vector2(p1CamPos.x - spectatorUI.pixelRect.width / 2.0f, p1CamPos.y - spectatorUI.pixelRect.height / 2.0f);
        p2Follower.localPosition = new Vector2(p2CamPos.x - spectatorUI.pixelRect.width / 2.0f, p2CamPos.y - spectatorUI.pixelRect.height / 2.0f);
        //p1Follower.localPosition = p1CamPos;

        p1Health.fillAmount = ((float)p1Finder.playerHealth.currentHealth / (float)p1Finder.playerHealth.maxHealth);
        p2Health.fillAmount = ((float)p2Finder.playerHealth.currentHealth / (float)p2Finder.playerHealth.maxHealth);
    }
}
