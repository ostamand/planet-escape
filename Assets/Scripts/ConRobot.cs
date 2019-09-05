using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ConRobot : MonoBehaviour
{
    // delay between start of animation & firing projectile
    public float delayProjectile = 0.4f;
    // firing speed = total animation time
    public float fireSpeed = 1.167f;
    public float walkSpeed = 1.0f;

    private Animator _animator;

    private SpawnProjectiles _projectiles;

    private bool _shooting = false;
    private bool _crouching = false;
    private bool _walking = false;

    private float _timeToFire = 0;

    private Queue<TimedAction> _actionsQueue = new Queue<TimedAction>();

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
                _animator.SetBool("Shooting", value);
                if (_shooting)
                {
                    _timeToFire = Time.time + delayProjectile;
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
            _crouching = value;
            _animator.SetBool("Crouching", value);
        }
    }

    public Vector3 ShootingDirection { get; set; }

    #endregion

    void Start()
	{
		_animator = GetComponent<Animator>();
        _projectiles = GetComponent<SpawnProjectiles>();
    }

    void Update()
    { 
        // for dev
        // ActionUntilRelease(KeyCode.Space, value => Shooting = value);

        // ActionUntilRelease(KeyCode.DownArrow, value => Crouching = value);

        /*
        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsNotIdle())
        {
            Debug.Log("Running true");
            SetWalking(true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && _walking)
        {
            Debug.Log("Running false");
            SetWalking(false);
        }
        */

        ProcessActions();
        ProcessFiring();

        // ProcessWalking();
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
        return _crouching || _walking;
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

    void ProcessWalking()
    {
        if (!_walking) { return; }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
    }

    void ProcessActions()
    {
        if (_actionsQueue.Count == 0){return;}
        TimedAction action = _actionsQueue.Peek();
        if (action.Update(Time.deltaTime))
        {
            // action done. remove from queue
            _actionsQueue.Dequeue();
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

    void EmptyActionQueue()
    {
        _actionsQueue = new Queue<TimedAction>();
    }

    void AddToActionQueue(TimedAction action)
    {
        _actionsQueue.Enqueue(action);

    }

    #endregion

    #region Public Helpers

    public void StartShooting(Vector3 target, int numberOfShots)
    {
        // make sure queue is empty
        EmptyActionQueue();

        AddToActionQueue(
            new TimedAction(deltaTime => Crouching = true, 1.0f)
            );
        //AddToActionQueue(
        //    new TimedAction(deltaTime => Rotate(30f, 0.5f), 0.5f)
        //    );
        AddToActionQueue(
            new TimedAction(deltaTime => Crouching = false, 0.75f)
            );
        AddToActionQueue(
            new TimedAction(deltaTime => ShootTarget(target), fireSpeed * numberOfShots)
            );
        AddToActionQueue(
            new TimedAction(deltaTime => Shooting = false, 0.2f)
            );
        AddToActionQueue(
            new TimedAction(deltaTime => Crouching = true, 0f)
            );
        //AddToActionQueue(
        //    new TimedAction(deltaTime => Rotate(-30f, 0.5f), 0.5f)
        //    );
    }

    #endregion

    void SetWalking(bool isRunning)
    {
        _walking = isRunning;
        _animator.SetBool("Walking", _walking);
    }

    private void OnAnimatorMove()
    {
        
    }
}
