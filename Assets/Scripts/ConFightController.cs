﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFightController : MonoBehaviour
{
    public List<GameObject> Cops = new List<GameObject>();
    public List<GameObject> Cons = new List<GameObject>();

    void Start()
    {
        GameObject con = Cons[0];
        ConRobot controller = con.GetComponent<ConRobot>();
        controller.StartShooting(Cops[0].transform.position, 2);
    }

    void Update()
    {
        
    }
}