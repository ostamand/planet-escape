using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ConRobot : MonoBehaviour
{
    // delay between start of animation & firing projectile
    public float delayProjectile = 0.4f;
    // firing speed = total animation time
    public float fireSpeed = 1.167f;

    private Animator _animator;
    private SpawnProjectiles _projectiles;
    private bool _shooting = false;
    private bool _crouching = false;
    private float _timeToFire = 0;
   
    void Start()
	{
		_animator = GetComponent<Animator>();
        _projectiles = GetComponent<SpawnProjectiles>();
	}

	void Update()
	{
        // for dev only, shooting

		if (Input.GetKeyDown(KeyCode.Space) && !_shooting && !_crouching)
		{
            SetShooting(true);
		}
        else if (Input.GetKeyUp(KeyCode.Space) && _shooting)
        {
            SetShooting(false);
        }

        // for dev only, crouching

        if (Input.GetKeyDown(KeyCode.DownArrow) && !_crouching)
        {
            SetCrouching(true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && _crouching)
        {
            SetCrouching(false);
        }

        ProcessFiring();
	}

    void SetCrouching(bool isCrouching)
    {
        _crouching = isCrouching;
        _animator.SetBool("Crouching", _crouching);
    }

    void SetShooting(bool isShooting)
    {
        _shooting = isShooting;
        _animator.SetBool("Shooting", _shooting);
        _timeToFire = Time.time + delayProjectile;
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
}
