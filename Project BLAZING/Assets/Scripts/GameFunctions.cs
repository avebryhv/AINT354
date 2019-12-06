using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctions : MonoBehaviour
{
    public static bool isPaused;
    public static bool allowPause;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        allowPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Cursor.visible = false;
        FreezeBodies();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    static void Resume()
    {
        isPaused = false;
        GameObject.FindObjectOfType<GameUI>().TogglePauseCanvas();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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


}
