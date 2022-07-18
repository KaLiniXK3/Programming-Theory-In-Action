using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : Enemy
{
    //Inheritance

    [Header("Enemy Special Variables")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed2;
    [SerializeField] float speed3;

    //Polymorphism
    //Encapsulation
    protected override void MoveType()
    {
        transform.LookAt(currentTarget);
        transform.Rotate(Vector3.up, 90);
        transform.Rotate(Vector3.forward, rotateSpeed);
        rotateSpeed += Time.fixedDeltaTime * speed2;
        if (rotateSpeed < -1080)
        {
            rotateSpeed = 0;
        }

    }
}
