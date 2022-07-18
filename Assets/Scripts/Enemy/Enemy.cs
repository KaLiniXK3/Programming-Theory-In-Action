using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Encapsulation
    //Inheritance

    //references
    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public GameObject player;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] ParticleSystem dealDamageParticle;

    //Variables
    [Header("Attack Variables")]
    //Attack
    [SerializeField] protected int health;
    [SerializeField] protected int minDamage;
    [SerializeField] protected int maxDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float forceMultiplier;
    bool canAttack;
    bool canDealDamage = true;

    [Header("Movement Variables")]
    //Movement
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float followSpeed;
    [SerializeField] protected float observationSphereRadius;
    [SerializeField] float suspiciousTime;
    float timeSinceLastSawPlayer;
    bool isTriggered;
    Vector3 startPosition;
    protected Transform currentTarget;

    [Header("Patrol Variables")]
    //Patrol
    [SerializeField] float wayPointTolerance = 1f;
    [SerializeField] float wayPointWaitTime;
    int currentWayPointIndex;
    float timeSinceArrivedWayPoint;
    float time;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        isTriggered = ObservationZone();
    }
    private void FixedUpdate()
    {
        if (isTriggered)
        {
            MoveToTarget(player.transform);
            speed = followSpeed;
            Attack();
            timeSinceLastSawPlayer = 0;
        }
        else if (timeSinceLastSawPlayer < suspiciousTime)
        {
            speed = walkSpeed;
            //Wait
        }
        else
        {
            Patrol();
        }
        timeSinceLastSawPlayer += Time.fixedDeltaTime;
    }

    //Abstractions
    //Polymorphisms
    //Encapsulations
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
            rb.AddForce(Target(player) * forceMultiplier);
            canAttack = false;
        }
        if (!canAttack)
        {
            time += Time.deltaTime;
            if (time > cooldown)
            {
                canAttack = true;
                canDealDamage = true;
                time = 0;
            }
        }
    }
    protected virtual void DealDamage()
    {
        PlayerHealth.health -= Random.Range(minDamage, maxDamage);
        Instantiate(dealDamageParticle, transform.position, Quaternion.identity);
        canAttack = false;
        if (PlayerHealth.health < 0)
        {
            PlayerHealth.health = 0;
        }
        PlayerHealth.UpdateHealthText();
    }
    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            GetComponent<Collider>().enabled = false;
            rb.AddForce(Vector3.up * 1500);
            Destroy(gameObject, 3);
        }
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
    //Polymorphism

    protected Vector3 Target(GameObject target)
    {
        return Vector3.Normalize(target.transform.position - transform.position);
    }
    protected Vector3 Target(Transform target)
    {
        currentTarget = target;

        return Vector3.Normalize(target.position - transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = startPosition;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canDealDamage)
            {
                DealDamage();
                canDealDamage = false;
            }
        }
    }
}


