using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConFightController : MonoBehaviour
{
    public List<GameObject> Cops = new List<GameObject>();
    public List<GameObject> Cons = new List<GameObject>();

    public List<AgentShootController> _copsControllers;
    public List<AutoShootController>  _consControllers;

    public Dictionary<string, string> ConActions = new Dictionary<string, string>
    { 
        { "Shoot0", "Shoot0"},
        { "Shoot1", "Shoot1"},
        { "Idle", "Idle"}
    };

    void Start()
    {
        // get controllers
        _consControllers = new List<AutoShootController>();
        foreach(GameObject con in Cons)
        {
            _consControllers.Add(con.GetComponent<AutoShootController>());
        }
        _copsControllers = new List<AgentShootController>();
        foreach(GameObject cop in Cops)
        {
            _copsControllers.Add(cop.GetComponent<AgentShootController>());
        }

        GameObject test = Cons[0];
        AutoShootController controller = test.GetComponent<AutoShootController>();
        controller.StartShooting(Cops[0].transform.position, 2, ConActions["Idle"]);
    }

    void Update()
    {
        // start by the cons
        for (int i = 0; i < Cops.Count; i++)
        {
            AutoShootController controller = _consControllers[i];
            if (controller.CanDoAction())
            {
                SetNextConAction(controller);
            }
        }

        // now the cops controlled by the agent
    }

    protected void SetNextConAction(AutoShootController controller)
    {
        // shoot cop 0, shoot cop 1, do nothing

        float[] probs = { 0.3f, 0.1f, 0.1f};

        // more chance of doing something if we were idle before

        if(controller.PreviousActionName == ConActions["Idle"])
        {
            probs[1] += 0.2f;
            probs[2] += 0.2f;
        }

        // more chance of shooting if target is standing up

        for(int i = 0; i < Cops.Count; i++)
        {
            probs[i] = _copsControllers[i].Crouching ? probs[i] : probs[i] + 0.2f;
        }





    }
}
