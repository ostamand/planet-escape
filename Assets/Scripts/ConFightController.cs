using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Character = ActionCharacter;

public class ConFightController : MonoBehaviour
{
    [Header("Con Properties")]
    [SerializeField]
    private float baseShootProb = 0.1f;

    [SerializeField]
    private float baseIdleProb = 0.3f;

    [SerializeField]
    [Tooltip("More chance of shooting if target is standing up")]
    private float oppAddProb = 0.2f;

    [SerializeField]
    [Tooltip("More change of doing something if we were idle before")]
    private float idleBeforeProb = 0.2f;

    [SerializeField]
    private float crouchingTotalTime = 1.0f;

    [Space(10)]

    [SerializeField]
    private List<GameObject> cops = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _cons = new List<GameObject>();

    private List<AgentShootController> copCtrls;
    private List<AutoShootController>  _consControllers;

    void Start()
    {
        // get controllers
        _consControllers = new List<AutoShootController>();
        foreach(GameObject con in _cons)
        {
            _consControllers.Add(con.GetComponent<AutoShootController>());
        }
        copCtrls = new List<AgentShootController>();
        foreach(GameObject cop in cops)
        {
            copCtrls.Add(cop.GetComponent<AgentShootController>());
        }

        GameObject test = _cons[0];
        AutoShootController controller = test.GetComponent<AutoShootController>();
        controller.StartShooting(cops[0].transform.position, 2, Character.ActionLabels[Character.Action.Shoot0]);
    }

    void Update()
    {
        // start by the cons
        for (int i = 0; i < cops.Count; i++)
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
        int i;

        // shoot cop 0, shoot cop 1, do nothing

        float[] probs = { baseShootProb, baseShootProb, baseIdleProb};

        // more chance of doing something if we were idle before

        if(controller.PreviousActionName == Character.ActionLabels[Character.Action.Idle])
        {
            probs[1] += idleBeforeProb;
            probs[2] += idleBeforeProb;
        }

        // more chance of shooting if target is standing up

        for(i = 0; i < cops.Count; i++)
        {
            probs[i] = copCtrls[i].Crouching ? probs[i] : probs[i] + oppAddProb;
        }

        // get the actual next action

        float total = probs.Sum();
        probs = probs.Select(f => f / total).ToArray();

        for (i = 1; i < probs.Length; i++)
        {
            probs[i] = probs[i] + probs[i-1];
        }

        float choice = Random.value;
        float lowerBound = 0f;
        Character.Action nextAction = Character.Action.Idle;

        for(i = 0; i < Character.ActionLabels.Count; i++)
        {
            if (choice >= lowerBound && choice < probs[i])
            {
                nextAction = Character.IndexToAction[i];
                break;
            }
        }

        // Debug.Log("Action: " + Character.ActionLabels[nextAction]);

        // apply the action

        if(nextAction == Character.Action.Shoot0) // || nextAction == Character.Action.Shoot1
        {
            int numOfShots = (int)(Random.value * 3) + 1;
            Vector3 target = cops[i].transform.position;
            controller.StartShooting(target, numOfShots, Character.ActionLabels[nextAction]);
        }
        else
        {
            controller.StartCrouching(crouchingTotalTime);
        }
    }
}
