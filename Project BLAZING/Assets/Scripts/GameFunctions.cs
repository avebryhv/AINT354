using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameFunctions : MonoBehaviour
{
    public static bool isPaused;
    public static bool allowPause;
    static float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        allowPause = true;
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            gameTime += Time.deltaTime;
        }
    }

    public static void PauseButtonPressed()
    {
        if (allowPause)
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
        

    }

    public static void ForcePause()
    {
        allowPause = false;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        FreezeBodies();
        RumbleOffAll();
    }

    public static void ForceResume()
    {
        allowPause = true;
        isPaused = false;
    }

    static void Pause()
    {
        isPaused = true;
        FreezeBodies();
        GameObject.FindObjectOfType<GameUI>().TogglePauseCanvas();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        RumbleOffAll();
    }

    static void Resume()
    {
        isPaused = false;
        GameObject.FindObjectOfType<GameUI>().TogglePauseCanvas();
        Cursor.lockState = CursorLockMode.Locked;
    }

    static void FreezeBodies()
    {
        Rigidbody[] foundBodies = GameObject.FindObjectsOfType<Rigidbody>();
        for (int i = 0; i < foundBodies.Length; i++)
        {
            foundBodies[i].velocity = new Vector3();
        }
    }

    

    public static void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
    }

    public static float ReturnGameTime()
    {
        return gameTime;
    }

    public static void RumbleOffAll()
    {
        Gamepad[] pads = Gamepad.all.ToArray();
        for (int i = 0; i < pads.Length; i++)
        {
            pads[i].SetMotorSpeeds(0, 0);
        }
    }


}
