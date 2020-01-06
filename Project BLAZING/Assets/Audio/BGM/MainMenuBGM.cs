using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBGM : MonoBehaviour
{
    public int ArenaSceneNum;
    AudioSource aud;
    //bool markedForDestroy;
    void Awake()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        aud = GetComponent<AudioSource>();
        //Debug.Log(FindObjectsOfType<AudioSource>().Length);
        if (FindObjectsOfType<AudioSource>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ////Debug.Log("OnSceneLoaded: " + scene.name);
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    //if (markedForDestroy)
        //    //{
        //    //    Destroy(gameObject);
        //    //}

        //}
        //else
        //{
        //    //markedForDestroy = true;
        //    if (SceneManager.GetActiveScene().buildIndex == ArenaSceneNum)
        //    {
        //        aud.Stop();
        //    }
        //    else
        //    {
        //        if (!aud.isPlaying)
        //        {
        //            aud.Play();
        //        }
        //    }
        //}

        if (SceneManager.GetActiveScene().buildIndex == ArenaSceneNum)
        {
            if (aud.isPlaying)
            {
                aud.Pause();
            }
            
        }
        else
        {
            if (!aud.isPlaying)
            {
                aud.Play();
            }
        }

        
    }

}
