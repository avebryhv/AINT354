using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSFX : MonoBehaviour
{
    AudioSource audioPlayer;

    public AudioClip railgunCharge;
    public AudioClip railgunFire;
    public AudioClip engines;
    public AudioClip bulletFireA;
    public AudioClip bulletFireB;
    public AudioClip bulletFireC;
    public AudioClip hitImpact;
    public AudioClip explosion;
    public AudioClip missileLaunch;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRailGunCharge()
    {
        audioPlayer.PlayOneShot(railgunCharge);
    }

    public void PlayRailGunFire()
    {
        audioPlayer.PlayOneShot(railgunFire);
    }

    public void PlayBulletSounds()
    {
        int ran = Random.Range(0, 2);
        switch (ran)
        {
            case 0:
                audioPlayer.PlayOneShot(bulletFireA);
                break;
            case 1:
                audioPlayer.PlayOneShot(bulletFireB);
                break;
            case 2:
                audioPlayer.PlayOneShot(bulletFireC);
                break;
            default:
                break;
        }
    }

    public void PlayHitImpact()
    {
        audioPlayer.PlayOneShot(hitImpact);
    }

    public void PlayExplosion()
    {
        audioPlayer.PlayOneShot(explosion);
    }

    public void PlayMissileLaunch()
    {
        audioPlayer.PlayOneShot(missileLaunch);
    }
}
