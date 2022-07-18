using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRectangle : Enemy
{
    //Inheritance

    [Header("Enemy Special Variables")]
    [SerializeField] float rotateSpeed;

    //Polymorphism
    //Encapsulation
    protected override void MoveType()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }
}
