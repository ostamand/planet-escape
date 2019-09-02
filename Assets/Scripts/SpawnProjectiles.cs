using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private float _timeToFire = 0;
    private GameObject _effectToSpawn;
    private float _fireRate;

    private void Start()
    {
        _effectToSpawn = vfx[0];
        _fireRate = _effectToSpawn.GetComponent<ProjectileMove>().fireRate;
    }

    private void Update()
    {
        /*
        ConRobot.RobotState state = gameObject.GetComponent<ConRobot>().CurrentState;
        if (state == ConRobot.RobotState.Shooting && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 / _fireRate;
            SpawnVFX();
        }
        */
    }

    public void Fire()
    {
        SpawnVFX();
    }

    private void SpawnVFX()
    {
        if (firePoint != null)
        {
            Instantiate(_effectToSpawn, firePoint.transform.position, Quaternion.identity);
        }
    }
}
