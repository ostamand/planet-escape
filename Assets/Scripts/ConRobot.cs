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


    #region Public Properties

    public bool Shooting
    {
        get
        {
            return _shooting;
        }
        set
        {
            _shooting = value;
            _animator.SetBool("Shooting", value);
            if (_shooting)
            {
                _timeToFire = Time.time + delayProjectile;
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

    #endregion

    void Start()
	{
		_animator = GetComponent<Animator>();
        _projectiles = GetComponent<SpawnProjectiles>();
	}

    void ActionUntilRelease(KeyCode key, Action<bool> setState)
    {
        if (Input.GetKeyDown(key) && !IsMoving())
        {
            setState(true);
        }
        else if (Input.GetKeyUp(key))
        {
            setState(false);
        }
    }

	void Update()
	{
        // for dev

        ActionUntilRelease(KeyCode.Space, value => Shooting = value);

        ActionUntilRelease(KeyCode.DownArrow, value => Crouching = value);


        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsMoving())
        {
            Debug.Log("Running true");
            SetWalking(true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && _walking)
        {
            Debug.Log("Running false");
            SetWalking(false);
        }

        ProcessFiring();
        ProcessWalking();
	}

    public bool IsMoving()
    {
        return _crouching || _walking;
    }

    void SetWalking(bool isRunning)
    {
        _walking = isRunning;
        _animator.SetBool("Walking", _walking);
    }

    void ProcessFiring()
    {
        if (!_shooting) { return; }
        if(Time.time > _timeToFire)
        {
            _projectiles.Fire();
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

    private void OnAnimatorMove()
    {
        
    }
}
