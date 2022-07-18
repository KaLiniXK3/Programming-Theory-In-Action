using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //references
    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public GameObject player;
    [SerializeField] PatrolPath patrolPath;

    //Variables
    [Header("Attack Variables")]
    //Attack
    [SerializeField] protected int health;
    [SerializeField] protected int minDamage;
    [SerializeField] protected int maxDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float cooldown;
    bool canAttack;

    [Header("Movement Variables")]
    //Movement
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float observationSphereRadius;
    [SerializeField] float suspiciousTime;
    float timeSinceLastSawPlayer;
    bool isTriggered;
    protected Transform currentTarget;

    [Header("Patrol Variables")]
    //Patrol
    [SerializeField] float wayPointTolerance = 1f;
    [SerializeField] float wayPointWaitTime;
    int currentWayPointIndex;
    float timeSinceArrivedWayPoint;
    float time;

    private void Update()
    {
        isTriggered = ObservationZone();
    }
    private void FixedUpdate()
    {
        if (isTriggered)
        {
            MoveToTarget(player.transform);
            Attack();
            timeSinceLastSawPlayer = 0;
        }
        else if (timeSinceLastSawPlayer < suspiciousTime)
        {
            //Wait
        }
        else
        {
            Patrol();
        }
        timeSinceLastSawPlayer += Time.fixedDeltaTime;
    }

    //General Methods
    protected void MoveToTarget(Transform target)
    {
        rb.MovePosition(transform.position + Target(target) * speed * Time.fixedDeltaTime);
        MoveType();
    }
    protected virtual void MoveType()
    {

    }
    protected float DistanceBetweenPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
    bool ObservationZone()
    {
        if (DistanceBetweenPlayer() < observationSphereRadius)
        {
            return true;
        }
        else return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, observationSphereRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    //Attack Methods
    void Attack()
    {
        if (DistanceBetweenPlayer() < attackRange && canAttack)
        {
            DealDamage();
            canAttack = false;
            Debug.Log(PlayerHealth.health);
        }
        if (!canAttack)
        {
            time += Time.deltaTime;
            if (time > cooldown)
            {
                canAttack = true;
                time = 0;
            }
        }
    }
    protected virtual void DealDamage()
    {
        PlayerHealth.health -= Random.Range(minDamage, maxDamage);
        if (PlayerHealth.health < 0)
        {
            PlayerHealth.health = 0;
        }
        PlayerHealth.UpdateHealthText();
    }

    //Patrol Methods
    void Patrol()
    {
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                timeSinceArrivedWayPoint = 0;
                CycleWayPoint();
            }
        }
        if (timeSinceArrivedWayPoint > wayPointWaitTime)
        {
            MoveToTarget(GetNextWayPoint());
        }
        timeSinceArrivedWayPoint += Time.deltaTime;
    }
    private Transform GetNextWayPoint()
    {
        return patrolPath.GetWayPointPosition(currentWayPointIndex);
    }
    private void CycleWayPoint()
    {
        currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
    }
    private bool AtWayPoint()
    {
        float distance = Vector3.Distance(transform.position, GetNextWayPoint().position);
        return distance < wayPointTolerance;
    }

    //Target overloads
    protected Vector3 Target(GameObject target)
    {
        return Vector3.Normalize(target.transform.position - transform.position);
    }
    protected Vector3 Target(Transform target)
    {
        currentTarget = target;

        return Vector3.Normalize(target.position - transform.position);
    }
}

