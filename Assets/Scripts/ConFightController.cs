using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFightController : MonoBehaviour
{
    public List<GameObject> Cops = new List<GameObject>();
    public List<GameObject> Cons = new List<GameObject>();

    public static readonly string[] ConActions = { "Idle", "Shoot0", "Shoot1" };

    void Start()
    {
        GameObject con = Cons[0];
        ShooterController controller = con.GetComponent<ShooterController>();
        controller.StartShooting(Cops[0].transform.position, 2, ConActions[2]);
    }

    void Update()
    {
        // start by the cons
        foreach(GameObject con in Cons)
        {
            ShooterController controller = con.GetComponent<ShooterController>();
            if (controller.CanRunAction())
            {
                SetNextConAction(controller);
            }
        }
    }

    protected void SetNextConAction(ShooterController controller)
    {
        // do nothing, shoot cop #0, shoot cop #2 
        float[] probs = { 0, 0, 0 };
    }
}
