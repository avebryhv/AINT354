using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMItem", menuName = "BGMItem")]
public class BGMITEM : ScriptableObject
{
    public AudioClip clip;
    public string title;
    public string copyright;
}
