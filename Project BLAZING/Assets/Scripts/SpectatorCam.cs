using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectatorCam : MonoBehaviour
{
    public Canvas spectatorUI;
    public CoreFinder p1Finder;
    public CoreFinder p2Finder;
    Camera cam;

    public RectTransform p1Follower;
    public RectTransform p2Follower;

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTrackerPositions()
    {
        Vector3 p1CamPos = cam.WorldToScreenPoint(p1Finder.player.transform.position);
        Vector3 p2CamPos = cam.WorldToScreenPoint(p2Finder.player.transform.position);

        p1Follower.transform.position = p1CamPos;
        p2Follower.transform.position = p2CamPos;
    }
}
