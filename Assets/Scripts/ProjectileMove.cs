using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject hitPrefab;

    public Vector3 Direction { get; set; }

    void Update()
    {
        if(speed > 0){
            transform.position += Direction * speed * Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            Instantiate(hitPrefab, pos, rot);
        }

        Destroy(gameObject);
    }
}
