using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private float duration = 5f;

    #region Public Properties

    public Vector3 Direction { get; set; }

    public int Damage { get; set; }

    #endregion

    #region Private Methods

    void Start()
    {

    }

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

        CharacterManager character = collision.gameObject.GetComponent<CharacterManager>();

        if(character != null)
        {
            character.TakeDamage(Damage);
            Debug.Log(character.Health);
        }
        
        Destroy(gameObject);
    }

    #endregion

    #region Public Methods

    public void SetDirectionDamage(Vector3 direction, int damage)
    {
        Direction = direction;
        Damage = damage;
    }

    #endregion

}
