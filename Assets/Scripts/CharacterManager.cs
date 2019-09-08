using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private int weaponDamage = 1;

    [SerializeField]
    private int totalHealth = 100;

    private bool shooting = false;
    private bool crouching = false;

    private int health = 0;

    #region Public Properties

    public int WeaponDamage
    {
        get
        {
            return weaponDamage;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
    }

    public bool Shooting
    {
        get
        {
            return shooting;
        }
        set
        {
            shooting = value;
        }
    }

    public bool Crouching
    {
        get
        {
            return crouching;
        }
        set
        {
            crouching = value;
        }
    }

    #endregion

    void Start()
    {
        health = totalHealth;
    }

    #region Public Methods

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    #endregion



}
