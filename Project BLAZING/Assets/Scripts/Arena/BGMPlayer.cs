using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    public BGMITEM[] bgmList;
    AudioSource audioPlayer;
    int bgmCount;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        bgmCount = bgmList.Length;
        PlayRandomBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayRandomBGM()
    {
        int toPlay = Random.Range(0, bgmCount);
        audioPlayer.clip = bgmList[toPlay].clip;
        audioPlayer.Play();
    }
}
