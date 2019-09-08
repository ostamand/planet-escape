using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool _crouching = false;
    private bool _shooting = false;

    // all available actions to the agentxe
    public enum AgentAction { Shoot0 = 0, ShootBarrel0 = 1, Crouching = 2, Idle = 3}

    private AgentAction _currentAction = AgentAction.Idle;
    private AgentAction _previousAction = AgentAction.Idle;

    #region Public Properties

    public Animator Animator { get; private set; }

    public AgentAction CurrentAction
    {
        get
        {
            return _currentAction;
        }
        set
        {
            if (value != _currentAction)
            {
                _previousAction = _currentAction;
                _currentAction = value;
            }
        }
    }

    public AgentAction PreviousAction
    {
        get
        {
            return _previousAction;
        }
        set
        {
            _previousAction = value;
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
            return _crouching;
        }
        set
        {
            if (value != _crouching)
            {
                _crouching = value;
                if (_crouching)
                {
                    CanDoAction = false;
                    CurrentAction = AgentAction.Crouching;
                    _shooting = false;
                    SetAnimators();
                }
            }
        }
    }

    public bool Shooting
    {
        get
        {
            return _shooting;
        }
        set
        {
            _shooting = value;
            if (_shooting)
            {
                CanDoAction = false;
                CurrentAction = AgentAction.Shoot0;
                _crouching = false;
                SetAnimators();
            }
        }
    }

    #endregion

    #region Private Methods

    void Start()
    {
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
        Animator.SetBool("Shooting", _shooting) ;
        Animator.SetBool("Crouching", _crouching);
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
