using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctions : MonoBehaviour
{
    public static bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PauseButtonPressed()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }

    }

    static void Pause()
    {
        isPaused = true;
        FreezeBodies();
        GameObject.FindObjectOfType<GameUI>().TogglePauseCanvas();
    }

    static void Resume()
    {
        isPaused = false;
        GameObject.FindObjectOfType<GameUI>().TogglePauseCanvas();
    }

    static void FreezeBodies()
    {
        Rigidbody[] foundBodies = GameObject.FindObjectsOfType<Rigidbody>();
        for (int i = 0; i < foundBodies.Length; i++)
        {
            foundBodies[i].velocity = new Vector3();
        }
    }
}
