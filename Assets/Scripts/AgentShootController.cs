﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentShootController : MonoBehaviour
{

    private bool _crouching = false;





    public bool Crouching
    {
        get
        {
            return _crouching;
        }
        set
        {
            _crouching = value;
        }
    }

    public void StartCrouching()
    {
        if (!Crouching) { return; }
//# Animator.SetBool("Crouching", true);
    }



    void Start()
    {
        
    }

    void Update()
    {
       
    }



}
