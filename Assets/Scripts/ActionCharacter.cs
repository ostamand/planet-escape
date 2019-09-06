using UnityEngine;
using System.Collections.Generic;


public abstract class ActionCharacter : MonoBehaviour
{
    protected Queue<TimedStep> _stepsQueue = new Queue<TimedStep>();

    protected string _previousActionName = "";
    protected string _currentActionName = "";
    protected bool _crouching = false;

    public enum Action { Shoot0 = 0, Shoot1 = 1, Idle = 2 }
    public static Dictionary<Action, string> ActionLabels = new Dictionary<Action, string>
    {
        { Action.Shoot0, "Shoot0"},
        { Action.Shoot1, "Shoot1"},
        { Action.Idle, "Idle"}
    };
    public static Dictionary<int, Action> IndexToAction = new Dictionary<int, Action>
    {
        { 0, Action.Shoot0},
        { 1, Action.Shoot1},
        { 2, Action.Idle}
    };

    #region Public Properties

    public Animator Animator { get; private set; }

    public bool Crouching
    {
        get
        {
            return _crouching;
        }
        set
        {
            _crouching = value;
            Animator.SetBool("Crouching", value);
        }
    }

    public string PreviousActionName
    {
        get
        {
            return _previousActionName;
        }
    }

    #endregion

    #region Public Methods

    public void Setup()
    {
        Animator = GetComponent<Animator>();
    }

    public bool CanDoAction()
    {
        if (_stepsQueue.Count > 0) { return false; }
        return true;
    }

    public virtual void RunAction(Action action)
    {

    }

    #endregion

    #region Pivate Helpers

    protected void AddToActionQueue(TimedStep action)
    {
        _stepsQueue.Enqueue(action);

    }

    protected void EmptyStepsQueue()
    {
        _stepsQueue = new Queue<TimedStep>();
    }

    protected void ProcessActions()
    {
        if (_stepsQueue.Count == 0) { return; }
        TimedStep action = _stepsQueue.Peek();
        if (action.Update(Time.deltaTime))
        {
            // step done. remove from queue
            _stepsQueue.Dequeue();

            // check if all actions are done
            if(_stepsQueue.Count == 0)
            {
                _previousActionName = _currentActionName;
                _currentActionName = string.Empty;
            }
        }
    }

    protected void AddTimedAction(List<TimedStep> steps, string actionName)
    {
        EmptyStepsQueue();
        foreach (TimedStep step in steps)
        {
            _stepsQueue.Enqueue(step);
        }
        _currentActionName = actionName;
    }

    #endregion
}
