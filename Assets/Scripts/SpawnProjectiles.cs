using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effEfectToSpawn;

    private void Start()
    {
        effEfectToSpawn = vfx[0];
    }

    public void Fire(Vector3 direction, int damage)
    {
        SpawnVFX(direction, damage);
    }

    private void SpawnVFX(Vector3 direction, int damage)
    {
        if (firePoint == null) { return; }
        GameObject bullet = Instantiate(effEfectToSpawn, firePoint.transform.position, Quaternion.identity);
        Destroy(bullet, 5f);
        bullet.GetComponent<ProjectileMove>().SetDirectionDamage(direction, damage);
    }
}
