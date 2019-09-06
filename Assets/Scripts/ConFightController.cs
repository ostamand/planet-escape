using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFightController : MonoBehaviour
{
    public List<GameObject> Cops = new List<GameObject>();
    public List<GameObject> Cons = new List<GameObject>();

    public Dictionary<string, string> ConActions = new Dictionary<string, string>
    {
        { "Idle", "Idle"},
        { "Shoot0", "Shoot0"},
        { "Shoot1", "Shoot1"}
    };

    void Start()
    {
        GameObject con = Cons[0];
        ShooterController controller = con.GetComponent<ShooterController>();
        controller.StartShooting(Cops[0].transform.position, 2, ConActions["Idle"]);
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
        float[] probs = { 0.3f, 0.1f, 0.1f};

        // more chance of doing something if we were idle before
        if(controller.PreviousActionName == ConActions["Idle"])
        {
            probs[1] += 0.2f;
            probs[2] += 0.2f;
        }

        // more chance of shooting if target is standing up





    }
}
