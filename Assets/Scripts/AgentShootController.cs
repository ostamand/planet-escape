using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterManager))]
public class AgentShootController : MonoBehaviour
{
    [Header("Debug")]

    [SerializeField]
    private bool _debugMode = true;

    [SerializeField]
    private Light _actionLight;

    [SerializeField]
    [Tooltip("Shows a light when an action is available to the agent.")]
    private bool _showActionLight = true;

    [Space(5)]

    [Header("Agent Properties")]

    private bool _canDoAction = true;

    // all available actions to the agentxe
    public enum AgentAction { Shoot0 = 0, ShootBarrel0 = 1, Crouching = 2, Idle = 3}

    private CharacterManager character;

    private AgentAction currentAction = AgentAction.Idle;
    private AgentAction previousAction = AgentAction.Idle;

    #region Public Properties

    public Animator Animator { get; private set; }

    public AgentAction CurrentAction
    {
        get
        {
            return currentAction;
        }
        set
        {
            if (value != currentAction)
            {
                previousAction = currentAction;
                currentAction = value;
            }
        }
    }

    public AgentAction PreviousAction
    {
        get
        {
            return previousAction;
        }
        set
        {
            previousAction = value;
        }
    }

    public bool CanDoAction
    {
        get
        {
            return _canDoAction;
        }
        set
        {
            if(value != _canDoAction)
            {
                _canDoAction = value;
                if (_debugMode)
                {
                    _actionLight.enabled = _canDoAction;

                    if (_canDoAction)
                    {
                        Shooting = false;
                        Crouching = false;
                        SetAnimators();
                    }
                }
            }
        }
    }

    public bool Crouching
    {
        get
        {
            return character.Crouching;
        }
        set
        {
            if (value != character.Crouching)
            {
                character.Crouching = value;
                if (character.Crouching)
                {
                    CanDoAction = false;
                    CurrentAction = AgentAction.Crouching;
                    character.Shooting = false;
                    SetAnimators();
                }
            }
        }
    }

    public bool Shooting
    {
        get
        {
            return character.Shooting;
        }
        set
        {
            character.Shooting = value;
            if (character.Shooting)
            {
                CanDoAction = false;
                CurrentAction = AgentAction.Shoot0;
                character.Crouching = false;
                SetAnimators();
            }
        }
    }

    #endregion

    #region Private Methods

    void Start()
    {
        character = GetComponent<CharacterManager>();
        Animator = GetComponent<Animator>();
        _actionLight.enabled = false;

        Crouching = true;
    }

    void Update()
    {
       if(_debugMode && _canDoAction)
       {
            // Check input from keyboard for all possible actions

            // Crouching: Arrow down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Crouching = true;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shooting = true;
            }
       }



    }

    void SetAnimators()
    {
        Animator.SetBool("Shooting", character.Shooting) ;
        Animator.SetBool("Crouching", character.Crouching);
    }

    #endregion

    #region Public Helpers

    public void DoAction()
    {
        CanDoAction = true;

        // clear current action if needed



        // TODO set action thru agent
    }

    #endregion

}
