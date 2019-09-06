using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoShootController : ActionCharacter
{
    // delay between start of animation & firing projectile
    public float delayProjectile = 0.4f;

    // firing speed = total animation time
    public float fireSpeed = 1.167f;

    private SpawnProjectiles _projectiles;

    private bool _shooting = false;
   
    private float _timeToFire = 0;

    #region Public Properties

    public bool Shooting
    {
        get
        {
            return _shooting;
        }
        set
        {
            if (_shooting != value)
            {
                _shooting = value;
                Animator.SetBool("Shooting", value);
                if (_shooting)
                {
                    _timeToFire = Time.time + delayProjectile;
                }
            }
        }
    }

    public Vector3 ShootingDirection { get; set; }

    #endregion

    void Start()
	{
        Setup();
        _projectiles = GetComponent<SpawnProjectiles>();
    }

    void Update()
    { 
        ProcessActions();
        ProcessFiring();
    }

    #region Private Helpers

    void ActionUntilRelease(KeyCode key, Action<bool> setState)
    {
        if (Input.GetKeyDown(key) && !IsNotIdle())
        {
            setState(true);
        }
        else if (Input.GetKeyUp(key))
        {
            setState(false);
        }
    }

    bool IsNotIdle()
    {
        return _crouching;
    }

    void ProcessFiring()
    {
        if (!_shooting) { return; }
        if (Time.time > _timeToFire)
        {
            _projectiles.Fire(ShootingDirection);
            _timeToFire = Time.time + fireSpeed + delayProjectile;
        }
    }

    void Rotate(float angle, float totalTime)
    {
        transform.Rotate(Vector3.up, Time.deltaTime * angle / totalTime );
    }

    void ShootForward()
    {
        ShootingDirection = transform.forward;
        Shooting = true;
    }

    void ShootTarget(Vector3 target)
    {
        ShootingDirection = (target - transform.position).normalized;
        Shooting = true;
    }

    #endregion

    #region Public Helpers

    public override void RunAction(Action action)
    {
        base.RunAction(action);
    }

    public void StartShooting(Vector3 target, int numberOfShots, string actionName)
    {
        List<TimedStep> steps = new List<TimedStep>
        {
            new TimedStep(deltaTime => Crouching = true, 1.0f),
            new TimedStep(deltaTime => Crouching = false, 0.75f),
            new TimedStep(deltaTime => ShootTarget(target), fireSpeed * numberOfShots),
            new TimedStep(deltaTime => Shooting = false, 0.2f),
            new TimedStep(deltaTime => Crouching = true, 0f)
        };
        AddTimedAction(steps, actionName);
    }

    #endregion
}
