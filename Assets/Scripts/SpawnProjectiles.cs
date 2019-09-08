using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private List<GameObject> vfx = new List<GameObject>();

    [SerializeField]
    private float maxDuration = 5f;


    private GameObject effectToSpawn;

    private void Start()
    {
        effectToSpawn = vfx[0];
    }

    public void Fire(Vector3 direction, int damage)
    {
        SpawnVFX(direction, damage);
    }

    private void SpawnVFX(Vector3 direction, int damage)
    {
        if (firePoint == null) { return; }
        GameObject bullet = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
        Destroy(bullet, maxDuration);
        bullet.GetComponent<ProjectileMove>().SetDirectionDamage(direction, damage);
    }
}
