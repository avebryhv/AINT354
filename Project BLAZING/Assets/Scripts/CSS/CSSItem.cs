using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CSSPlayer", menuName = "CSSPlayer")]
public class CSSItem : ScriptableObject
{
    public int moveSpeed;
    public int health;
    public int fireRate;
    public int fireRange;
    public string leftWeapon;
    public string rightWeapon;
    public string backWeapon;
}
