using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D ThisRigidBody2D;

    [Header("Movement Params")]
    public float Speed;
    public float JumpForce;
    public float PushOffset = 0.02f;
    public float TerminalVelocity = 1f;


    private Vector2 velocity;

    private bool onAir = false;

    /*protected void Update()
    {
        CheckVelocity();

        transform.position += (Vector3)velocity * Speed * Time.deltaTime; 
    }

    private void CheckVelocity()
    {
        velocity.x = Input.GetAxis("Horizontal");
        //velocity.y = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(ThisRigidBody2D.linearVelocityY) < 0.01f)
        {
            ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
        }
    }*/

    private void Start()
    {
        float idealDrag = Speed / TerminalVelocity;
        ThisRigidBody2D.linearDamping = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);
    }

    private void FixedUpdate()
    {
        CheckOnAir();
        float direction = Input.GetAxis("Horizontal");
        if (!onAir)
        {
            ThisRigidBody2D.linearVelocityX = 0f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                ThisRigidBody2D.AddForceX(Speed * 0.5f, ForceMode2D.Impulse);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                ThisRigidBody2D.AddForceX(Speed * -0.5f, ForceMode2D.Impulse);
            }
        }
        else
        {
            ThisRigidBody2D.AddForceX(Speed * direction * PushOffset, ForceMode2D.Impulse);
        }
    }

    private void CheckOnAir()
    {
        onAir = Mathf.Abs(ThisRigidBody2D.linearVelocityY) > 0.02f;
    }
}
