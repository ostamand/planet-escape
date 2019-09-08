using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterManager))]
public class AutoShootController : MonoBehaviour
{
    [Header("Action Properties")]

    [SerializeField]
    private float baseShootProb = 0.1f;

    [SerializeField]
    private float baseIdleProb = 0.3f;

    [SerializeField]
    [Tooltip("More chance of shooting if target is standing up")]
    private float oppProb = 0.2f;

    [SerializeField]
    [Tooltip("More change of doing something if we were idle before")]
    private float idleBeforeProb = 0.2f;

    [Header("Stats")]

    [SerializeField]
    [Tooltip("Chance hit when enemy is crouching")]
    private float hitCrouchingProb = 0.2f;

    [SerializeField]
    [Tooltip("Chance hit when enemy is not crouching")]
    private float hitIdleProb = 0.8f;

    [Space(10)]

    [SerializeField]
    private GameObject[] enemies = new GameObject[1];

    private CharacterManager character;

    // for shooting
    private int _currentNumOfShots = 0;
    private int _totalNumberOfShots = 0;
    private SpawnProjectiles projectiles;

    public enum CharacterAction { Shoot0 = 0, Shoot1 = 1, Idle = 2 }
    private CharacterAction _previousAction = CharacterAction.Idle;
    private CharacterAction _currentAction = CharacterAction.Idle;

    #region Public Properties

    public CharacterAction PreviousAction
    {
        get
        {
            return _previousAction;
        }
        set
        {
            _previousAction = value;
        }
    }

    public CharacterAction CurrentAction
    {
        get
        {
            return _currentAction;
        }
        set
        {
            _currentAction = value ;
        }
    }

    public Animator Animator { get; private set; }

    public bool Shooting
    {
        get
        {
            return character.Shooting;
        }
        set
        {
            if (character.Shooting != value)
            {
                character.Shooting = value;
                Crouching = !value;
                Animator.SetBool("Shooting", character.Shooting);
            }
        }
    }

    public bool Crouching
    {
        get
        {
            return character.Crouching;
        }
        set
        {
            if (character.Crouching != value)
            {
                character.Crouching = value;
                Animator.SetBool("Crouching", character.Crouching);
            }
        }
    }

    public Vector3 ShootingDirection { get; set; }

    #endregion

    public static Dictionary<CharacterAction, string> ActionLabels = new Dictionary<CharacterAction, string>
    {
        { CharacterAction.Shoot0, "Shoot0"},
        { CharacterAction.Shoot1, "Shoot1"},
        { CharacterAction.Idle, "Idle"}
    };

    public static Dictionary<int, CharacterAction> IndexToAction = new Dictionary<int, CharacterAction>
    {
        { 0, CharacterAction.Shoot0},
        { 1, CharacterAction.Shoot1},
        { 2, CharacterAction.Idle}
    };

    #region Private Methods

    void Rotate(float angle, float totalTime)
    {
        transform.Rotate(Vector3.up, Time.deltaTime * angle / totalTime );
    }

    void ShootForward()
    {
        ShootingDirection = transform.forward;
        Shooting = true;
    }

    #endregion

    void Start()
	{
        projectiles = GetComponent<SpawnProjectiles>();
        character = GetComponent<CharacterManager>();
        Animator = GetComponent<Animator>();

        Crouching = true;

        StartCrouching();
    }

    void Update()
    {

    }

    #region Public Methods

    public void ShootProjectile()
    {
        if (!character.Shooting) { return; }
        projectiles.Fire(ShootingDirection, character.WeaponDamage);
        _currentNumOfShots++;
        Shooting &= _currentNumOfShots < _totalNumberOfShots;
        Crouching = !Shooting;
    }

    public void StartShooting(Vector3 target, int numberOfShots, CharacterAction actionName)
    {
        CurrentAction = actionName;
        ShootingDirection = (target - transform.position).normalized;
        _totalNumberOfShots = numberOfShots;
         _currentNumOfShots = 0;
        // will trigger start of animation
        Shooting = true;
    }

    public void StartCrouching()
    {
        if (!Crouching) { return; }
        Animator.SetBool("Crouching", true);
    }

    public void CanDoAction()
    {
        int i;

        // shoot cop 0, shoot cop 1, do nothing

        float[] probs = { baseShootProb, baseShootProb, baseIdleProb };

        // more chance of doing someoppAddif we were idle before

        if (PreviousAction == CharacterAction.Idle)
        {
            probs[1] += idleBeforeProb;
            probs[2] += idleBeforeProb;
        }

        // more chance of shooting if target is standing up

        for (i = 0; i < enemies.Length; i++)
        {
            probs[i] = enemies[i].GetComponent<CharacterManager>().Crouching ? probs[i] : probs[i] + oppProb;
        }

        // get the actual next action

        float total = probs.Sum();
        probs = probs.Select(f => f / total).ToArray();

        for (i = 1; i < probs.Length; i++)
        {
            probs[i] = probs[i] + probs[i - 1];
        }

        float choice = UnityEngine.Random.value;
        float lowerBound = 0f;
        CharacterAction nextAction = CharacterAction.Idle;

        for (i = 0; i < ActionLabels.Count; i++)
        {
            if (choice >= lowerBound && choice < probs[i])
            {
                nextAction = IndexToAction[i];
                break;
            }
        }

        // apply the action
        if (nextAction == CharacterAction.Shoot0) // || nextAction == Character.Action.Shoot1
        {
            int numOfShots = (int)(UnityEngine.Random.value * 3) + 1;
            Vector3 target = enemies[i].transform.position;
            StartShooting(target, numOfShots, nextAction);
        }
    }

    #endregion
}
