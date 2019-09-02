using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    void Start()
    {
        
    }

    void Update()
    {
        if(speed != 0){
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        Destroy(gameObject);
    }
}
