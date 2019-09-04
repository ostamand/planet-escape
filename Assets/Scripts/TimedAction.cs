using System;

public class TimedAction
{
    private Action<float> _updateAction;
    private float _totalTime;
    private float _runTime = 0;

    public TimedAction(Action<float> updateAction, float totalTime)
    {
        _updateAction = updateAction;
        _totalTime = totalTime;
    }

    public bool Update(float deltaTime)
    {
        if(_runTime <= _totalTime)
        {
            _updateAction(deltaTime);
            _runTime += deltaTime;
            return false;
        }
        return true;
    }
}
