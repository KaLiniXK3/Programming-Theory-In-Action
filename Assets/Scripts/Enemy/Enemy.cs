using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //references
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public GameObject player;
    [SerializeField] PatrolPath patrolPath;

    //Variables
    //Attack
    [SerializeField] protected int health;
    [SerializeField] protected int minDamage;
    [SerializeField] protected int maxDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float cooldown;
    //Movement
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float observationSphereRadius;
    //Patrol
    [SerializeField] float wayPointTolerance = 1f;
    [SerializeField] int currentWayPointIndex;
    bool isTriggered;
    float time;
    bool canAttack;


    private void Update()
    {
        isTriggered = ObservationZone();
        Attack();
    }
    private void FixedUpdate()
    {
        if (isTriggered)
        {
            MoveToTarget(Target(player));
        }
        else
        {
            Patrol();
        }
    }

    //General Methods
    protected void MoveToTarget(Vector3 target)
    {
        rb.MovePosition(transform.position + target * speed * Time.fixedDeltaTime);
        MoveType();
    }
    protected virtual void MoveType()
    {

    }
    bool ObservationZone()
    {
        if (DistanceBetweenPlayer() < observationSphereRadius)
        {
            return true;
        }
        else return false;
    }
    private float DistanceBetweenPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, observationSphereRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
    //

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
    }
    //

    //Patrol Methods
    void Patrol()
    {
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                CycleWayPoint();
            }
        }
        MoveToTarget(Target(GetNextWayPoint()));
    }
    private Vector3 GetNextWayPoint()
    {
        return patrolPath.GetWayPointPosition(currentWayPointIndex);
    }
    private void CycleWayPoint()
    {
        currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
    }
    private bool AtWayPoint()
    {
        float distance = Vector3.Distance(transform.position, GetNextWayPoint());
        return distance < wayPointTolerance;
    }
    //

    //Target overloads
    protected Vector3 Target(GameObject target)
    {
        return Vector3.Normalize(target.transform.position - transform.position);
    }
    protected Vector3 Target(Vector3 target)
    {
        return Vector3.Normalize(target - transform.position);
    }
    //
}

