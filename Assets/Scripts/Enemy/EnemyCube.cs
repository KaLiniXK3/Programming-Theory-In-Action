using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : Enemy
{
    //variables
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundMask;
    Vector3 velocityCube;

    bool canJump = true;
    protected override void MoveType()
    {
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = true;
        }
    }
}
