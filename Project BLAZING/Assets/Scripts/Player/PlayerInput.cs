﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CoreFinder finder;


    // Start is called before the first frame update
    void Start()
    {
        finder = GameObject.FindGameObjectWithTag("CoreFinder").GetComponent<CoreFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            finder.lockOn.LockButtonPressed();
        }

        if (Input.GetButtonDown("Evade"))
        {
            finder.playerMovement.EvadePressed();
        }
    }
}