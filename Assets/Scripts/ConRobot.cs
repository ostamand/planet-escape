using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ConRobot : MonoBehaviour
{
    // delay between start of animation & firing projectile
    public float delayProjectile = 0.4f;
    public float fireSpeed = 1.167f;

    private Animator _animator;
    private SpawnProjectiles _projectiles;
    private bool _shooting = false;
    private float _timeToFire = 0;
   
    void Start()
	{
		_animator = GetComponent<Animator>();
        _projectiles = GetComponent<SpawnProjectiles>();
	}

	void Update()
	{
        // for dev only
		if (Input.GetKeyDown(KeyCode.Space) && !_shooting)
		{
            SetShooting(true);
		}
        else if (Input.GetKeyUp(KeyCode.Space) && _shooting)
        {
            SetShooting(false);
        }
        ProcessFiring();
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
