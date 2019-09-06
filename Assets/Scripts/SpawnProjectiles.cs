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

    public void Fire(Vector3 direction)
    {
        SpawnVFX(direction);
    }

    private void SpawnVFX(Vector3 direction)
    {
        if (firePoint != null)
        {
            GameObject bullet = Instantiate(_effectToSpawn, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<ProjectileMove>().Direction = direction;
        }
    }
}
