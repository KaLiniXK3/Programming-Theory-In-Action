using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : Enemy
{
    //Variables
    [Header("Enemy Special Variables")]
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundMask;
    Vector3 velocityCube;
    [SerializeField] float rotateSpeed;

    bool canJump = true;
    protected override void MoveType()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(Target(currentTarget));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.fixedDeltaTime * rotateSpeed);
        if (canJump)
        {
            Jump();
        }
    }
    void Jump()
    {
        velocityCube.y = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);
        rb.velocity += velocityCube;
        canJump = false;
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = true;
        }
    }
}
