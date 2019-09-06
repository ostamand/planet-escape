using UnityEngine;
using System.Collections.Generic;

public abstract class ActionCharacter : MonoBehaviour
{
    protected Queue<TimedAction> _actionsQueue = new Queue<TimedAction>();

    protected void AddToActionQueue(TimedAction action)
    {
        _actionsQueue.Enqueue(action);

    }

    protected void EmptyActionQueue()
    {
        _actionsQueue = new Queue<TimedAction>();
    }

    protected void ProcessActions()
    {
        if (_actionsQueue.Count == 0) { return; }
        TimedAction action = _actionsQueue.Peek();
        if (action.Update(Time.deltaTime))
        {
            // action done. remove from queue
            _actionsQueue.Dequeue();
        }
    }

}
