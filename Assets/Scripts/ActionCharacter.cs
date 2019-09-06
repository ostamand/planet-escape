using UnityEngine;
using System.Collections.Generic;


public abstract class ActionCharacter : MonoBehaviour
{
    protected Queue<TimedStep> _stepsQueue = new Queue<TimedStep>();

    protected string _previousActionName = "";
    protected string _currentActionName = "";
    protected bool _crouching = false;
    private Animator _animator;

    #region Public Properties

    public Animator Animator
    {
        get
        {
            return _animator;
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
            _crouching = value;
            _animator.SetBool("Crouching", value);
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
        _animator = GetComponent<Animator>();
    }

    public bool CanDoAction()
    {
        if (_stepsQueue.Count > 0) { return false; }
        return true;
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
