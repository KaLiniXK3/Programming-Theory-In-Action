using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCylinder : Enemy
{
    [Header("Enemy Special Variables")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed2;
    protected override void MoveType()
    {
        transform.LookAt(currentTarget);
        transform.Rotate(Vector3.forward, 90);
        transform.Rotate(Vector3.up, rotateSpeed);
        rotateSpeed += Time.fixedDeltaTime * speed2;
    }
}
